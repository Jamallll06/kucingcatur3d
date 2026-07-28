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






    // =====================================
    // CALL WHEN BOSS DEFEATED
    // =====================================

    public void CompleteLevel()
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



        UnlockLevel();



        if (AudioManager.Instance != null)
        {
            AudioManager.Instance
                .PlayVictory();
        }

    }








    private void UnlockLevel()
    {

        if (LevelManager.Instance == null)
            return;



        int nextLevel =
            LevelManager.Instance.CurrentLevel + 1;



        if (SaveManager.Instance != null)
        {
            SaveManager.Instance
                .UnlockLevel(nextLevel);
        }


    }









    // =====================================
    // BUTTON NEXT LEVEL
    // =====================================

    public void NextLevel()
    {

        if (LevelManager.Instance == null)
            return;



        int next =
            LevelManager.Instance.CurrentLevelIndex + 1;



        // selesai semua level

        if (next >= 5)
        {

            Debug.Log(
                "Semua level selesai!"
            );


            ReturnMenu();

            return;
        }






        string sceneName =
            "Level"
            +
            (next + 1);



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(sceneName);

        }
        else
        {

            SceneManager.LoadScene(
                sceneName);

        }

    }








    // =====================================
    // RETRY
    // =====================================

    public void RetryLevel()
    {

        string currentScene =
            SceneManager.GetActiveScene()
            .name;



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(currentScene);

        }
        else
        {

            SceneManager.LoadScene(
                currentScene);

        }

    }








    // =====================================
    // MAIN MENU
    // =====================================

    public void ReturnMenu()
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


}