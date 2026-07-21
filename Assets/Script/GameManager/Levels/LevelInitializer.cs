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
            "Load Level : "
            + data.levelName
        );



        SetupBoss(data);


        SetupGrid(data);

    }




    private void SetupBoss(LevelData data)
    {

        BossHealth boss =
            FindFirstObjectByType<BossHealth>();


        if (boss == null)
            return;



        Debug.Log(
            "Boss : "
            + data.bossName
        );



        // nanti kita tambah setter HP

    }




    private void SetupGrid(LevelData data)
    {

        GridManager grid =
            FindFirstObjectByType<GridManager>();


        if (grid == null)
            return;



        Debug.Log(
            "Grid : "
            + data.gridWidth +
            " x " +
            data.gridHeight
        );

    }

}