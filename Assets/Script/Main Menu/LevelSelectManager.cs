using UnityEngine;
using UnityEngine.UI;



public class LevelSelectManager : MonoBehaviour
{

    public static LevelSelectManager Instance { get; private set; }



    [Header("Level Buttons")]
    [SerializeField]
    private Button[] levelButtons;



    [Header("Lock Icon")]
    [SerializeField]
    private GameObject[] lockIcons;



    [Header("Scene")]
    [SerializeField]
    private string levelPrefix = "Level";



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
        SetupButtons();
    }







    // ======================================
    // SETUP BUTTON
    // ======================================

    private void SetupButtons()
    {

        int unlockedLevel = 1;


        if (SaveManager.Instance != null)
        {
            unlockedLevel =
                SaveManager.Instance
                .GetUnlockedLevel();
        }




        for (int i = 0;
             i < levelButtons.Length;
             i++)
        {

            int levelNumber = i + 1;



            if (levelButtons[i] == null)
            {
                Debug.LogError(
                    $"Level Button {i} belum diassign!"
                );

                continue;
            }



            bool unlocked =
                levelNumber <= unlockedLevel;



            levelButtons[i].interactable =
                unlocked;



            int index = i;



            levelButtons[i]
                .onClick
                .RemoveAllListeners();



            levelButtons[i]
                .onClick
                .AddListener(() =>
                {
                    SelectLevel(index);
                });





            // LOCK ICON

            if (lockIcons != null &&
                i < lockIcons.Length &&
                lockIcons[i] != null)
            {

                lockIcons[i]
                    .SetActive(!unlocked);

            }

        }


    }








    // ======================================
    // SELECT LEVEL
    // ======================================

    public void SelectLevel(int index)
    {

        if (SaveManager.Instance != null)
        {

            int level =
                index + 1;


            if (!SaveManager.Instance
                .IsLevelUnlocked(level))
            {

                Debug.Log(
                    "Level masih terkunci"
                );

                return;
            }

        }




        if (LevelManager.Instance != null)
        {

            LevelManager.Instance
                .StartLevel(index);

        }
        else
        {

            string sceneName =
                levelPrefix
                +
                (index + 1);



            if (SceneLoader.Instance != null)
            {

                SceneLoader.Instance
                    .LoadScene(sceneName);

            }

        }

    }









    // ======================================
    // REFRESH UI
    // ======================================

    public void Refresh()
    {
        SetupButtons();
    }








    // ======================================
    // RESET SAVE
    // ======================================

    public void ResetProgress()
    {

        if (SaveManager.Instance != null)
        {

            SaveManager.Instance
                .ResetSave();

        }


        SetupButtons();


        Debug.Log(
            "Progress berhasil reset"
        );

    }



}