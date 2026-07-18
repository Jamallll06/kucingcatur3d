using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public bool IsDefeated { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDefeated)
            return;

        CurrentHealth -= damage;

        Debug.Log(
            $"Boss menerima {damage} damage. " +
            $"HP Boss: {CurrentHealth}/{maxHealth}"
        );

        if (CurrentHealth <= 0)
            Defeat();
    }

    private void Defeat()
    {
        IsDefeated = true;

        BossAI bossAI = GetComponent<BossAI>();

        if (bossAI != null)
            bossAI.enabled = false;

        gameObject.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.Victory();
    }
}