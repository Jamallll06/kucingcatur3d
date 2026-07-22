using System.Collections.Generic;
using UnityEngine;


public class BossBarrierAbility : MonoBehaviour
{

    [SerializeField]
    private Barrier barrierPrefab;


    [SerializeField]
    private int barrierCount = 3;


    [SerializeField]
    private int barrierDuration = 3;



    private List<Barrier> activeBarriers =
        new List<Barrier>();



    public void SpawnBarrier()
    {

        ClearBarrier();


        for (int i = 0; i < barrierCount; i++)
        {

            Tile tile =
                GridManager.Instance.GetRandomFreeTile();


            if (tile == null)
                continue;



            Barrier barrier =
                Instantiate(
                    barrierPrefab,
                    tile.transform.position +
                    Vector3.up * 0.5f,
                    Quaternion.identity
                );



            barrier.Initialize(
                tile,
                barrierDuration
            );


            activeBarriers.Add(barrier);

        }


        Debug.Log(
            "Barrier aktif selama "
            + barrierDuration
            + " turn"
        );

    }






    public void ReduceBarrierTurn()
    {

        foreach (Barrier barrier in activeBarriers)
        {
            if (barrier != null)
            {
                barrier.ReduceTurn();
            }
        }


        activeBarriers.RemoveAll(
            x => x == null
        );

    }


    public void SetSetting(
        int count,
        int duration
    )
        {
            barrierCount = count;

            barrierDuration = duration;
        }



    public void ClearBarrier()
    {

        foreach (Barrier barrier in activeBarriers)
        {
            if (barrier != null)
                Destroy(barrier.gameObject);
        }


        activeBarriers.Clear();

    }

}