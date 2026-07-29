using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroHUD : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private Slider heroHpSlider;
    [SerializeField] private TMP_Text heroHpText;

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

    [Header("Boss")]
    [SerializeField] private Slider bossHpSlider;
    [SerializeField] private TMP_Text bossHpText;
    [SerializeField] private TMP_Text bossNameText;

    [Header("Gameplay")]
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text formText;

    [Header("Smooth")]
    [SerializeField] private float smoothSpeed = 8f;

    [Header("Skill HUD")]
    [SerializeField] private Image formIcon;

    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillCostText;
    [SerializeField] private TMP_Text skillDescriptionText;

    [Header("Form Icons")]
    [SerializeField] private Sprite kingIcon;
    [SerializeField] private Sprite queenIcon;
    [SerializeField] private Sprite rookIcon;
    [SerializeField] private Sprite bishopIcon;
    [SerializeField] private Sprite knightIcon;
    [SerializeField] private Sprite pawnIcon;

    [Header("Notification")]

    [SerializeField]
    private CanvasGroup notificationGroup;
    [SerializeField]
    private TMP_Text notificationText;
    [SerializeField]
    private float notificationDuration = 1.5f;
    [SerializeField]
    private float notificationScale = 1.2f;

    [Header("Animation")]

    [SerializeField]
    private RectTransform heroHpObject;
    [SerializeField]
    private RectTransform bossHpObject;
    [SerializeField]
    private TMP_Text turnTextObject;
    [SerializeField]
    private float shakeAmount = 10f;
    [SerializeField]
    private float shakeDuration = 0.2f;


    private Vector3 heroHpStartPos;
    private Vector3 bossHpStartPos;

    private ChessPiece hero;
    private BossHealth boss;
    private TransformingPiece transformingPiece;
    private Vector3 notificationStartScale;

    private void Start()
    {
        hero = FindFirstObjectByType<ChessPiece>();

        boss = FindFirstObjectByType<BossHealth>();

        transformingPiece =
            FindFirstObjectByType<TransformingPiece>();


        if (heroHpObject != null)
            heroHpStartPos =
                heroHpObject.localPosition;


        if (bossHpObject != null)
            bossHpStartPos =
                bossHpObject.localPosition;


        notificationGroup.alpha = 0;


        RefreshInstant();
    }

    private void Update()
    {
        UpdateHeroHP();

        UpdateEnergy();

        UpdateBossHP();

        UpdateTurn();

        UpdateLevel();

        UpdateForm();

        UpdateSkillHUD();
    }

    private void UpdateHeroHP()
    {
        if (hero == null)
            return;

        float target =
            (float)hero.CurrentHealth /
            hero.MaxHealth;

        heroHpSlider.value =
            Mathf.Lerp(
                heroHpSlider.value,
                target,
                Time.deltaTime * smoothSpeed);

        heroHpText.text =
            hero.CurrentHealth +
            " / " +
            hero.MaxHealth;
    }

    private void UpdateEnergy()
    {
        if (EnergyManager.Instance == null)
            return;

        float target =
            (float)EnergyManager.Instance.CurrentEnergy /
            EnergyManager.Instance.MaxEnergy;

        energySlider.value =
            Mathf.Lerp(
                energySlider.value,
                target,
                Time.deltaTime * smoothSpeed);

        energyText.text =
            EnergyManager.Instance.CurrentEnergy +
            " / " +
            EnergyManager.Instance.MaxEnergy;
    }

    private void UpdateBossHP()
    {
        if (boss == null)
            return;

        float target =
            (float)boss.CurrentHealth /
            boss.MaxHealth;

        bossHpSlider.value =
            Mathf.Lerp(
                bossHpSlider.value,
                target,
                Time.deltaTime * smoothSpeed);

        bossHpText.text =
            boss.CurrentHealth +
            " / " +
            boss.MaxHealth;

        bossNameText.text =
            boss.gameObject.name;
    }

    private void UpdateTurn()
    {
        if (TurnManager.Instance == null)
            return;

        turnText.text =
            TurnManager.Instance.IsPlayerTurn
            ? "PLAYER TURN"
            : "BOSS TURN";
    }

    private void UpdateLevel()
    {
        if (LevelManager.Instance == null)
            return;

        levelText.text =
            "LEVEL " +
            LevelManager.Instance.CurrentLevel;
    }

    private void UpdateForm()
    {
        if (transformingPiece == null)
            return;

        formText.text =
            transformingPiece.CurrentForm
            .ToString()
            .ToUpper();
    }

    private void UpdateSkillHUD()
    {
        if (transformingPiece == null)
            return;

        switch (transformingPiece.CurrentForm)
        {
            case CatForm.King:

                formIcon.sprite = kingIcon;

                skillNameText.text = "King Shield";

                skillCostText.text = "Cost : ⚡⚡";

                skillDescriptionText.text =
                    "Blokir 1 serangan Boss.";

                break;

            case CatForm.Queen:

                formIcon.sprite = queenIcon;

                skillNameText.text = "Queen Laser";

                skillCostText.text = "Cost : ⚡⚡⚡";

                skillDescriptionText.text =
                    "Laser lurus dengan damage tinggi.";

                break;

            case CatForm.Rook:

                formIcon.sprite = rookIcon;

                skillNameText.text = "Iron Armor";

                skillCostText.text = "Cost : ⚡⚡";

                skillDescriptionText.text =
                    "Mengurangi damage yang diterima.";

                break;

            case CatForm.Bishop:

                formIcon.sprite = bishopIcon;

                skillNameText.text = "Blink";

                skillCostText.text = "Cost : ⚡⚡";

                skillDescriptionText.text =
                    "Teleport ke tile tujuan.";

                break;

            case CatForm.Knight:

                formIcon.sprite = knightIcon;

                skillNameText.text = "Pounce";

                skillCostText.text = "Cost : ⚡⚡⚡";

                skillDescriptionText.text =
                    "Lompatan dengan damage besar.";

                break;

            case CatForm.Pawn:

                formIcon.sprite = pawnIcon;

                skillNameText.text = "Heal";

                skillCostText.text = "Cost : ⚡";

                skillDescriptionText.text =
                    "Memulihkan sebagian HP.";

                break;
        }
    }

    private IEnumerator ShowNotificationRoutine(
    string message)
    {

        notificationText.text = message;


        notificationText.transform.localScale =
            notificationStartScale *
            notificationScale;


        notificationGroup.alpha = 0;


        float timer = 0;


        while (timer < 0.2f)
        {
            timer += Time.deltaTime;


            notificationGroup.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    timer / 0.2f);


            notificationText.transform.localScale =
                Vector3.Lerp(
                    notificationText.transform.localScale,
                    notificationStartScale,
                    timer / 0.2f);


            yield return null;
        }



        yield return new WaitForSeconds(
            notificationDuration);



        timer = 0;


        while (timer < 0.3f)
        {
            timer += Time.deltaTime;


            notificationGroup.alpha =
                Mathf.Lerp(
                    1,
                    0,
                    timer / 0.3f);


            yield return null;
        }


        notificationGroup.alpha = 0;

    }

    public void ShakeHeroHP()
    {
        if (heroHpObject != null)
            StartCoroutine(
                ShakeRoutine(
                    heroHpObject,
                    heroHpStartPos));
    }

    public void ShakeBossHP()
    {
        if (bossHpObject != null)
            StartCoroutine(
                ShakeRoutine(
                    bossHpObject,
                    bossHpStartPos));
    }

    private IEnumerator ShakeRoutine(
    RectTransform target,
    Vector3 startPosition)
    {

        float timer = 0;


        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;


            float x =
                Random.Range(
                    -shakeAmount,
                    shakeAmount);


            target.localPosition =
                startPosition +
                new Vector3(x, 0, 0);


            yield return null;
        }


        target.localPosition =
            startPosition;
    }


    public void ShowNotification(string text)
    {
        StopAllCoroutines();

        StartCoroutine(
            ShowNotificationRoutine(text));
    }

    public void AnimateTurn()
    {
        StartCoroutine(
            TurnPop());
    }

    private IEnumerator TurnPop()
    {

        Vector3 start =
            turnTextObject.transform.localScale;


        turnTextObject.transform.localScale =
            start * 1.3f;


        float timer = 0;


        while (timer < 0.2f)
        {
            timer += Time.deltaTime;


            turnTextObject.transform.localScale =
                Vector3.Lerp(
                    start * 1.3f,
                    start,
                    timer / 0.2f);


            yield return null;
        }
    }

    public void EnergyFullEffect()
    {
        StartCoroutine(
            EnergyPulse());
    }

    private IEnumerator EnergyPulse()
    {

        Vector3 start =
            energySlider.transform.localScale;


        energySlider.transform.localScale =
            start * 1.15f;


        yield return new WaitForSeconds(0.15f);


        energySlider.transform.localScale =
            start;
    }

    public void FormChangeEffect()
    {
        StartCoroutine(
            FormPop());
    }

    private IEnumerator FormPop()
    {

        Vector3 start =
            formText.transform.localScale;


        formText.transform.localScale =
            start * 1.4f;


        float timer = 0;


        while (timer < 0.25f)
        {
            timer += Time.deltaTime;


            formText.transform.localScale =
                Vector3.Lerp(
                    start * 1.4f,
                    start,
                    timer / 0.25f);


            yield return null;
        }
    }

    private void RefreshInstant()
    {
        UpdateHeroHP();
        UpdateEnergy();
        UpdateBossHP();
        UpdateTurn();
        UpdateLevel();
        UpdateForm();
    }
}