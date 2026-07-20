using UnityEngine;
using System.Collections;


public abstract class BossAbility : MonoBehaviour
{

    public string abilityName;


    public float cooldown = 3;


    bool ready = true;



    public IEnumerator UseAbility()
    {

        if (!ready)
            yield break;


        ready = false;


        yield return Execute();



        yield return new WaitForSeconds(cooldown);


        ready = true;

    }



    protected abstract IEnumerator Execute();

}