using UnityEngine;


public class MainMenuBackground : MonoBehaviour
{

    [Header("Camera Movement")]
    [SerializeField]
    private Transform cameraTransform;


    [SerializeField]
    private float movementAmount = 0.5f;


    [SerializeField]
    private float movementSpeed = 0.5f;



    [Header("Object Floating")]
    [SerializeField]
    private Transform floatingObject;


    [SerializeField]
    private float floatHeight = 0.15f;


    [SerializeField]
    private float floatSpeed = 2f;



    private Vector3 cameraStartPosition;

    private Vector3 objectStartPosition;



    private void Start()
    {

        if (cameraTransform != null)
        {
            cameraStartPosition =
                cameraTransform.position;
        }


        if (floatingObject != null)
        {
            objectStartPosition =
                floatingObject.position;
        }

    }







    private void Update()
    {

        CameraMovement();


        FloatingEffect();

    }








    private void CameraMovement()
    {

        if (cameraTransform == null)
            return;



        float x =
            Mathf.Sin(
                Time.time *
                movementSpeed
            )
            *
            movementAmount;



        cameraTransform.position =
            cameraStartPosition +
            new Vector3(
                x,
                0,
                0
            );

    }









    private void FloatingEffect()
    {

        if (floatingObject == null)
            return;



        float y =
            Mathf.Sin(
                Time.time *
                floatSpeed
            )
            *
            floatHeight;



        floatingObject.position =
            objectStartPosition +
            new Vector3(
                0,
                y,
                0
            );

    }

}