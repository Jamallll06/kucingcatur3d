using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;



public class ButtonAnimation : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{

    [Header("Scale")]
    [SerializeField]
    private float hoverScale = 1.1f;


    [SerializeField]
    private float clickScale = 0.9f;



    [Header("Speed")]
    [SerializeField]
    private float animationSpeed = 10f;



    private Vector3 originalScale;



    private Coroutine scaleRoutine;



    private void Awake()
    {
        originalScale =
            transform.localScale;
    }







    public void OnPointerEnter(
        PointerEventData eventData)
    {

        AnimateScale(
            originalScale * hoverScale
        );

    }








    public void OnPointerExit(
        PointerEventData eventData)
    {

        AnimateScale(
            originalScale
        );

    }








    public void OnPointerClick(
        PointerEventData eventData)
    {

        StartCoroutine(
            ClickEffect()
        );

    }








    private IEnumerator ClickEffect()
    {

        transform.localScale =
            originalScale * clickScale;



        yield return new WaitForSeconds(
            0.08f
        );



        AnimateScale(
            originalScale
        );

    }









    private void AnimateScale(
        Vector3 target)
    {

        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);



        scaleRoutine =
            StartCoroutine(
                ScaleRoutine(target)
            );

    }








    private IEnumerator ScaleRoutine(
        Vector3 target)
    {

        while (Vector3.Distance(
            transform.localScale,
            target
        ) > 0.01f)
        {

            transform.localScale =
                Vector3.Lerp(
                    transform.localScale,
                    target,
                    Time.deltaTime *
                    animationSpeed
                );


            yield return null;

        }



        transform.localScale =
            target;

    }

}