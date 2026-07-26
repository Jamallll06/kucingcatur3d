using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}