using UnityEngine;


public class LevelInitializer : MonoBehaviour
{
    public static LevelInitializer Instance;


    private void Awake()
    {
        Instance = this;
    }



    private void Start()
    {
        ApplyLevelData();
    }




    private void ApplyLevelData()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError(
                "LevelManager tidak ditemukan"
            );

            return;
        }



        LevelData data =
            LevelManager.Instance.CurrentLevelData;



        if (data == null)
        {
            Debug.LogError(
                "LevelData kosong"
            );

            return;
        }



        Debug.Log(
            "Memulai : "
            + data.levelName
        );



        SetupBoss(data);


        SetupGrid(data);
    }






    private void SetupBoss(LevelData data)
    {
        BossHealth boss =
            FindFirstObjectByType<BossHealth>();


        if (boss != null)
        {
            boss.SetHealth(
                data.bossHP
            );


            Debug.Log(
                "Boss HP : "
                + data.bossHP
            );
        }





        BossAI ai =
            FindFirstObjectByType<BossAI>();


        if (ai != null)
        {
            ai.SetAccuracy(
                data.accuracy
            );


            Debug.Log(
                "Boss Accuracy : "
                + data.accuracy
            );
        }





        BossAbilityManager ability =
            FindFirstObjectByType<BossAbilityManager>();


        if (ability != null)
        {
            ability.ApplyLevelData(
                data
            );


            Debug.Log(
                "Boss Ability Loaded"
            );
        }
    }







    private void SetupGrid(LevelData data)
    {

        GridManager grid =
            FindFirstObjectByType<GridManager>();


        if (grid == null)
        {
            Debug.LogError(
                "GridManager tidak ditemukan"
            );

            return;
        }



        grid.InitializeGrid(
            data.gridWidth,
            data.gridHeight
        );


        Debug.Log(
            "Grid Setup : "
            + data.gridWidth
            + " x "
            + data.gridHeight
        );

    }

}