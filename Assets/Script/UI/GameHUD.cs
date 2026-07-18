using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text formText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text turnText;

    private TransformingPiece heroCat;

    private void Start()
    {
        heroCat = FindFirstObjectByType<TransformingPiece>();
    }

    private void Update()
    {
        if (heroCat != null)
        {
            formText.text =
                $"Form: {heroCat.CurrentForm}";

            healthText.text =
                $"HP: {heroCat.CurrentHealth}/{heroCat.MaxHealth}";
        }

        if (EnergyManager.Instance != null)
        {
            energyText.text =
                $"Energy: {EnergyManager.Instance.CurrentEnergy}/" +
                $"{EnergyManager.Instance.MaxEnergy}";
        }

        if (TurnManager.Instance != null)
        {
            turnText.text =
                TurnManager.Instance.IsPlayerTurn
                    ? "TURN: PLAYER"
                    : "TURN: BOSS";
        }
    }
}