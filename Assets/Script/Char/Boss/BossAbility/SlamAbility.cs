using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SlamAbility : BossAbility
{

    public Transform boss;



    protected override IEnumerator Execute()
    {


        Vector2Int pos =
        GridManager.Instance.WorldToGrid(
        boss.position);



        List<Vector2Int> area =
        new List<Vector2Int>();


        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                area.Add(
                pos + new Vector2Int(x, y));

            }
        }



        GridManager.Instance
        .ShowAttackTelegraph(area);



        yield return new WaitForSeconds(1.5f);



        GridManager.Instance
        .ClearHighlights();



    }

}