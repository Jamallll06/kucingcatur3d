using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;

    public bool IsGameFinished { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void Victory()
    {
        if (IsGameFinished)
            return;

        IsGameFinished = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        TurnManager.Instance.EndGame();

        Debug.Log("VICTORY!");
    }

    public void CheckGameOver()
    {
        if (IsGameFinished)
            return;

        ChessPiece[] livingPieces = FindObjectsByType<ChessPiece>(
            FindObjectsSortMode.None
        );

        if (livingPieces.Length > 0)
            return;

        IsGameFinished = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        TurnManager.Instance.EndGame();

        Debug.Log("GAME OVER!");
    }
}