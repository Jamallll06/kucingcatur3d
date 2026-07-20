using UnityEngine;


public class RelicManager : MonoBehaviour
{

    public static RelicManager Instance;



    public int bonusHP;
    public int bonusEnergy;
    public int bonusDamage;



    private void Awake()
    {

        Instance = this;

        DontDestroyOnLoad(gameObject);

    }



    public void AddHealth(int value)
    {

        bonusHP += value;

    }



    public void AddEnergy(int value)
    {

        bonusEnergy += value;

    }



    public void AddDamage(int value)
    {

        bonusDamage += value;

    }

}