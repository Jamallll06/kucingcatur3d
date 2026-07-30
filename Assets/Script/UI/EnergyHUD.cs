using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class EnergyHUD : MonoBehaviour
{

    [Header("Energy UI")]
    [SerializeField]
    private Slider energySlider;


    [SerializeField]
    private TMP_Text energyText;



    [Header("Animation")]
    [SerializeField]
    private RectTransform energyObject;


    [SerializeField]
    private float smoothSpeed = 8f;


    [SerializeField]
    private float pulseScale = 1.15f;


    [SerializeField]
    private float pulseDuration = 0.2f;



    private Vector3 startScale;


    private bool wasFull;



    private void Start()
    {

        if (energyObject != null)
        {
            startScale =
                energyObject.localScale;
        }


        Refresh();

    }







    private void Update()
    {

        if (EnergyManager.Instance == null)
            return;



        UpdateEnergy();


        CheckFullEnergy();

    }









    private void UpdateEnergy()
    {

        int current =
            EnergyManager.Instance.CurrentEnergy;


        int max =
            EnergyManager.Instance.MaxEnergy;



        if (energySlider != null)
        {

            energySlider.maxValue =
                max;


            energySlider.value =
                Mathf.Lerp(
                    energySlider.value,
                    current,
                    Time.deltaTime *
                    smoothSpeed
                );

        }





        if (energyText != null)
        {

            energyText.text =
                current
                +
                " / "
                +
                max;

        }

    }










    private void CheckFullEnergy()
    {

        int current =
            EnergyManager.Instance.CurrentEnergy;


        int max =
            EnergyManager.Instance.MaxEnergy;



        bool full =
            current >= max;



        if (full && !wasFull)
        {

            StartCoroutine(
                FullEnergyEffect()
            );


            if (NotificationHUD.Instance != null)
            {
                NotificationHUD.Instance
                .Show(
                "ENERGY FULL!"
                );
            }

        }



        wasFull =
            full;

    }









    private IEnumerator FullEnergyEffect()
    {

        if (energyObject == null)
            yield break;



        energyObject.localScale =
            startScale *
            pulseScale;



        yield return new WaitForSeconds(
            pulseDuration
        );



        energyObject.localScale =
            startScale;

    }









    private void Refresh()
    {

        if (EnergyManager.Instance == null)
            return;



        if (energySlider != null)
        {

            energySlider.maxValue =
            EnergyManager.Instance.MaxEnergy;


            energySlider.value =
            EnergyManager.Instance.CurrentEnergy;

        }




        if (energyText != null)
        {

            energyText.text =
            EnergyManager.Instance.CurrentEnergy
            +
            " / "
            +
            EnergyManager.Instance.MaxEnergy;

        }

    }

}