using System.Collections;
using UnityEngine;


public enum TurnState
{
    PlayerTurn,
    BossTurn,
    GameOver
}



public class TurnManager : MonoBehaviour
{

    public static TurnManager Instance { get; private set; }



    [Header("Boss Reference")]
    [SerializeField] private BossAI bossAI;



    [Header("Timing")]
    [SerializeField] private float bossTurnDelay = 0.5f;



    public TurnState CurrentTurn { get; private set; }



    public bool IsPlayerTurn
    {
        get
        {
            return CurrentTurn == TurnState.PlayerTurn;
        }
    }





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
        BeginPlayerTurn();
    }








    public void BeginPlayerTurn()
    {

        if (CurrentTurn == TurnState.GameOver)
            return;



        CurrentTurn =
            TurnState.PlayerTurn;



        Debug.Log(
            "TURN : PLAYER"
        );

    }









    public void EndPlayerTurn()
    {

        if (!IsPlayerTurn)
            return;



        StartCoroutine(
            BossTurnRoutine()
        );

    }









    private IEnumerator BossTurnRoutine()
    {

        CurrentTurn =
            TurnState.BossTurn;



        Debug.Log(
            "TURN : BOSS"
        );



        yield return new WaitForSeconds(
            bossTurnDelay
        );





        // =========================
        // BOSS AI ATTACK
        // =========================


        if (bossAI != null &&
           bossAI.gameObject.activeInHierarchy)
        {

            yield return bossAI.ExecuteTurn();

        }

        else
        {

            Debug.Log(
                "Boss AI tidak ditemukan"
            );

        }







        // =========================
        // BOSS ABILITY CHECK
        // =========================


        BossAbilityManager ability =
            FindFirstObjectByType<BossAbilityManager>();



        if (ability != null)
        {

            ability.CheckAbility();

        }








        // =========================
        // UPDATE BARRIER
        // =========================


        BossBarrierAbility barrier =
            FindFirstObjectByType<BossBarrierAbility>();



        if (barrier != null)
        {

            barrier.ReduceBarrierTurn();

        }








        yield return new WaitForSeconds(
            bossTurnDelay
        );



        BeginPlayerTurn();

    }









    public void EndGame()
    {

        CurrentTurn =
            TurnState.GameOver;



        Debug.Log(
            "TURN SYSTEM STOP"
        );

    }









    public bool IsBossTurn()
    {
        return CurrentTurn == TurnState.BossTurn;
    }

}