using UnityEngine;


public class ArenaManager : MonoBehaviour
{

    public static ArenaManager Instance;


    public Tile[] tiles;



    private void Awake()
    {

        Instance = this;

    }



    public void DisableTile(Vector2Int pos)
    {

        foreach (Tile tile in tiles)
        {

            if (tile.GridPosition == pos)
            {

                tile.gameObject
                .SetActive(false);


                Debug.Log(
                "Tile hancur "
                + pos
                );

            }

        }

    }



    public void ActivateDangerTile(
    Vector2Int pos)
    {

        foreach (Tile tile in tiles)
        {

            if (tile.GridPosition == pos)
            {

                Debug.Log(
                "Danger tile aktif "
                + pos
                );

            }

        }

    }

}