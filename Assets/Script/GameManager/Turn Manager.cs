using System.Collections;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;


    public bool IsPlayerTurn { get; private set; }


    [SerializeField]
    private BossBase currentBoss;


    private bool isChangingTurn;


    private void Awake()
    {
        Instance = this;
    }



    private void Start()
    {
        IsPlayerTurn = true;

        Debug.Log(
        "Guardian Boss Aktif"
        );
    }



    public void EndPlayerTurn()
    {
        StartBossTurn();
    }



    public void StartBossTurn()
    {
        StartCoroutine(BossTurnRoutine());
    }



    private IEnumerator BossTurnRoutine()
    {

        Debug.Log("MASUK BOSS TURN ROUTINE");


        IsPlayerTurn = false;


        Debug.Log("BOSS TURN");



        yield return new WaitForSeconds(1);



        if (currentBoss != null)
        {

            Debug.Log(
                "Boss ditemukan: "
                + currentBoss.name
                +
                " Type: "
                +
                currentBoss.GetType()
             );


            GuardianBoss boss =
            currentBoss as GuardianBoss;



            if (boss != null)
            {

                Debug.Log(
                "Memanggil Guardian Boss"
                );


                yield return StartCoroutine(
                    boss.BossTurn()
                );

            }

            else
            {
                Debug.LogError(
                "Current Boss bukan GuardianBoss"
                );
            }

        }

        else
        {
            Debug.LogError(
            "Current Boss kosong!"
            );
        }



        IsPlayerTurn = true;

    }



    public void SetBoss(BossBase boss)
    {
        currentBoss = boss;
    }



    public void EndGame()
    {

        StopAllCoroutines();

        IsPlayerTurn = false;


        Debug.Log(
            "GAME END"
        );

    }



    public void Victory()
    {

        StopAllCoroutines();

        IsPlayerTurn = false;


        Debug.Log(
            "PLAYER WIN"
        );

    }

}