using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }

    [SerializeField] private int maxEnergy = 6;

    public int CurrentEnergy { get; private set; }
    public int MaxEnergy => maxEnergy;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddEnergy(int amount)
    {
        CurrentEnergy = Mathf.Clamp(
            CurrentEnergy + amount,
            0,
            maxEnergy
        );
    }

    public bool TrySpend(int cost)
    {
        if (CurrentEnergy < cost)
        {
            Debug.Log("Energy tidak cukup.");
            return false;
        }

        CurrentEnergy -= cost;
        return true;
    }
}