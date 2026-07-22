using UnityEngine;

public class BossAbilityManager : MonoBehaviour
{
    [Header("Ability Enable")]
    public bool useBarrier;
    public bool useSummon;
    public bool useLaser;
    public bool useRage;


    [Header("Trigger")]
    [SerializeField] private float abilityHealthPercent = 0.5f;


    private BossHealth bossHealth;

    private BossBarrierAbility barrierAbility;

    private bool abilityUsed = false;



    private void Awake()
    {
        bossHealth = GetComponent<BossHealth>();

        barrierAbility =
            GetComponent<BossBarrierAbility>();
    }



    public void CheckAbility()
    {
        if (abilityUsed)
            return;


        if (bossHealth == null)
            return;



        float hpPercent =
            (float)bossHealth.CurrentHealth /
            bossHealth.MaxHealth;



        if (hpPercent <= abilityHealthPercent)
        {
            ExecuteAbility();

            abilityUsed = true;
        }

    }





    private void ExecuteAbility()
    {

        Debug.Log(
            "Boss menggunakan Ability!"
        );



        if (useBarrier)
        {
            UseBarrier();
        }



        if (useLaser)
        {
            UseLaser();
        }



        if (useSummon)
        {
            UseSummon();
        }



        if (useRage)
        {
            UseRage();
        }

    }






    private void UseBarrier()
    {
        if (barrierAbility != null)
        {
            barrierAbility.SpawnBarrier();
        }


        Debug.Log(
            "Barrier Aktif"
        );
    }





    private void UseLaser()
    {
        Debug.Log(
            "Laser Ability Aktif"
        );

        // nanti Level 3
    }





    private void UseSummon()
    {
        Debug.Log(
            "Summon Aktif"
        );

        // nanti Level 5
    }





    private void UseRage()
    {
        Debug.Log(
            "Rage Aktif"
        );

        // nanti Level 5
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



        if (barrierAbility != null)
        {
            barrierAbility.SetSetting(
                data.barrierCount,
                data.barrierDuration
            );
        }


        Debug.Log(
            "Boss Ability Loaded : "
            + data.levelName
        );

    }

}