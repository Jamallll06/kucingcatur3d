using UnityEngine;
using UnityEngine.UI;



public class LevelSelectManager : MonoBehaviour
{

    public static LevelSelectManager Instance { get; private set; }



    [Header("Level Buttons")]
    [SerializeField]
    private Button[] levelButtons;



    [Header("Locked UI")]
    [SerializeField]
    private GameObject[] lockIcons;



    [Header("Scene")]
    [SerializeField]
    private string levelScenePrefix = "Level";



    private int unlockedLevel;







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

        LoadUnlockedLevel();


        SetupButtons();

    }









    private void LoadUnlockedLevel()
    {

        /*
         PlayerPrefs:
         
         1 = Level 1 terbuka
         2 = Level 2 terbuka
         3 = Level 3 terbuka
         dst
        */


        unlockedLevel =
            PlayerPrefs.GetInt(
                "UNLOCK_LEVEL",
                1
            );


        Debug.Log(
            "Unlocked Level : "
            + unlockedLevel
        );

    }









    private void SetupButtons()
    {

        for (int i = 0;
            i < levelButtons.Length;
            i++)
        {


            int levelNumber =
                i + 1;



            bool unlocked =
                levelNumber <= unlockedLevel;



            levelButtons[i]
                .interactable =
                unlocked;



            if (lockIcons != null &&
               i < lockIcons.Length)
            {

                lockIcons[i]
                    .SetActive(
                        !unlocked
                    );

            }



            if (unlocked)
            {

                levelButtons[i]
                    .onClick
                    .RemoveAllListeners();



                levelButtons[i]
                    .onClick
                    .AddListener(
                        () =>
                        {
                            PlayLevel(
                                levelNumber
                            );
                        }
                    );

            }

        }

    }









    public void PlayLevel(
        int levelNumber
    )
    {

        Debug.Log(
            "Start Level "
            + levelNumber
        );



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    levelScenePrefix
                    +
                    levelNumber
                );

        }

    }









    public void Back()
    {

        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    "MainMenu"
                );

        }

    }

}