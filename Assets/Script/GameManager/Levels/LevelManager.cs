using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;


    [Header("Level Database")]
    [SerializeField] private LevelData[] levels;

    

    private int currentLevelIndex = 0;



    public LevelData CurrentLevelData
    {
        get
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogWarning(
                    "LevelManager: Tidak ada LevelData!"
                );

                return null;
            }


            if (currentLevelIndex < 0 ||
                currentLevelIndex >= levels.Length)
            {
                Debug.LogWarning(
                    "LevelManager: Index level tidak valid : "
                    + currentLevelIndex
                );

                return null;
            }


            return levels[currentLevelIndex];
        }
    }



    public int CurrentLevel
    {
        get
        {
            return currentLevelIndex + 1;
        }
    }





    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        DontDestroyOnLoad(gameObject);

    }





    private void Start()
    {

        currentLevelIndex =
            PlayerPrefs.GetInt(
                "CURRENT_LEVEL",
                0
            );



        // Safety jika save rusak
        if (levels == null ||
            levels.Length == 0)
        {
            Debug.LogWarning(
                "LevelManager belum memiliki LevelData"
            );

            return;
        }



        if (currentLevelIndex >= levels.Length)
        {
            currentLevelIndex = 0;
        }

    }





    // Dipanggil saat boss kalah
    public void CompleteLevel()
    {

        if (CurrentLevelData == null)
        {
            Debug.LogWarning(
                "Tidak bisa menyelesaikan level. Data kosong."
            );

            return;
        }



        Debug.Log(
            "LEVEL SELESAI : "
            + CurrentLevelData.levelName
        );



        currentLevelIndex++;



        SaveProgress();



        // Semua level selesai
        if (currentLevelIndex >= levels.Length)
        {

            GameComplete();

            return;

        }



        LoadNextLevel();

    }





    // Kompatibel dengan script lama
    public void LevelComplete()
    {

        CompleteLevel();

    }





    private void LoadNextLevel()
    {

        if (CurrentLevelData == null)
        {
            Debug.LogWarning(
                "Level berikutnya tidak ditemukan"
            );

            return;
        }



        Debug.Log(
            "LOAD LEVEL : "
            + CurrentLevelData.levelName
        );



        SceneManager.LoadScene(
            CurrentLevelData.levelNumber
        );

    }





    public void StartLevel(int index)
    {

        if (levels == null ||
           index < 0 ||
           index >= levels.Length)
        {
            Debug.LogWarning(
                "Level index tidak tersedia"
            );

            return;
        }



        currentLevelIndex = index;


        SaveProgress();


        LoadNextLevel();

    }





    private void SaveProgress()
    {

        PlayerPrefs.SetInt(
            "CURRENT_LEVEL",
            currentLevelIndex
        );


        PlayerPrefs.Save();

    }





    public void ResetProgress()
    {

        PlayerPrefs.DeleteKey(
            "CURRENT_LEVEL"
        );


        currentLevelIndex = 0;


        Debug.Log(
            "Progress level direset"
        );

    }





    private void GameComplete()
    {

        Debug.Log(
            "SEMUA LEVEL SELESAI!"
        );


        // Nanti bisa load Ending Scene

    }

}