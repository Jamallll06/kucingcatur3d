using UnityEngine;


public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { get; private set; }



    // ==========================
    // SAVE KEY
    // ==========================

    private const string UNLOCK_LEVEL =
        "UNLOCK_LEVEL";


    private const string CURRENT_LEVEL =
        "CURRENT_LEVEL";


    private const string BGM_VOLUME =
        "BGM_VOLUME";


    private const string SFX_VOLUME =
        "SFX_VOLUME";





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







    // ==========================
    // LEVEL SYSTEM
    // ==========================


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


            Debug.Log(
                "Unlock Level : "
                + level
            );

        }

    }








    public int GetUnlockedLevel()
    {

        return PlayerPrefs.GetInt(
            UNLOCK_LEVEL,
            1
        );

    }








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
            1
        );

    }









    // ==========================
    // AUDIO SYSTEM
    // ==========================


    public void SaveBGMVolume(float value)
    {

        PlayerPrefs.SetFloat(
            BGM_VOLUME,
            value
        );


        PlayerPrefs.Save();

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

    }








    public float GetSFXVolume()
    {

        return PlayerPrefs.GetFloat(
            SFX_VOLUME,
            1f
        );

    }








    // ==========================
    // RESET DATA
    // ==========================


    public void ResetProgress()
    {

        PlayerPrefs.DeleteKey(
            UNLOCK_LEVEL
        );


        PlayerPrefs.DeleteKey(
            CURRENT_LEVEL
        );


        PlayerPrefs.Save();



        Debug.Log(
            "Progress berhasil direset"
        );

    }








    public void ResetAllData()
    {

        PlayerPrefs.DeleteAll();


        PlayerPrefs.Save();



        Debug.Log(
            "Semua data dihapus"
        );

    }

}