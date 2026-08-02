using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class FormHUD : MonoBehaviour
{

    [Header("Form UI")]
    [SerializeField]
    private TMP_Text formText;


    [SerializeField]
    private Image formIcon;



    [Header("Form Icons")]
    [SerializeField]
    private Sprite kingIcon;

    [SerializeField]
    private Sprite queenIcon;

    [SerializeField]
    private Sprite rookIcon;

    [SerializeField]
    private Sprite bishopIcon;

    [SerializeField]
    private Sprite knightIcon;

    [SerializeField]
    private Sprite pawnIcon;



    [Header("Animation")]
    [SerializeField]
    private float transformScale = 1.35f;


    [SerializeField]
    private float animationDuration = 0.25f;



    private TransformingPiece transformingPiece;


    private CatForm lastForm;



    private Vector3 startScale;





    private void Start()
    {

        transformingPiece =
            FindFirstObjectByType<TransformingPiece>();


        if (formText != null)
        {
            startScale =
                formText.transform.localScale;
        }


        UpdateForm(true);

    }






    private void Update()
    {

        if (transformingPiece == null)
        {
            transformingPiece =
                FindFirstObjectByType<TransformingPiece>();

            return;
        }


        UpdateForm(false);

    }








    private void UpdateForm(bool instant)
    {

        CatForm currentForm =
            transformingPiece.CurrentForm;



        if (currentForm == lastForm &&
           !instant)
            return;



        lastForm = currentForm;



        switch (currentForm)
        {

            case CatForm.King:

                SetForm(
                    "KING",
                    kingIcon
                );

                break;



            case CatForm.Queen:

                SetForm(
                    "QUEEN",
                    queenIcon
                );

                break;



            case CatForm.Rook:

                SetForm(
                    "ROOK",
                    rookIcon
                );

                break;



            case CatForm.Bishop:

                SetForm(
                    "BISHOP",
                    bishopIcon
                );

                break;



            case CatForm.Knight:

                SetForm(
                    "KNIGHT",
                    knightIcon
                );

                break;



            case CatForm.Pawn:

                SetForm(
                    "PAWN",
                    pawnIcon
                );

                break;

        }



        if (!instant)
        {
            StartCoroutine(
                TransformAnimation()
            );
        }

    }








    private void SetForm(
        string name,
        Sprite icon)
    {

        if (formText != null)
        {
            formText.text =
                name;
        }


        if (formIcon != null)
        {
            formIcon.sprite =
                icon;
        }

    }









    private IEnumerator TransformAnimation()
    {

        if (formText == null)
            yield break;



        formText.transform.localScale =
            startScale *
            transformScale;



        float timer = 0;



        while (timer < animationDuration)
        {

            timer += Time.deltaTime;


            formText.transform.localScale =
                Vector3.Lerp(
                    startScale *
                    transformScale,

                    startScale,

                    timer /
                    animationDuration
                );


            yield return null;

        }



        formText.transform.localScale =
            startScale;

    }

}