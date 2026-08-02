using UnityEngine;
using TMPro;
using System.Collections;


public class TurnHUD : MonoBehaviour
{

    [Header("Turn UI")]
    [SerializeField]
    private TMP_Text turnText;



    [Header("Animation")]
    [SerializeField]
    private float popScale = 1.3f;


    [SerializeField]
    private float popDuration = 0.2f;



    private string lastTurn = "";

    private Vector3 startScale;



    private void Start()
    {

        if (turnText != null)
        {
            startScale =
                turnText.transform.localScale;
        }


        UpdateTurn();

    }







    private void Update()
    {

        UpdateTurn();

    }









    private void UpdateTurn()
    {

        if (TurnManager.Instance == null)
            return;



        string currentTurn;


        if (TurnManager.Instance.IsPlayerTurn)
        {
            currentTurn =
                "PLAYER TURN";
        }
        else
        {
            currentTurn =
                "BOSS TURN";
        }





        if (currentTurn != lastTurn)
        {

            lastTurn = currentTurn;


            if (turnText != null)
            {
                turnText.text =
                    currentTurn;
            }


            StartCoroutine(
                TurnPop()
            );

        }

    }









    private IEnumerator TurnPop()
    {

        if (turnText == null)
            yield break;



        turnText.transform.localScale =
            startScale *
            popScale;



        float timer = 0;



        while (timer < popDuration)
        {

            timer += Time.deltaTime;


            turnText.transform.localScale =
                Vector3.Lerp(
                    startScale * popScale,
                    startScale,
                    timer / popDuration
                );


            yield return null;

        }



        turnText.transform.localScale =
            startScale;

    }

}