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



    private GameObject currentPanel;







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


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMenuMusic();
        }
    }









    private void DisableAll()
    {

        if (mainPanel != null)
            mainPanel.SetActive(false);


        if (levelPanel != null)
            levelPanel.SetActive(false);


        if (settingsPanel != null)
            settingsPanel.SetActive(false);


        if (creditsPanel != null)
            creditsPanel.SetActive(false);

    }









    public void ShowMain()
    {

        DisableAll();


        if (mainPanel != null)
            mainPanel.SetActive(true);


        currentPanel =
            mainPanel;


    }









    public void ShowLevelSelect()
    {

        DisableAll();


        if (levelPanel != null)
            levelPanel.SetActive(true);


        currentPanel =
            levelPanel;


    }









    public void ShowSettings()
    {

        DisableAll();


        if (settingsPanel != null)
            settingsPanel.SetActive(true);


        currentPanel =
            settingsPanel;

    }









    public void ShowCredits()
    {

        DisableAll();


        if (creditsPanel != null)
            creditsPanel.SetActive(true);


        currentPanel =
            creditsPanel;

    }









    public void BackToMain()
    {

        ShowMain();

    }









    public void PlayGame()
    {

        Debug.Log(
            "Start Game"
        );



        if (MenuAudioManager.Instance != null)
        {
            MenuAudioManager.Instance
                .PlayClick();
        }



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    "Level1"
                );

        }
        else
        {

            Debug.LogError(
                "SceneLoader tidak ditemukan"
            );

        }

    }









    public void LoadLevel(
        int level)
    {

        if (SceneLoader.Instance == null)
        {
            Debug.LogError(
                "SceneLoader tidak ditemukan"
            );

            return;
        }



        SceneLoader.Instance
            .LoadScene(
                "Level" + level
            );

    }









    public void QuitGame()
    {

        Debug.Log(
            "Quit Game"
        );


#if UNITY_EDITOR

        UnityEditor.EditorApplication
            .isPlaying = false;

#else

        Application.Quit();

#endif

    }

}