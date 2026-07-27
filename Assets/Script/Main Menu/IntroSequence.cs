using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class IntroSequence : MonoBehaviour
{

    [Header("UI")]
    [SerializeField]
    private CanvasGroup logo;


    [SerializeField]
    private CanvasGroup title;


    [SerializeField]
    private CanvasGroup menuButtons;



    [Header("Hero")]
    [SerializeField]
    private Transform hero;


    [SerializeField]
    private float heroMoveHeight = 1f;



    [Header("Timing")]
    [SerializeField]
    private float fadeDuration = 1f;


    [SerializeField]
    private float waitTime = 1f;



    private Vector3 heroStartPosition;

    private bool skipped;







    private void Start()
    {

        if (hero != null)
            heroStartPosition =
                hero.position;


        StartCoroutine(
            IntroRoutine()
        );

    }








    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) ||
           Input.GetKeyDown(KeyCode.Return))
        {
            SkipIntro();
        }

    }









    private IEnumerator IntroRoutine()
    {

        HideAll();



        yield return FadeIn(
            logo
        );



        yield return new WaitForSeconds(
            waitTime
        );



        yield return FadeIn(
            title
        );



        yield return HeroAppear();



        yield return new WaitForSeconds(
            0.5f
        );



        yield return FadeIn(
            menuButtons
        );

    }









    private void HideAll()
    {

        if (logo != null)
            logo.alpha = 0;


        if (title != null)
            title.alpha = 0;


        if (menuButtons != null)
            menuButtons.alpha = 0;

    }









    private IEnumerator FadeIn(
        CanvasGroup group)
    {

        if (group == null)
            yield break;



        float timer = 0;



        while (timer < fadeDuration)
        {

            timer += Time.deltaTime;


            group.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    timer / fadeDuration
                );


            yield return null;

        }


        group.alpha = 1;

    }









    private IEnumerator HeroAppear()
    {

        if (hero == null)
            yield break;



        Vector3 start =
            heroStartPosition -
            Vector3.up *
            heroMoveHeight;



        hero.position =
            start;



        float timer = 0;



        while (timer < fadeDuration)
        {

            timer += Time.deltaTime;



            hero.position =
                Vector3.Lerp(
                    start,
                    heroStartPosition,
                    timer / fadeDuration
                );



            yield return null;

        }


        hero.position =
            heroStartPosition;

    }









    public void SkipIntro()
    {

        if (skipped)
            return;


        skipped = true;



        StopAllCoroutines();



        if (logo != null)
            logo.alpha = 1;


        if (title != null)
            title.alpha = 1;


        if (menuButtons != null)
            menuButtons.alpha = 1;



        if (hero != null)
            hero.position =
                heroStartPosition;



        Debug.Log(
            "Intro skipped"
        );

    }

}