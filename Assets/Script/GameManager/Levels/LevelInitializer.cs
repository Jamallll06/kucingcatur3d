using UnityEngine;


public class LevelInitializer : MonoBehaviour
{

    [SerializeField]
    private LevelData levelData;



    private void Awake()
    {
        ApplyLevelData();
    }





    private void ApplyLevelData()
    {

        if(levelData == null)
        {
            Debug.LogError(
                "LevelData belum diisi!"
            );

            return;
        }



        Debug.Log(
            "Load Level : "
            + levelData.levelName
        );



        SetupGrid();


        SetupBoss();


        SetupAbility();

    }







    private void SetupGrid()
    {

        GridManager grid =
            FindFirstObjectByType<GridManager>();


        if(grid == null)
        {
            Debug.LogError(
                "GridManager tidak ditemukan"
            );

            return;
        }



        grid.InitializeGrid(
            levelData.gridWidth,
            levelData.gridHeight
        );



        Debug.Log(
            "Grid : "
            + levelData.gridWidth
            + " x "
            + levelData.gridHeight
        );

    }









    private void SetupBoss()
    {

        BossHealth boss =
            FindFirstObjectByType<BossHealth>();


        if(boss != null)
        {

            boss.SetHealth(
                levelData.bossHP
            );


            Debug.Log(
                "Boss HP : "
                + levelData.bossHP
            );

        }



        BossAI ai =
            FindFirstObjectByType<BossAI>();


        if(ai != null)
        {

            ai.SetAccuracy(
                levelData.accuracy
            );

        }

    }








    private void SetupAbility()
    {

        BossAbilityManager ability =
            FindFirstObjectByType<BossAbilityManager>();



        if(ability != null)
        {

            ability.ApplyLevelData(
                levelData
            );

        }

    }

}