using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class LevelCompleteManager : MonoBehaviour
{

    public static LevelCompleteManager Instance { get; private set; }



    [Header("Victory UI")]
    [SerializeField]
    private GameObject victoryPanel;


    [SerializeField]
    private TMP_Text titleText;


    [SerializeField]
    private TMP_Text levelText;



    [Header("Delay")]
    [SerializeField]
    private float showDelay = 1f;



    private bool completed;







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









    private void Start()
    {

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

    }









    public void LevelComplete()
    {

        if (completed)
            return;


        completed = true;



        Invoke(
            nameof(ShowVictory),
            showDelay
        );

    }









    private void ShowVictory()
    {

        if (victoryPanel != null)
            victoryPanel.SetActive(true);



        if (titleText != null)
        {
            titleText.text =
                "VICTORY!";
        }



        if (levelText != null &&
           LevelManager.Instance != null)
        {

            levelText.text =
                "LEVEL "
                +
                LevelManager.Instance.CurrentLevel
                +
                " COMPLETE";

        }



        UnlockNextLevel();

    }









    private void UnlockNextLevel()
    {

        if (LevelManager.Instance == null)
            return;



        LevelManager.Instance.LevelComplete();



        Debug.Log(
            "Level berhasil diselesaikan"
        );

    }









    public void NextLevel()
    {

        if (LevelManager.Instance == null)
            return;



        int nextLevel =
            LevelManager.Instance.CurrentLevel;



        if (nextLevel > 5)
        {

            Debug.Log(
                "Semua level selesai"
            );


            ReturnToMenu();

            return;

        }




        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance.LoadScene(
                "Level" + nextLevel
            );

        }
        else
        {

            SceneManager.LoadScene(
                "Level" + nextLevel
            );

        }

    }









    public void RetryLevel()
    {

        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance.LoadScene(
                SceneManager.GetActiveScene().name
            );

        }
        else
        {

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );

        }

    }









    public void ReturnToMenu()
    {

        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance.LoadScene(
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

}