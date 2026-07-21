using UnityEngine;
using UnityEngine.SceneManagement;

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



        if (TurnManager.Instance != null)
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



        if (TurnManager.Instance != null)
            TurnManager.Instance.EndGame();



        Debug.Log("GAME OVER!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Keluar dari game.");
    }
}