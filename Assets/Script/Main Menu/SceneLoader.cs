using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class SceneLoader : MonoBehaviour
{

    public static SceneLoader Instance { get; private set; }



    [Header("Fade")]
    [SerializeField]
    private CanvasGroup fadeCanvas;


    [SerializeField]
    private float fadeDuration = 0.5f;



    [Header("Loading UI")]
    [SerializeField]
    private GameObject loadingPanel;


    [SerializeField]
    private Slider loadingBar;


    [SerializeField]
    private TMP_Text loadingText;



    [Header("Loading Message")]
    [SerializeField]
    private string[] loadingTips;



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

        if (fadeCanvas != null)
        {
            fadeCanvas.alpha = 1;

            StartCoroutine(
                FadeIn()
            );
        }



        if (loadingPanel != null)
            loadingPanel.SetActive(false);

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


        // =========================
        // SHOW LOADING
        // =========================

        if (loadingPanel != null)
            loadingPanel.SetActive(true);



        if (loadingBar != null)
            loadingBar.value = 0;



        SetRandomTip();



        // =========================
        // AUDIO FADE
        // =========================

        if (MenuAudioManager.Instance != null)
        {
            MenuAudioManager.Instance
                .FadeOutMusic();
        }




        yield return FadeOut();





        // =========================
        // LOAD SCENE
        // =========================

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
                    operation.progress /
                    0.9f
                );



            if (loadingBar != null)
            {
                loadingBar.value =
                    progress;
            }



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









    private void SetRandomTip()
    {

        if (loadingTips == null ||
           loadingTips.Length == 0)
            return;



        int index =
            Random.Range(
                0,
                loadingTips.Length
            );



        if (loadingText != null)
        {
            loadingText.text =
                loadingTips[index];
        }

    }

}