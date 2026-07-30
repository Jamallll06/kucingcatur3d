using UnityEngine;
using TMPro;


public class LevelHUD : MonoBehaviour
{

    [Header("Level UI")]
    [SerializeField]
    private TMP_Text levelText;


    [SerializeField]
    private TMP_Text levelNameText;



    private int lastLevel = -1;



    private void Start()
    {
        UpdateLevel();
    }



    private void Update()
    {
        UpdateLevel();
    }






    private void UpdateLevel()
    {

        if (LevelManager.Instance == null)
            return;



        int currentLevel =
            LevelManager.Instance.CurrentLevel;



        if (currentLevel != lastLevel)
        {

            lastLevel = currentLevel;


            if (levelText != null)
            {
                levelText.text =
                    "LEVEL "
                    +
                    currentLevel;
            }



            if (levelNameText != null &&
               LevelManager.Instance.CurrentLevelData != null)
            {

                levelNameText.text =
                    LevelManager.Instance
                    .CurrentLevelData
                    .levelName;

            }


            Debug.Log(
                "Level HUD Update : "
                +
                currentLevel
            );

        }

    }

}