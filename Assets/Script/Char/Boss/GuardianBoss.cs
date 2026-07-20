using UnityEngine;
using System.Collections;


public class GuardianBoss : BossBase
{

    public BossChessAI chessAI;


    public SlamAbility slam;


    public CrossAttackAbility crossAttack;



    public IEnumerator BossTurn()
    {


        yield return StartCoroutine(
        chessAI.TakeTurn()
        );


        float distance =
        Vector3.Distance(
        transform.position,
        chessAI.hero.position);



        if (distance <= 2)
        {

            StartCoroutine(
            slam.UseAbility());

        }

        else
        {

            StartCoroutine(
            crossAttack.UseAbility());

        }


    }

}