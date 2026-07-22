using UnityEngine;


public class BossHealth : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 20;



    public int MaxHealth => maxHealth;


    public int CurrentHealth { get; private set; }


    public bool IsDefeated { get; private set; }




    private void Awake()
    {
        CurrentHealth = maxHealth;
    }





    public void SetHealth(int value)
    {

        maxHealth = value;

        CurrentHealth = value;

        IsDefeated = false;



        Debug.Log(
            $"Boss HP diatur menjadi {CurrentHealth}"
        );

    }







    public void TakeDamage(int damage)
    {

        if (IsDefeated)
            return;



        CurrentHealth -= damage;



        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBossHit();




        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(
                0.2f,
                0.08f
            );
        }




        Debug.Log(
            $"Boss menerima {damage} damage. " +
            $"HP Boss: {CurrentHealth}/{maxHealth}"
        );




        if (CurrentHealth <= 0)
        {
            Defeat();
        }

    }








    private void Defeat()
    {

        IsDefeated = true;



        Debug.Log(
            "Boss dikalahkan!"
        );




        // Matikan AI Boss

        BossAI bossAI =
            GetComponent<BossAI>();


        if (bossAI != null)
            bossAI.enabled = false;







        // Kirim event ke Level

        BossLevelComplete levelComplete =
            GetComponent<BossLevelComplete>();


        if (levelComplete != null)
        {
            levelComplete.BossDefeated();
        }







        // Victory UI

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Victory();
        }






        gameObject.SetActive(false);

    }

}