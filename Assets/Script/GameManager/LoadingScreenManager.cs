using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LoadingScreenManager : MonoBehaviour
{

    public static LoadingScreenManager Instance { get; private set; }



    [Header("UI")]
    [SerializeField]
    private CanvasGroup loadingPanel;


    [SerializeField]
    private Slider progressBar;


    [SerializeField]
    private TMP_Text loadingText;



    [Header("Loading Tips")]
    [SerializeField]
    private string[] tips;



    [Header("Fade")]
    [SerializeField]
    private float fadeSpeed = 2f;



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









    public void LoadScene(
        string sceneName)
    {

        StartCoroutine(
            LoadRoutine(sceneName)
        );

    }









    private IEnumerator LoadRoutine(
        string sceneName)
    {

        yield return FadeIn();



        AsyncOperation operation =
            SceneManager.LoadSceneAsync(
                sceneName
            );



        operation.allowSceneActivation =
            false;



        while (!operation.isDone)
        {

            float progress =
                Mathf.Clamp01(
                    operation.progress / 0.9f
                );



            if (progressBar != null)
                progressBar.value =
                    progress;



            if (loadingText != null)
            {
                loadingText.text =
                    "Loading "
                    +
                    Mathf.RoundToInt(
                        progress * 100
                    )
                    +
                    "%";
            }



            if (operation.progress >= 0.9f)
            {

                yield return new WaitForSeconds(
                    0.5f
                );


                operation.allowSceneActivation =
                    true;

            }



            yield return null;

        }

    }









    private IEnumerator FadeIn()
    {

        if (loadingPanel == null)
            yield break;



        loadingPanel.alpha = 0;



        loadingPanel.gameObject
            .SetActive(true);



        while (
            loadingPanel.alpha < 1
        )
        {

            loadingPanel.alpha =
                Mathf.MoveTowards(
                    loadingPanel.alpha,
                    1,
                    Time.deltaTime *
                    fadeSpeed
                );


            yield return null;

        }

    }









    public void SetRandomTip()
    {

        if (
            tips.Length == 0 ||
            loadingText == null
        )
            return;



        int index =
            Random.Range(
                0,
                tips.Length
            );



        loadingText.text =
            tips[index];

    }

}