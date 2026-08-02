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

    [Header("Turn Settings")]
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
        if (bossAI == null)
            bossAI = FindFirstObjectByType<BossAI>();

        if (bossAbility == null)
            bossAbility = FindFirstObjectByType<BossAbilityManager>();

        BeginPlayerTurn();
    }

    //--------------------------------------------------
    // PLAYER TURN
    //--------------------------------------------------

    public void BeginPlayerTurn()
    {
        if (CurrentTurn == TurnState.GameOver)
            return;

        CurrentTurn = TurnState.PlayerTurn;
        turnRunning = false;

        Debug.Log("===== PLAYER TURN =====");

        if (HUDManager.Instance != null)
            HUDManager.Instance.ShowMessage("PLAYER TURN");
    }

    public void EndPlayerTurn()
    {
        if (CurrentTurn != TurnState.PlayerTurn)
            return;

        if (turnRunning)
            return;

        StartCoroutine(BossTurnRoutine());
    }

    //--------------------------------------------------
    // BOSS TURN
    //--------------------------------------------------

    private IEnumerator BossTurnRoutine()
    {
        turnRunning = true;

        CurrentTurn = TurnState.BossTurn;

        Debug.Log("===== BOSS TURN =====");

        if (HUDManager.Instance != null)
            HUDManager.Instance.ShowMessage("BOSS TURN");

        yield return new WaitForSeconds(bossTurnDelay);

        // Boss Ability
        if (bossAbility != null &&
            bossAbility.enabled &&
            bossAbility.gameObject.activeInHierarchy)
        {
            yield return bossAbility.ExecuteAbilityRoutine();
        }

        // Boss AI
        if (bossAI != null &&
            bossAI.enabled &&
            bossAI.gameObject.activeInHierarchy)
        {
            yield return bossAI.ExecuteTurn();
        }

        // Minion Turn
        MinionAI[] minions =
            FindObjectsByType<MinionAI>(FindObjectsSortMode.None);

        foreach (MinionAI minion in minions)
        {
            if (minion == null)
                continue;

            if (!minion.enabled)
                continue;

            if (!minion.gameObject.activeInHierarchy)
                continue;

            yield return minion.ExecuteTurn();
        }

        yield return new WaitForSeconds(bossTurnDelay);

        if (CurrentTurn == TurnState.GameOver)
            yield break;

        BeginPlayerTurn();
    }

    //--------------------------------------------------
    // GAME
    //--------------------------------------------------

    public void EndGame()
    {
        if (CurrentTurn == TurnState.GameOver)
            return;

        CurrentTurn = TurnState.GameOver;
        turnRunning = false;

        Debug.Log("===== GAME OVER =====");

        if (HUDManager.Instance != null)
            HUDManager.Instance.ShowMessage("GAME OVER");

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameOver();
    }

    //--------------------------------------------------
    // UTILITY
    //--------------------------------------------------

    public void SkipBossTurn()
    {
        if (CurrentTurn != TurnState.BossTurn)
            return;

        StopAllCoroutines();

        BeginPlayerTurn();
    }

    public void RestartTurn()
    {
        StopAllCoroutines();

        turnRunning = false;

        BeginPlayerTurn();
    }
}