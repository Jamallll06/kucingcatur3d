using UnityEngine;


public class LevelSave : MonoBehaviour
{


    public static void SaveLevel(int level)
    {

        PlayerPrefs.SetInt(
            "CURRENT_LEVEL",
            level
        );


        PlayerPrefs.Save();

    }



    public static int LoadLevel()
    {

        return PlayerPrefs.GetInt(
            "CURRENT_LEVEL",
            0
        );

    }


}