using UnityEngine;


public class BossLevelComplete : MonoBehaviour
{

    private bool completed = false;


    public void BossDefeated()
    {

        if (completed)
            return;


        completed = true;


        Debug.Log(
            "Boss defeated - Level Complete"
        );


        // Tunggu sebelum pindah level
        Invoke(
            nameof(NextLevel),
            2f
        );

    }



    private void NextLevel()
    {

        if (LevelManager.Instance == null)
        {
            Debug.LogWarning(
                "LevelManager tidak ditemukan"
            );

            return;
        }


        LevelManager.Instance.LevelComplete();

    }


}