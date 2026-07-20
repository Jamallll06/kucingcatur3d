using UnityEngine;


public class ObjectiveManager : MonoBehaviour
{

    public static ObjectiveManager Instance;


    public string objectiveText;


    private int currentProgress;


    public int targetAmount;



    private void Awake()
    {
        Instance = this;
    }



    public void AddProgress(int amount)
    {

        currentProgress += amount;


        Debug.Log(
        objectiveText
        + " "
        + currentProgress
        + "/"
        + targetAmount
        );


        if (currentProgress >= targetAmount)
        {

            CompleteObjective();

        }

    }



    void CompleteObjective()
    {

        Debug.Log(
        "Objective Complete!"
        );

    }


}