using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUD : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider energySlider;

    [Header("Texts")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text formText;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text bossNameText;

    [Header("Animation")]
    [SerializeField] private float smoothSpeed = 8f;

    private TransformingPiece hero;
    private BossHealth boss;

    private float currentHP;
    private float currentEnergy;

    private void Start()
    {
        hero = FindFirstObjectByType<TransformingPiece>();
        boss = FindFirstObjectByType<BossHealth>();

        if (hero != null)
        {
            hpSlider.maxValue = hero.MaxHealth;
            currentHP = hero.CurrentHealth;
        }

        if (EnergyManager.Instance != null)
        {
            energySlider.maxValue = EnergyManager.Instance.MaxEnergy;
            currentEnergy = EnergyManager.Instance.CurrentEnergy;
        }

        if (LevelManager.Instance != null && levelText != null)
        {
            levelText.text =
                "LEVEL " +
                LevelManager.Instance.CurrentLevel;
        }

        if (boss != null &&
            bossNameText != null)
        {
            bossNameText.text =
                boss.gameObject.name;
        }
    }

    private void Update()
    {
        if (hero == null)
            return;

        UpdateHealth();

        UpdateEnergy();

        UpdateForm();

        UpdateTurn();
    }

    private void UpdateHealth()
    {
        currentHP = Mathf.Lerp(
            currentHP,
            hero.CurrentHealth,
            Time.deltaTime * smoothSpeed);

        hpSlider.value = currentHP;

        hpText.text =
            hero.CurrentHealth +
            "/" +
            hero.MaxHealth;
    }

    private void UpdateEnergy()
    {
        if (EnergyManager.Instance == null)
            return;

        currentEnergy = Mathf.Lerp(
            currentEnergy,
            EnergyManager.Instance.CurrentEnergy,
            Time.deltaTime * smoothSpeed);

        energySlider.value = currentEnergy;

        energyText.text =
            EnergyManager.Instance.CurrentEnergy +
            "/" +
            EnergyManager.Instance.MaxEnergy;
    }

    private void UpdateForm()
    {
        formText.text =
            hero.CurrentForm.ToString();
    }

    private void UpdateTurn()
    {
        if (TurnManager.Instance == null)
            return;

        if (TurnManager.Instance.IsPlayerTurn)
        {
            turnText.text = "PLAYER TURN";
            turnText.color = Color.green;
        }
        else
        {
            turnText.text = "BOSS TURN";
            turnText.color = Color.red;
        }
    }

    public void PauseButton()
    {
        if (PauseManager.Instance != null)
            PauseManager.Instance.OpenPause();
    }
}