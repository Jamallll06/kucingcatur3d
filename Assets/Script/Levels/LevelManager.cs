using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Database")]
    [SerializeField] private LevelData[] levels;

    private int currentLevelIndex = 0;

    public int CurrentLevelIndex => currentLevelIndex;

    public int CurrentLevel => currentLevelIndex + 1;

    public LevelData CurrentLevelData
    {
        get
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogError("Level Database kosong.");
                return null;
            }

            currentLevelIndex = Mathf.Clamp(
                currentLevelIndex,
                0,
                levels.Length - 1);

            return levels[currentLevelIndex];
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

        LoadProgress();
    }

    private void LoadProgress()
    {
        if (SaveManager.Instance != null)
        {
            currentLevelIndex =
                SaveManager.Instance.GetCurrentLevel();
        }
        else
        {
            currentLevelIndex =
                PlayerPrefs.GetInt("CURRENT_LEVEL", 0);
        }

        if (levels != null && levels.Length > 0)
        {
            currentLevelIndex = Mathf.Clamp(
                currentLevelIndex,
                0,
                levels.Length - 1);
        }

        Debug.Log($"Current Level : {CurrentLevel}");
    }

    public void StartLevel(int index)
    {
        if (levels == null || levels.Length == 0)
            return;

        if (index < 0 || index >= levels.Length)
            return;

        currentLevelIndex = index;

        SaveProgress();

        SceneManager.LoadScene(
            levels[index].levelNumber);
    }

    public void LevelComplete()
    {
        UnlockNextLevel();

        currentLevelIndex++;

        if (currentLevelIndex >= levels.Length)
        {
            GameComplete();
            return;
        }

        SaveProgress();
    }

    private void UnlockNextLevel()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.UnlockLevel(CurrentLevel + 1);
        }
        else
        {
            int unlocked =
                PlayerPrefs.GetInt("UNLOCK_LEVEL", 1);

            if (CurrentLevel + 1 > unlocked)
            {
                PlayerPrefs.SetInt(
                    "UNLOCK_LEVEL",
                    CurrentLevel + 1);

                PlayerPrefs.Save();
            }
        }
    }

    public void SaveProgress()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveCurrentLevel(
                currentLevelIndex);
        }
        else
        {
            PlayerPrefs.SetInt(
                "CURRENT_LEVEL",
                currentLevelIndex);

            PlayerPrefs.Save();
        }
    }

    public void ResetProgress()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.ResetSave();
        }
        else
        {
            PlayerPrefs.DeleteKey("CURRENT_LEVEL");
            PlayerPrefs.DeleteKey("UNLOCK_LEVEL");
            PlayerPrefs.Save();
        }

        currentLevelIndex = 0;
    }

    private void GameComplete()
    {
        Debug.Log("===== GAME COMPLETE =====");

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}