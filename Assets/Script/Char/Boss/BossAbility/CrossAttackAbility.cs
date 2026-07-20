using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CrossAttackAbility : BossAbility
{

    public Transform boss;



    protected override IEnumerator Execute()
    {

        Vector2Int pos =
        GridManager.Instance.WorldToGrid(
        boss.position);



        List<Vector2Int> area =
        new List<Vector2Int>();


        area.Add(pos + Vector2Int.up);
        area.Add(pos + Vector2Int.down);
        area.Add(pos + Vector2Int.left);
        area.Add(pos + Vector2Int.right);



        GridManager.Instance
        .ShowAttackTelegraph(area);



        yield return new WaitForSeconds(1.5f);



        GridManager.Instance
        .ClearHighlights();


    }

}