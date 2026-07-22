using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Tile Tile { get; private set; }

    private int remainingTurn;


    public void Initialize(Tile tile, int duration)
    {
        Tile = tile;

        remainingTurn = duration;

        tile.SetBlocked(true);
    }


    public void ReduceTurn()
    {
        remainingTurn--;


        Debug.Log(
            "Barrier sisa turn : "
            + remainingTurn
        );


        if (remainingTurn <= 0)
        {
            DestroyBarrier();
        }
    }



    private void DestroyBarrier()
    {
        if (Tile != null)
        {
            Tile.SetBlocked(false);
        }


        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        if (Tile != null)
        {
            Tile.SetBlocked(false);
        }
    }
}