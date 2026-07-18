using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject barContainer;

    private BossHealth bossHealth;

    private void Start()
    {
        bossHealth = FindFirstObjectByType<BossHealth>();

        if (bossHealth == null)
        {
            Debug.LogWarning("BossHealth tidak ditemukan.");
            return;
        }

        healthSlider.minValue = 0;
        healthSlider.maxValue = bossHealth.MaxHealth;
        healthSlider.value = bossHealth.CurrentHealth;
    }

    private void Update()
    {
        if (bossHealth == null)
            return;

        healthSlider.value = bossHealth.CurrentHealth;

        if (bossHealth.IsDefeated && barContainer != null)
            barContainer.SetActive(false);
    }
}