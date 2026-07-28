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
            "Boss HP Set : "
            + CurrentHealth
        );

    }








    public void TakeDamage(int damage)
    {

        if (IsDefeated)
            return;



        CurrentHealth -= damage;



        CurrentHealth =
            Mathf.Max(
                CurrentHealth,
                0
            );



        Debug.Log(
            $"Boss Damage {damage} | HP {CurrentHealth}/{MaxHealth}"
        );





        // CHECK BOSS ABILITY

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

        if (IsDefeated)
            return;



        IsDefeated = true;



        Debug.Log(
            "===== BOSS KALAH ====="
        );





        BossAI ai =
            GetComponent<BossAI>();


        if (ai != null)
            ai.enabled = false;







        // ==========================
        // LEVEL COMPLETE SYSTEM
        // ==========================

        if (LevelCompleteManager.Instance != null)
        {

            LevelCompleteManager.Instance
                .CompleteLevel();

        }



        // ==========================
        // GAME MANAGER OPTIONAL
        // ==========================

        if (GameManager.Instance != null)
        {

            GameManager.Instance
                .Victory();

        }






        gameObject.SetActive(false);

    }

}