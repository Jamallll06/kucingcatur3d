using UnityEngine;
using System.Collections;


public class BossChessAI : MonoBehaviour
{

    public Transform hero;



    public IEnumerator TakeTurn()
    {

        Vector2Int bossPos =
        GridManager.Instance.WorldToGrid(
        transform.position);



        Vector2Int heroPos =
        GridManager.Instance.WorldToGrid(
        hero.position);



        Vector2Int target =
        CalculateMove(
        bossPos,
        heroPos);



        yield return Move(target);

    }



    Vector2Int CalculateMove(
    Vector2Int boss,
    Vector2Int hero)
    {

        Vector2Int move = Vector2Int.zero;


        if (Mathf.Abs(hero.x - boss.x)
        >
        Mathf.Abs(hero.y - boss.y))
        {

            move.x =
            hero.x > boss.x ? 1 : -1;

        }

        else
        {

            move.y =
            hero.y > boss.y ? 1 : -1;

        }


        return boss + move;

    }



    IEnumerator Move(Vector2Int target)
    {

        Vector3 destination =
        GridManager.Instance
        .GridToWorld(target);



        while (Vector3.Distance(
        transform.position,
        destination) > 0.05f)
        {

            transform.position =
            Vector3.MoveTowards(
            transform.position,
            destination,
            5 * Time.deltaTime);


            yield return null;

        }


        transform.position = destination;

    }

}