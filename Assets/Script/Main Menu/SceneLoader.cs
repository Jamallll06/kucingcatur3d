using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }


    [Header("Loading UI")]
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;


    [Header("Fade")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 0.5f;


    [Header("Settings")]
    [SerializeField] private bool dontDestroy = true;



    private void Awake()
    {
        if (Instance != null &&
            Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        if (dontDestroy)
            DontDestroyOnLoad(gameObject);
    }



    private void Start()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);


        if (fadeCanvas != null)
        {
            fadeCanvas.alpha = 1;

            StartCoroutine(
                FadeIn()
            );
        }
    }



    // =====================================
    // PUBLIC LOAD
    // =====================================

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError(
                "Scene name kosong!"
            );

            return;
        }


        StartCoroutine(
            LoadSceneRoutine(sceneName)
        );
    }



    public void LoadLevel(int level)
    {
        string sceneName =
            "Level" + level;


        LoadScene(sceneName);
    }



    // =====================================
    // LOAD ROUTINE
    // =====================================

    private IEnumerator LoadSceneRoutine(
        string sceneName)
    {

        if (loadingPanel != null)
            loadingPanel.SetActive(true);



        yield return StartCoroutine(
            FadeOut()
        );



        AsyncOperation operation =
            SceneManager.LoadSceneAsync(
                sceneName
            );


        operation.allowSceneActivation = false;



        while (!operation.isDone)
        {

            float progress =
                Mathf.Clamp01(
                    operation.progress / 0.9f
                );



            if (loadingSlider != null)
            {
                loadingSlider.value =
                    progress;
            }



            if (operation.progress >= 0.9f)
            {

                yield return new WaitForSeconds(
                    0.3f
                );


                operation.allowSceneActivation =
                    true;
            }


            yield return null;
        }



        yield return null;



        if (loadingPanel != null)
            loadingPanel.SetActive(false);



        yield return StartCoroutine(
            FadeIn()
        );
    }



    // =====================================
    // FADE
    // =====================================

    private IEnumerator FadeOut()
    {

        if (fadeCanvas == null)
            yield break;



        float timer = 0;



        while (timer < fadeDuration)
        {

            timer += Time.deltaTime;


            fadeCanvas.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    timer / fadeDuration
                );


            yield return null;
        }



        fadeCanvas.alpha = 1;
    }




    private IEnumerator FadeIn()
    {

        if (fadeCanvas == null)
            yield break;



        float timer = 0;



        while (timer < fadeDuration)
        {

            timer += Time.deltaTime;


            fadeCanvas.alpha =
                Mathf.Lerp(
                    1,
                    0,
                    timer / fadeDuration
                );


            yield return null;
        }



        fadeCanvas.alpha = 0;
    }

}