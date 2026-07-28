using UnityEngine;


public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { get; private set; }



    private const string CURRENT_LEVEL = "CURRENT_LEVEL";
    private const string UNLOCK_LEVEL = "UNLOCK_LEVEL";

    private const string BGM_VOLUME = "BGM_VOLUME";
    private const string SFX_VOLUME = "SFX_VOLUME";





    private void Awake()
    {

        if (Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        DontDestroyOnLoad(gameObject);

    }






    // =========================
    // LEVEL
    // =========================


    public void SaveCurrentLevel(int level)
    {

        PlayerPrefs.SetInt(
            CURRENT_LEVEL,
            level
        );

        PlayerPrefs.Save();

    }



    public int GetCurrentLevel()
    {

        return PlayerPrefs.GetInt(
            CURRENT_LEVEL,
            0
        );

    }







    public void UnlockLevel(int level)
    {

        int current =
            GetUnlockedLevel();



        if (level > current)
        {

            PlayerPrefs.SetInt(
                UNLOCK_LEVEL,
                level
            );


            PlayerPrefs.Save();

        }

    }






    public int GetUnlockedLevel()
    {

        return PlayerPrefs.GetInt(
            UNLOCK_LEVEL,
            1
        );

    }






    public bool IsLevelUnlocked(int level)
    {

        return level <= GetUnlockedLevel();

    }








    // =========================
    // AUDIO SAVE
    // =========================


    public void SaveBGMVolume(float value)
    {

        PlayerPrefs.SetFloat(
            BGM_VOLUME,
            value
        );


        PlayerPrefs.Save();


        Debug.Log(
            "Save BGM : "
            + value
        );

    }






    public float GetBGMVolume()
    {

        return PlayerPrefs.GetFloat(
            BGM_VOLUME,
            1f
        );

    }






    public void SaveSFXVolume(float value)
    {

        PlayerPrefs.SetFloat(
            SFX_VOLUME,
            value
        );


        PlayerPrefs.Save();


        Debug.Log(
            "Save SFX : "
            + value
        );

    }






    public float GetSFXVolume()
    {

        return PlayerPrefs.GetFloat(
            SFX_VOLUME,
            1f
        );

    }








    // =========================
    // RESET
    // =========================


    public void ResetSave()
    {

        PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();


        Debug.Log(
            "SAVE RESET"
        );

    }

    // =========================
    // RESET PROGRESS ONLY
    // =========================

    public void ResetProgress()
    {

        PlayerPrefs.DeleteKey(
            CURRENT_LEVEL
        );


        PlayerPrefs.DeleteKey(
            UNLOCK_LEVEL
        );


        PlayerPrefs.Save();


        Debug.Log(
            "LEVEL PROGRESS RESET"
        );

    }

}