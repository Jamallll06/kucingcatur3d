using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;


    public int currentLevel = 1;



    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }



    public void CompleteLevel()
    {

        Debug.Log(
            "Level "
            + currentLevel
            + " selesai"
        );


        currentLevel++;


        LoadNextLevel();

    }



    public void LoadNextLevel()
    {

        string sceneName =
        "Level" + currentLevel;


        SceneManager.LoadScene(sceneName);

    }



    public void RestartLevel()
    {

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );

    }


}