using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }


    [Header("Level Database")]
    [SerializeField]
    private LevelData[] levels;


    private int currentLevelIndex = 0;



    public LevelData CurrentLevelData
    {
        get
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogError(
                    "LevelManager : LevelData kosong!"
                );

                return null;
            }


            if (currentLevelIndex < 0 ||
                currentLevelIndex >= levels.Length)
            {
                Debug.LogError(
                    "Level index tidak valid : "
                    + currentLevelIndex
                );

                return null;
            }


            return levels[currentLevelIndex];
        }
    }



    public int CurrentLevel =>
        currentLevelIndex + 1;



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

        LoadProgress();
    }



    private void LoadProgress()
    {

        currentLevelIndex =
            PlayerPrefs.GetInt(
                "CURRENT_LEVEL",
                0
            );



        // Anti corrupt save

        if (currentLevelIndex >= levels.Length)
        {
            Debug.LogWarning(
                "Save level corrupt, reset ke Level 1"
            );


            currentLevelIndex = 0;


            SaveProgress();
        }



        Debug.Log(
            "Current Level : "
            + CurrentLevel
        );

    }




    public void StartLevel(int index)
    {
        if (index < 0 ||
           index >= levels.Length)
        {
            Debug.LogWarning(
                "Level tidak tersedia"
            );

            return;
        }


        currentLevelIndex = index;

        SaveProgress();

        LoadLevelScene();
    }





    public void LevelComplete()
    {
        Debug.Log(
            "Level selesai : "
            + CurrentLevelData.levelName
        );


        currentLevelIndex++;


        if (currentLevelIndex >= levels.Length)
        {
            GameComplete();
            return;
        }


        SaveProgress();


        LoadLevelScene();
    }




    private void LoadLevelScene()
    {
        LevelData data =
            CurrentLevelData;


        if (data == null)
            return;



        Debug.Log(
            "Loading Scene : "
            + data.levelNumber
        );


        SceneManager.LoadScene(
            data.levelNumber
        );
    }





    private void SaveProgress()
    {
        PlayerPrefs.SetInt(
            "CURRENT_LEVEL",
            currentLevelIndex
        );


        PlayerPrefs.Save();


        Debug.Log(
            "Progress Saved : "
            + currentLevelIndex
        );
    }





    private void GameComplete()
    {
        Debug.Log(
            "SEMUA LEVEL SELESAI"
        );


        // nanti bisa load ending scene
    }





    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(
            "CURRENT_LEVEL"
        );


        currentLevelIndex = 0;


        Debug.Log(
            "Progress Reset"
        );
    }
}