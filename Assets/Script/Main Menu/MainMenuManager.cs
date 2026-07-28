using UnityEngine;


public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager Instance { get; private set; }



    [Header("Panels")]
    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject levelPanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject creditsPanel;

    [SerializeField]
    private GameObject loadingPanel;



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

        ShowMain();

    }








    // ============================
    // SHOW MAIN PANEL
    // ============================

    public void ShowMain()
    {

        SetPanel(
            true,
            false,
            false,
            false
        );



        if (loadingPanel != null)
            loadingPanel.SetActive(false);

    }









    // ============================
    // LEVEL SELECT
    // ============================

    public void ShowLevelSelect()
    {

        SetPanel(
            false,
            true,
            false,
            false
        );


    }









    // ============================
    // SETTINGS
    // ============================

    public void ShowSettings()
    {

        SetPanel(
            false,
            false,
            true,
            false
        );


    }









    // ============================
    // CREDIT
    // ============================

    public void ShowCredits()
    {

        SetPanel(
            false,
            false,
            false,
            true
        );


    }









    private void SetPanel(
        bool main,
        bool level,
        bool settings,
        bool credits)
    {


        if (mainPanel != null)
            mainPanel.SetActive(main);


        if (levelPanel != null)
            levelPanel.SetActive(level);


        if (settingsPanel != null)
            settingsPanel.SetActive(settings);


        if (creditsPanel != null)
            creditsPanel.SetActive(credits);


    }










    // ============================
    // CONTINUE GAME
    // ============================

    public void ContinueGame()
    {

        int level = 0;



        if (SaveManager.Instance != null)
        {

            level =
                SaveManager.Instance
                .GetCurrentLevel();

        }
        else if (LevelManager.Instance != null)
        {

            level =
                LevelManager.Instance
                .CurrentLevelIndex;

        }



        LoadLevel(level);

    }









    // ============================
    // NEW GAME
    // ============================

    public void NewGame()
    {

        if (SaveManager.Instance != null)
        {

            SaveManager.Instance
                .ResetSave();

        }


        if (LevelManager.Instance != null)
        {

            LevelManager.Instance
                .ResetProgress();

        }



        LoadLevel(0);

    }









    // ============================
    // PLAY FIRST LEVEL
    // ============================

    public void PlayGame()
    {

        LoadLevel(0);

    }









    private void LoadLevel(int index)
    {

        ShowLoading();



        if (LevelManager.Instance != null)
        {

            LevelManager.Instance
                .StartLevel(index);


            return;

        }




        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    "Level"
                    +
                    (index + 1)
                );

        }

    }









    // ============================
    // LOADING
    // ============================

    private void ShowLoading()
    {

        if (loadingPanel != null)
            loadingPanel.SetActive(true);

    }









    // ============================
    // RESET SAVE BUTTON
    // ============================

    public void ResetSave()
    {

        if (SaveManager.Instance != null)
        {

            SaveManager.Instance
                .ResetSave();

        }



        if (LevelManager.Instance != null)
        {

            LevelManager.Instance
                .ResetProgress();

        }



        Debug.Log(
            "SAVE RESET"
        );

    }









    // ============================
    // QUIT
    // ============================

    public void QuitGame()
    {

        Debug.Log(
            "QUIT GAME"
        );


#if UNITY_EDITOR

        UnityEditor.EditorApplication
            .isPlaying = false;


#else

        Application.Quit();


#endif

    }

}