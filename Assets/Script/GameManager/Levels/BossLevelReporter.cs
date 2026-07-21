using UnityEngine;


public class BossLevelReporter : MonoBehaviour
{


    public void BossDefeated()
    {

        if (LevelManager.Instance != null)
        {

            LevelManager.Instance.CompleteLevel();

        }

    }


}