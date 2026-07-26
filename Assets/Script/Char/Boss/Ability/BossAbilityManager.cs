using System.Collections;
using UnityEngine;


public class BossAbilityManager : MonoBehaviour
{

    [Header("Ability Enable")]

    public bool useBarrier;

    public bool useLaser;

    public bool useSummon;

    public bool useRage;



    [Header("Setting")]

    [Range(0f, 1f)]
    public float abilityHealthPercent = 0.8f;


    public int abilityCooldown = 2;



    private int cooldownCounter;



    private BossHealth bossHealth;



    private BossBarrierAbility barrierAbility;

    private BossLaserAbility laserAbility;

    private BossSummonAbility summonAbility;

    private BossRageAbility rageAbility;




    private void Awake()
    {

        bossHealth =
            GetComponent<BossHealth>();


        barrierAbility =
            GetComponent<BossBarrierAbility>();


        laserAbility =
            GetComponent<BossLaserAbility>();


        summonAbility =
            GetComponent<BossSummonAbility>();


        rageAbility =
            GetComponent<BossRageAbility>();

    }





    public void CheckAbility()
    {
        StartCoroutine(
            ExecuteAbilityRoutine()
        );
    }






    public IEnumerator ExecuteAbilityRoutine()
    {

        if (bossHealth == null)
            yield break;



        cooldownCounter++;



        if (cooldownCounter < abilityCooldown)
            yield break;



        float hpPercent =
            (float)bossHealth.CurrentHealth /
            bossHealth.MaxHealth;



        if (hpPercent > abilityHealthPercent)
            yield break;



        cooldownCounter = 0;



        yield return ExecuteAbility();

    }







    private IEnumerator ExecuteAbility()
    {

        Debug.Log(
            "BOSS MENGGUNAKAN ABILITY"
        );



        if (useBarrier &&
           barrierAbility != null)
        {

            barrierAbility.SpawnBarrier();


            yield return
            new WaitForSeconds(0.5f);

        }





        if (useLaser &&
           laserAbility != null)
        {

            laserAbility.ExecuteLaser();


            yield return
            new WaitForSeconds(1.5f);

        }





        if (useSummon &&
           summonAbility != null)
        {

            summonAbility.Summon();


            yield return
            new WaitForSeconds(0.5f);

        }






        if (useRage &&
           rageAbility != null)
        {

            rageAbility.Activate();


            yield return
            new WaitForSeconds(0.5f);

        }

    }





    public void ApplyLevelData(LevelData data)
    {

        useBarrier =
            data.useBarrier;


        useLaser =
            data.useLaser;


        useSummon =
            data.useSummon;


        useRage =
            data.useRage;



        abilityCooldown =
            data.abilityCooldown;



        abilityHealthPercent =
            data.abilityHealthPercent;




        if (barrierAbility != null)
        {
            barrierAbility.SetSetting(
                data.barrierCount,
                data.barrierDuration
            );
        }




        if (laserAbility != null)
        {

            laserAbility.SetSetting(
                data.laserWarningTime,
                data.laserDamage
            );

        }



        Debug.Log(
            "Boss Ability Loaded"
        );

    }

}