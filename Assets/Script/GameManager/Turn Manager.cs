using System.Collections;
using UnityEngine;

public enum TurnState
{
    PlayerTurn,
    BossTurn,
    GameOver
}

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [Header("Boss")]
    [SerializeField] private BossAI bossAI;
    [SerializeField] private BossAbilityManager bossAbility;

    [Header("Delay")]
    [SerializeField] private float bossTurnDelay = 0.5f;

    public TurnState CurrentTurn { get; private set; }

    public bool IsPlayerTurn => CurrentTurn == TurnState.PlayerTurn;
    public bool IsBossTurn => CurrentTurn == TurnState.BossTurn;

    private bool turnRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        BeginPlayerTurn();
    }

    public void BeginPlayerTurn()
    {
        if (CurrentTurn == TurnState.GameOver)
            return;

        CurrentTurn = TurnState.PlayerTurn;
        turnRunning = false;

        Debug.Log("===== PLAYER TURN =====");
    }

    public void EndPlayerTurn()
    {
        if (CurrentTurn != TurnState.PlayerTurn)
            return;

        if (turnRunning)
            return;

        StartCoroutine(BossTurnRoutine());
    }

    private IEnumerator BossTurnRoutine()
    {
        turnRunning = true;

        CurrentTurn = TurnState.BossTurn;

        Debug.Log("===== BOSS TURN =====");

        yield return new WaitForSeconds(bossTurnDelay);

        // Ability Boss
        if (bossAbility != null)
        {
            yield return bossAbility.ExecuteAbilityRoutine();
        }

        // Gerakan Boss
        if (bossAI != null &&
            bossAI.gameObject.activeInHierarchy)
        {
            yield return bossAI.ExecuteTurn();
        }

        // Gerakan seluruh Minion
        MinionAI[] minions =
            FindObjectsByType<MinionAI>(
                FindObjectsSortMode.None
            );

        foreach (MinionAI minion in minions)
        {
            if (minion != null)
                yield return minion.ExecuteTurn();
        }

        yield return new WaitForSeconds(bossTurnDelay);

        BeginPlayerTurn();
    }

    public void EndGame()
    {
        CurrentTurn = TurnState.GameOver;
        turnRunning = false;

        Debug.Log("===== GAME OVER =====");
    }
}