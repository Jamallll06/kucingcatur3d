using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBarrierAbility : MonoBehaviour
{

    [Header("Barrier Setting")]

    [SerializeField]
    private GameObject barrierPrefab;


    [SerializeField]
    private int barrierCount = 3;


    [SerializeField]
    private int barrierDuration = 3;



    private readonly List<GameObject> activeBarriers =
        new List<GameObject>();





    public void SetSetting(
        int count,
        int duration
    )
    {
        barrierCount = count;
        barrierDuration = duration;


        Debug.Log(
            $"Barrier Setting : {count} | {duration}"
        );
    }







    public void SpawnBarrier()
    {

        if (GridManager.Instance == null)
        {
            Debug.LogWarning(
                "GridManager tidak ditemukan"
            );

            return;
        }



        ClearOldBarrier();



        for (int i = 0;
            i < barrierCount;
            i++)
        {

            Tile tile =
                GridManager.Instance
                .GetRandomFreeTile();



            if (tile == null)
                break;



            CreateBarrier(tile);

        }


    }









    private void CreateBarrier(Tile tile)
    {

        if (barrierPrefab == null)
        {
            Debug.LogWarning(
                "Barrier Prefab belum dipasang"
            );

            return;
        }



        GameObject barrier =
            Instantiate(
                barrierPrefab,
                tile.transform.position,
                Quaternion.identity
            );



        activeBarriers.Add(
            barrier
        );



        Barrier barrierScript =
            barrier.GetComponent<Barrier>();



        if (barrierScript != null)
        {

            barrierScript.Initialize(
                tile,
                barrierDuration
            );

        }



        Debug.Log(
            "Barrier dibuat di "
            + tile.GridPosition
        );

    }









    private void ClearOldBarrier()
    {

        foreach (GameObject barrier in activeBarriers)
        {

            if (barrier != null)
            {
                Destroy(barrier);
            }

        }


        activeBarriers.Clear();

    }

}