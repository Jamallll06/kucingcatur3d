using UnityEngine;

public class PersistentManagers : MonoBehaviour
{
    private static PersistentManagers instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}