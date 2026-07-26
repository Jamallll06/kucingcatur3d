using System.Collections.Generic;
using UnityEngine;


public class BossSummonAbility : MonoBehaviour
{

    [Header("Summon Setting")]

    [SerializeField]
    private GameObject minionPrefab;


    [SerializeField]
    private int summonCount = 2;


    [SerializeField]
    private int maxMinion = 4;



    [Header("Spawn")]
    [SerializeField]
    private float spawnHeight = 0.5f;



    private List<GameObject> activeMinions =
        new List<GameObject>();






    public void SetSetting(
        int count,
        int max
    )
    {

        summonCount = count;

        maxMinion = max;


        Debug.Log(
            $"Summon Setting : {count} / Max {max}"
        );

    }








    public void Summon()
    {

        if (GridManager.Instance == null)
        {
            Debug.LogWarning(
                "GridManager tidak ditemukan"
            );

            return;
        }



        RemoveDeadMinions();



        int availableSlot =
            maxMinion -
            activeMinions.Count;



        if (availableSlot <= 0)
        {

            Debug.Log(
                "Jumlah minion sudah maksimal"
            );

            return;

        }





        int spawnAmount =
            Mathf.Min(
                summonCount,
                availableSlot
            );







        for (int i = 0;
            i < spawnAmount;
            i++)
        {


            Tile spawnTile =
                GridManager.Instance
                .GetRandomFreeTile();



            if (spawnTile == null)
            {

                Debug.Log(
                    "Tidak ada tile kosong"
                );

                break;

            }






            SpawnMinion(
                spawnTile
            );


        }

    }









    private void SpawnMinion(
        Tile tile
    )
    {


        if (minionPrefab == null)
        {

            Debug.LogWarning(
                "Minion prefab belum dipasang"
            );

            return;

        }





        Vector3 position =
            tile.transform.position +
            Vector3.up * spawnHeight;





        GameObject minion =
            Instantiate(
                minionPrefab,
                position,
                Quaternion.identity
            );






        activeMinions.Add(
            minion
        );



        tile.IsOccupied = true;




        MinionAI ai =
            minion.GetComponent<MinionAI>();



        if (ai != null)
        {

            ai.SetGridPosition(
                tile.GridPosition
            );

        }




        Debug.Log(
            "Minion Spawn : "
            + tile.GridPosition
        );


    }









    private void RemoveDeadMinions()
    {

        activeMinions.RemoveAll(
            minion => minion == null
        );

    }








    public int GetActiveMinionCount()
    {

        RemoveDeadMinions();


        return activeMinions.Count;

    }


}