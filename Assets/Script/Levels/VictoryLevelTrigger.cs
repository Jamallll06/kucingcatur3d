using UnityEngine;


public class VictoryLevelTrigger : MonoBehaviour
{

    private void Update()
    {

        if (GameManager.Instance == null)
            return;


        if (GameManager.Instance.IsGameFinished)
        {
            Invoke(
                nameof(NextLevel),
                2f
            );
        }

    }



    private void NextLevel()
    {

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LevelComplete();
        }

    }

}