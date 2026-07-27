using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }



    public bool IsGameFinished { get; private set; }








    private void Awake()
    {

        if (Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

    }









    // =========================
    // PLAYER MENANG
    // =========================

    public void Victory()
    {

        if (IsGameFinished)
            return;



        IsGameFinished = true;



        Debug.Log(
            "PLAYER VICTORY"
        );



        if (TurnManager.Instance != null)
        {
            TurnManager.Instance
                .EndGame();
        }



        if (LevelCompleteManager.Instance != null)
        {

            LevelCompleteManager.Instance
                .LevelComplete();

        }

    }









    // =========================
    // PLAYER KALAH
    // =========================

    public void GameOver()
    {

        if (IsGameFinished)
            return;



        IsGameFinished = true;



        Debug.Log(
            "GAME OVER"
        );



        if (TurnManager.Instance != null)
        {
            TurnManager.Instance
                .EndGame();
        }



        if (DefeatScreenManager.Instance != null)
        {

            DefeatScreenManager.Instance
                .ShowDefeat();

        }

    }









    // =========================
    // CEK HERO MATI
    // =========================

    public void CheckGameOver()
    {

        if (IsGameFinished)
            return;



        ChessPiece[] pieces =
            FindObjectsByType<ChessPiece>(
                FindObjectsSortMode.None
            );



        if (pieces.Length > 0)
            return;



        GameOver();

    }









    public void RestartGame()
    {

        Time.timeScale = 1f;



        SceneManager.LoadScene(
            SceneManager.GetActiveScene()
            .buildIndex
        );

    }









    public void ReturnToMenu()
    {

        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    "MainMenu"
                );

        }
        else
        {

            SceneManager.LoadScene(
                "MainMenu"
            );

        }

    }









    public void QuitGame()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication
            .isPlaying = false;

#else

        Application.Quit();

#endif

    }

}