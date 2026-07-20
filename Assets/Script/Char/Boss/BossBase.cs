using UnityEngine;
using System;


public class BossBase : MonoBehaviour
{

    public int maxHealth = 100;


    protected int currentHealth;


    public Action OnBossDeath;



    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }



    public virtual void TakeDamage(int damage)
    {

        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Die();
        }

    }



    protected virtual void Die()
    {

        Debug.Log(
        "Boss Dead"
        );


        OnBossDeath?.Invoke();


        GameManager.Instance.Victory();


        Destroy(gameObject);

    }



    public float GetHealthPercent()
    {
        return
        (float)currentHealth / maxHealth;
    }

}