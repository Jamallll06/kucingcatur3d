using UnityEngine;


public class HUDManager : MonoBehaviour
{

    public static HUDManager Instance { get; private set; }



    [Header("HUD References")]
    [SerializeField]
    private HeroHealthHUD heroHealthHUD;


    [SerializeField]
    private BossHealthHUD bossHealthHUD;


    [SerializeField]
    private TurnHUD turnHUD;


    [SerializeField]
    private FormHUD formHUD;



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





    // =========================
    // NOTIFICATION
    // =========================

    public void ShowMessage(string message)
    {

        if (NotificationHUD.Instance != null)
        {
            NotificationHUD.Instance
                .Show(message);
        }

    }







    // =========================
    // FORM CHANGE
    // =========================

    public void OnFormChanged(
        CatForm form)
    {

        string message =
            form.ToString()
            .ToUpper()
            +
            " FORM!";


        ShowMessage(message);


        Debug.Log(
            "HUD FORM CHANGE : "
            +
            form
        );

    }







    // =========================
    // SKILL
    // =========================

    public void SkillUsed(
        string skillName)
    {

        ShowMessage(
            skillName
            +
            " ACTIVATED!"
        );

    }







    // =========================
    // BOSS WARNING
    // =========================

    public void BossWarning(
        string warning)
    {

        ShowMessage(
            warning
        );

    }







    // =========================
    // VICTORY
    // =========================

    public void Victory()
    {

        ShowMessage(
            "VICTORY!"
        );

    }







    // =========================
    // GAME OVER
    // =========================

    public void GameOver()
    {

        ShowMessage(
            "GAME OVER"
        );

    }


}