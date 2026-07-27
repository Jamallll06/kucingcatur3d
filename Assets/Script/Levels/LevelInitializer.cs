using UnityEngine;


public class LevelInitializer : MonoBehaviour
{

    public static LevelInitializer Instance;



    private void Awake()
    {

        Instance = this;


        ApplyLevelData();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBattleMusic();
        }

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





        // GRID DULU
        SetupGrid(data);



        // BOSS SETELAH GRID
        SetupBoss(data);



        Debug.Log(
            "Level Loaded : "
            + data.levelName
        );

    }








    private void SetupGrid(
        LevelData data)
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

    }








    private void SetupBoss(
        LevelData data)
    {

        BossHealth boss =
            FindFirstObjectByType<BossHealth>();


        if (boss != null)
            boss.SetHealth(
                data.bossHP
            );




        BossAI ai =
            FindFirstObjectByType<BossAI>();


        if (ai != null)
            ai.SetAccuracy(
                data.accuracy
            );





        BossAbilityManager ability =
            FindFirstObjectByType<BossAbilityManager>();


        if (ability != null)
            ability.ApplyLevelData(
                data
            );

    }

}