using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{

    public static ResultPanel Instance;


    [Header("Panel")]
    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject lossPanel;



    [Header("Animation")]
    [SerializeField]
    private float delay = 1f;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    private void Start()
    {
        winPanel.SetActive(false);
        lossPanel.SetActive(false);
    }



    public void ShowWin()
    {
        StartCoroutine(
            WinRoutine()
        );
    }



    public void ShowLoss()
    {
        StartCoroutine(
            LossRoutine()
        );
    }



    IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(delay);


        winPanel.SetActive(true);


        Time.timeScale = 0;
    }



    IEnumerator LossRoutine()
    {
        yield return new WaitForSeconds(delay);


        lossPanel.SetActive(true);


        Time.timeScale = 0;
    }



    public void Retry()
    {
        Time.timeScale = 1;


        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }



    public void LevelSelect()
    {
        Time.timeScale = 1;


        SceneManager.LoadScene(
            "LevelSelect"
        );
    }



    public void MainMenu()
    {
        Time.timeScale = 1;


        SceneManager.LoadScene(
            "MainMenu"
        );
    }


    public void NextLevel()
    {
        Time.timeScale = 1;


        if (LevelManager.Instance == null)
        {
            Debug.LogError(
                "LevelManager tidak ditemukan"
            );

            return;
        }



        LevelManager.Instance.LevelComplete();



        LevelManager.Instance.LoadNextLevel();
    }

}