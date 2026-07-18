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

    [SerializeField] private BossAI bossAI;
    [SerializeField] private float bossTurnDuration = 1.5f;

    public TurnState CurrentTurn { get; private set; }

    public bool IsPlayerTurn =>
        CurrentTurn == TurnState.PlayerTurn;

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
        CurrentTurn = TurnState.PlayerTurn;
        Debug.Log("Giliran Player");
    }

    public void EndPlayerTurn()
    {
        if (!IsPlayerTurn)
            return;

        StartCoroutine(BossTurnRoutine());
    }

    private IEnumerator BossTurnRoutine()
    {
        CurrentTurn = TurnState.BossTurn;
        Debug.Log("Giliran Boss");

        if (bossAI != null && bossAI.gameObject.activeInHierarchy)
            yield return bossAI.ExecuteTurn();
        else
            yield return null;

        BeginPlayerTurn();
    }

    public void EndGame()
    {
        CurrentTurn = TurnState.GameOver;
    }
}