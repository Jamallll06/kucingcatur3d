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

    private TransformingPiece hero;

    private void Start()
    {
        hero = FindFirstObjectByType<TransformingPiece>();

        if (hero != null)
        {
            hpSlider.maxValue = hero.MaxHealth;
        }

        if (EnergyManager.Instance != null)
        {
            energySlider.maxValue =
                EnergyManager.Instance.MaxEnergy;
        }
    }

    private void Update()
    {
        if (hero == null)
            return;

        hpSlider.value = hero.CurrentHealth;
        hpText.text =
            $"{hero.CurrentHealth}/{hero.MaxHealth}";

        formText.text =
            hero.CurrentForm.ToString();

        if (EnergyManager.Instance != null)
        {
            energySlider.value =
                EnergyManager.Instance.CurrentEnergy;

            energyText.text =
                $"{EnergyManager.Instance.CurrentEnergy}/{EnergyManager.Instance.MaxEnergy}";
        }

        if (TurnManager.Instance != null)
        {
            turnText.text =
                TurnManager.Instance.IsPlayerTurn
                    ? "PLAYER TURN"
                    : "BOSS TURN";
        }
    }
}