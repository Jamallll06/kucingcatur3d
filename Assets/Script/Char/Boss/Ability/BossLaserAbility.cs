using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossLaserAbility : MonoBehaviour
{

    [Header("Laser Setting")]

    [SerializeField]
    private int damage = 2;


    [SerializeField]
    private float warningTime = 1f;



    [Header("Effect")]

    [SerializeField]
    private GameObject laserEffect;



    private BossHealth bossHealth;



    private void Awake()
    {
        bossHealth =
            GetComponent<BossHealth>();
    }




    public void SetSetting(
        float warning,
        int laserDamage
    )
    {

        warningTime = warning;

        damage = laserDamage;


        Debug.Log(
            $"Laser Setting : Damage {damage} Warning {warningTime}"
        );

    }





    public void ExecuteLaser()
    {
        StartCoroutine(
            LaserRoutine()
        );
    }







    private IEnumerator LaserRoutine()
    {

        Debug.Log(
            "BOSS LASER WARNING"
        );



        List<Vector2Int> targetTiles =
            GetLaserTiles();



        // TELEGRAPH MERAH

        if (GridManager.Instance != null)
        {
            GridManager.Instance
                .ShowAttackTelegraph(
                    targetTiles
                );
        }




        yield return new WaitForSeconds(
            warningTime
        );




        DamageTarget(
            targetTiles
        );




        if (GridManager.Instance != null)
        {
            GridManager.Instance
                .ClearHighlights();
        }


    }









    private List<Vector2Int> GetLaserTiles()
    {

        List<Vector2Int> tiles =
            new List<Vector2Int>();



        if (GridManager.Instance == null)
            return tiles;



        Tile bossTile =
            GetBossTile();



        if (bossTile == null)
            return tiles;



        Vector2Int pos =
            bossTile.GridPosition;




        // Laser lurus ke depan

        for (int x = 0;
            x < GridManager.Instance.Width;
            x++)
        {

            Vector2Int target =
                new Vector2Int(
                    x,
                    pos.y
                );



            tiles.Add(target);

        }




        return tiles;

    }








    private Tile GetBossTile()
    {

        float closest =
            Mathf.Infinity;


        Tile result = null;



        foreach (Tile tile in GridManager.Instance.Tiles)
        {

            float distance =
                Vector3.Distance(
                    transform.position,
                    tile.transform.position
                );



            if (distance < closest)
            {
                closest = distance;

                result = tile;
            }

        }



        return result;

    }








    private void DamageTarget(
        List<Vector2Int> tiles
    )
    {

        ChessPiece[] pieces =
            FindObjectsByType<ChessPiece>(
                FindObjectsSortMode.None
            );



        foreach (ChessPiece piece in pieces)
        {

            Tile tile =
                GridManager.Instance
                .GetTile(
                    piece.CurrentPosition
                );



            if (tile == null)
                continue;




            if (tiles.Contains(
                tile.GridPosition))
            {

                piece.TakeDamage(
                    damage
                );


                Debug.Log(
                    "Hero terkena Laser"
                );

            }

        }


        if (laserEffect != null)
        {
            Instantiate(
                laserEffect,
                transform.position,
                Quaternion.identity
            );
        }


    }

}