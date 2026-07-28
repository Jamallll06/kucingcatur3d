using UnityEngine;


public class LevelInitializer : MonoBehaviour
{
    public static LevelInitializer Instance { get; private set; }


    private bool initialized;



    private void Awake()
    {
        if (Instance != null &&
            Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
    }



    private void Start()
    {
        InitializeLevel();
    }





    public void InitializeLevel()
    {
        if (initialized)
            return;



        if (LevelManager.Instance == null)
        {
            Debug.LogError(
                "LevelInitializer : LevelManager tidak ditemukan!"
            );

            return;
        }



        LevelData data =
            LevelManager.Instance.CurrentLevelData;



        if (data == null)
        {
            Debug.LogError(
                "LevelInitializer : LevelData kosong!"
            );

            return;
        }



        Debug.Log(
            "Loading Level : "
            + data.levelName
        );



        SetupGrid(data);


        SetupBoss(data);


        initialized = true;



        Debug.Log(
            "Level berhasil diinisialisasi"
        );
    }







    // ============================
    // GRID SETUP
    // ============================

    private void SetupGrid(LevelData data)
    {

        GridManager grid =
            FindFirstObjectByType<GridManager>();


        if (grid == null)
        {
            Debug.LogError(
                "GridManager tidak ditemukan!"
            );

            return;
        }



        grid.InitializeGrid(
            data.gridWidth,
            data.gridHeight
        );



        Debug.Log(
            $"Grid Setup {data.gridWidth}x{data.gridHeight}"
        );

    }








    // ============================
    // BOSS SETUP
    // ============================

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

}