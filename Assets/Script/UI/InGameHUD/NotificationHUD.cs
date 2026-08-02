using UnityEngine;
using TMPro;
using System.Collections;


public class NotificationHUD : MonoBehaviour
{

    public static NotificationHUD Instance;



    [Header("UI")]
    [SerializeField]
    private CanvasGroup canvasGroup;


    [SerializeField]
    private TMP_Text notificationText;



    [Header("Animation")]
    [SerializeField]
    private float fadeInTime = 0.2f;


    [SerializeField]
    private float stayTime = 1.5f;


    [SerializeField]
    private float fadeOutTime = 0.3f;


    [SerializeField]
    private float popScale = 1.3f;



    private Vector3 startScale;



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

        if (notificationText != null)
        {
            startScale =
                notificationText
                .transform
                .localScale;
        }


        Hide();

    }







    public void Show(string message)
    {

        StopAllCoroutines();


        StartCoroutine(
            ShowRoutine(message)
        );

    }








    private IEnumerator ShowRoutine(
        string message)
    {


        notificationText.text =
            message;



        notificationText
            .transform
            .localScale =
            startScale *
            popScale;



        canvasGroup.alpha = 0;



        float timer = 0;



        while (timer < fadeInTime)
        {

            timer += Time.deltaTime;


            canvasGroup.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    timer / fadeInTime
                );


            notificationText
                .transform
                .localScale =
                Vector3.Lerp(
                    startScale * popScale,
                    startScale,
                    timer / fadeInTime
                );


            yield return null;

        }





        yield return new WaitForSeconds(
            stayTime
        );






        timer = 0;


        while (timer < fadeOutTime)
        {

            timer += Time.deltaTime;


            canvasGroup.alpha =
                Mathf.Lerp(
                    1,
                    0,
                    timer / fadeOutTime
                );


            yield return null;

        }



        Hide();

    }








    private void Hide()
    {

        if (canvasGroup != null)
            canvasGroup.alpha = 0;


    }





}