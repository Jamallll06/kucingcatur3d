using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseManager : MonoBehaviour
{

    public static PauseManager Instance { get; private set; }



    [Header("UI")]

    [SerializeField]
    private GameObject pausePanel;



    [Header("Pause Settings")]

    [SerializeField]
    private KeyCode pauseKey = KeyCode.Escape;



    private bool isPaused;



    public bool IsPaused => isPaused;







    private void Awake()
    {

        if(Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

    }







    private void Start()
    {

        if(pausePanel != null)
            pausePanel.SetActive(false);


        Time.timeScale = 1f;

    }







    private void Update()
    {

        if(Input.GetKeyDown(pauseKey))
        {

            if(isPaused)
                Resume();


            else
                Pause();

        }

    }









    // ==========================
    // PAUSE
    // ==========================


    public void Pause()
    {

        if(isPaused)
            return;



        isPaused = true;



        Time.timeScale = 0f;



        if(pausePanel != null)
        {
            pausePanel.SetActive(true);
        }



        Debug.Log(
            "GAME PAUSED"
        );

    }









    // ==========================
    // RESUME
    // ==========================


    public void Resume()
    {

        if(!isPaused)
            return;



        isPaused = false;



        Time.timeScale = 1f;



        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }



        Debug.Log(
            "GAME RESUME"
        );

    }









    // ==========================
    // RESTART
    // ==========================


    public void RestartLevel()
    {

        Time.timeScale = 1f;



        SceneManager.LoadScene(
            SceneManager
            .GetActiveScene()
            .name
        );


    }









    // ==========================
    // MENU
    // ==========================


    public void BackToMenu()
    {

        Time.timeScale = 1f;



        if(SceneLoader.Instance != null)
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









    // ==========================
    // BUTTON PAUSE
    // ==========================


    public void OpenPause()
    {

        Pause();

    }



    public void ClosePause()
    {

        Resume();

    }

}