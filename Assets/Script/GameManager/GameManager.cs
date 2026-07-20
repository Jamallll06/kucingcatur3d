using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
    }



    public void GameOver()
    {

        Debug.Log(
            "GAME OVER"
        );


        TurnManager.Instance.EndGame();

    }



    public void Victory()
    {

        Debug.Log(
            "VICTORY"
        );


        TurnManager.Instance.Victory();

    }



    public void RestartGame()
    {

        Time.timeScale = 1;


        SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex);

    }



    public void CheckGameOver()
    {

        TransformingPiece hero =
        FindFirstObjectByType<TransformingPiece>();


        if (hero.CurrentHealth <= 0)
        {
            GameOver();
        }

    }

    // =========================
    // QUIT GAME
    // =========================

    public void QuitGame()
    {
        Debug.Log("Quit Game");


#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

    Application.Quit();

#endif

    }

}