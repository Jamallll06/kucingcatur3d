using UnityEngine;
using System.Collections;


public class BossAbilityManager : MonoBehaviour
{

    [Header("Ability Settings")]
    public bool useBarrier;
    public bool useSummon;
    public bool useRage;



    private BossHealth bossHealth;



    private void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
    }



    public IEnumerator ExecuteAbility()
    {

        if (useBarrier)
        {
            CreateBarrier();

            yield return new WaitForSeconds(1f);
        }



        if (useSummon)
        {
            SummonMinion();

            yield return new WaitForSeconds(1f);
        }



        if (useRage)
        {
            RageMode();

            yield return new WaitForSeconds(1f);
        }

    }

    public bool CanUseAbility()
    {

        if (bossHealth == null)
            return false;


        float hp =
        (float)bossHealth.CurrentHealth /
        bossHealth.MaxHealth;


        return hp <= 0.5f;

    }


    private void CreateBarrier()
    {
        Debug.Log(
            "Boss membuat barrier!"
        );

        // nanti isi spawn barrier tile
    }



    private void SummonMinion()
    {
        Debug.Log(
            "Boss summon minion!"
        );

        // spawn enemy tambahan
    }



    private void RageMode()
    {
        Debug.Log(
            "Boss masuk rage mode!"
        );

    }

}