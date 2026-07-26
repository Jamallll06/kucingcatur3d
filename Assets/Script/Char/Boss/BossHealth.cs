using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth = 20;


    public int MaxHealth => maxHealth;

    public int CurrentHealth { get; private set; }

    public bool IsDefeated { get; private set; }



    private BossAbilityManager abilityManager;



    private void Awake()
    {
        CurrentHealth = maxHealth;

        abilityManager =
            GetComponent<BossAbilityManager>();
    }




    public void SetHealth(int value)
    {
        maxHealth = value;

        CurrentHealth = value;

        IsDefeated = false;


        Debug.Log(
            "Boss HP Set : " +
            CurrentHealth
        );
    }




    public void TakeDamage(int damage)
    {
        if (IsDefeated)
            return;



        CurrentHealth -= damage;


        Debug.Log(
            $"Boss menerima {damage} damage " +
            $"HP : {CurrentHealth}/{MaxHealth}"
        );



        if (abilityManager != null)
        {
            abilityManager.CheckAbility();
        }



        if (CurrentHealth <= 0)
        {
            Defeat();
        }

    }





    private void Defeat()
    {
        IsDefeated = true;


        Debug.Log(
            "BOSS KALAH"
        );



        BossAI ai =
            GetComponent<BossAI>();

        if (ai != null)
            ai.enabled = false;



        BossLevelComplete complete =
            GetComponent<BossLevelComplete>();


        if (complete != null)
            complete.BossDefeated();



        if (GameManager.Instance != null)
        {
            GameManager.Instance.Victory();
        }



        gameObject.SetActive(false);
    }
}