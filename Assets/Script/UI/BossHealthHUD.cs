using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class BossHealthHUD : MonoBehaviour
{

    [Header("UI")]
    [SerializeField]
    private Slider bossHpSlider;


    [SerializeField]
    private TMP_Text bossHpText;


    [SerializeField]
    private TMP_Text bossNameText;



    [Header("Animation")]
    [SerializeField]
    private RectTransform hpBarObject;


    [SerializeField]
    private float shakeAmount = 10f;


    [SerializeField]
    private float shakeDuration = 0.2f;



    [SerializeField]
    private float smoothSpeed = 8f;



    private BossHealth boss;


    private int lastHP;


    private Vector3 startPosition;



    private void Start()
    {

        boss =
            FindFirstObjectByType<BossHealth>();


        if (hpBarObject != null)
            startPosition =
                hpBarObject.localPosition;


        if (boss != null)
        {
            lastHP =
                boss.CurrentHealth;
        }


        Refresh();

    }





    private void Update()
    {

        if (boss == null)
        {
            boss =
            FindFirstObjectByType<BossHealth>();

            return;
        }


        UpdateHP();


        CheckDamage();

    }






    private void UpdateHP()
    {

        if (bossHpSlider != null)
        {

            bossHpSlider.maxValue =
                boss.MaxHealth;


            bossHpSlider.value =
                Mathf.Lerp(
                    bossHpSlider.value,
                    boss.CurrentHealth,
                    Time.deltaTime *
                    smoothSpeed
                );

        }



        if (bossHpText != null)
        {
            bossHpText.text =
                boss.CurrentHealth
                +
                " / "
                +
                boss.MaxHealth;
        }


        if (bossNameText != null)
        {
            bossNameText.text =
                boss.name;
        }

    }








    private void CheckDamage()
    {

        if (boss.CurrentHealth < lastHP)
        {

            StartCoroutine(
                Shake()
            );


            lastHP =
                boss.CurrentHealth;

        }

    }








    private IEnumerator Shake()
    {

        if (hpBarObject == null)
            yield break;



        float timer = 0;



        while (timer < shakeDuration)
        {

            timer += Time.deltaTime;


            float x =
                Random.Range(
                    -shakeAmount,
                    shakeAmount
                );


            hpBarObject.localPosition =
                startPosition
                +
                new Vector3(
                    x,
                    0,
                    0
                );


            yield return null;

        }



        hpBarObject.localPosition =
            startPosition;

    }








    private void Refresh()
    {

        if (boss == null)
            return;


        if (bossHpSlider != null)
        {
            bossHpSlider.maxValue =
                boss.MaxHealth;


            bossHpSlider.value =
                boss.CurrentHealth;
        }


    }

}