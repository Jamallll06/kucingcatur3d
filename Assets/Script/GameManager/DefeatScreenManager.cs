using UnityEngine;
using TMPro;



public class DefeatScreenManager : MonoBehaviour
{

    public static DefeatScreenManager Instance { get; private set; }



    [Header("UI")]
    [SerializeField]
    private GameObject defeatPanel;


    [SerializeField]
    private TMP_Text defeatText;



    [Header("Animation")]
    [SerializeField]
    private float delayBeforeShow = 1f;



    private bool isShown;





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

        if (defeatPanel != null)
            defeatPanel.SetActive(false);

    }









    public void ShowDefeat()
    {

        if (isShown)
            return;



        isShown = true;



        Invoke(
            nameof(OpenPanel),
            delayBeforeShow
        );

    }









    private void OpenPanel()
    {

        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }



        if (defeatText != null)
        {
            defeatText.text =
                "GAME OVER";
        }



        Debug.Log(
            "Defeat Screen Aktif"
        );

    }









    public void Retry()
    {

        Debug.Log(
            "Restart Level"
        );



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    UnityEngine
                    .SceneManagement
                    .SceneManager
                    .GetActiveScene()
                    .name
                );

        }

    }









    public void ReturnToMenu()
    {

        Debug.Log(
            "Return Main Menu"
        );



        if (SceneLoader.Instance != null)
        {

            SceneLoader.Instance
                .LoadScene(
                    "MainMenu"
                );

        }

    }

}