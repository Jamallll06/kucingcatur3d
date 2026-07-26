using UnityEngine;


public class BossRageAbility : MonoBehaviour
{

    [Header("Rage Setting")]

    [SerializeField]
    private float damageMultiplier = 1.5f;


    [SerializeField]
    private float speedMultiplier = 1.3f;



    [SerializeField]
    private ParticleSystem rageEffect;



    private bool isRaging;



    private BossAI bossAI;




    private void Awake()
    {
        bossAI =
            GetComponent<BossAI>();
    }






    public void Activate()
    {

        if (isRaging)
            return;



        isRaging = true;



        Debug.Log(
            "===== BOSS RAGE ACTIVE ====="
        );



        ApplyRage();



        if (rageEffect != null)
        {
            rageEffect.Play();
        }

    }








    private void ApplyRage()
    {

        if (bossAI != null)
        {

            bossAI.ApplyRage(
                damageMultiplier,
                speedMultiplier
            );

        }


    }







    public bool IsRaging()
    {
        return isRaging;
    }


}