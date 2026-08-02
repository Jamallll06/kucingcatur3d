using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HeroHealthHUD : MonoBehaviour
{

    [Header("Hero HP UI")]
    [SerializeField]
    private Slider heroHpSlider;


    [SerializeField]
    private TMP_Text heroHpText;



    [Header("Smooth")]
    [SerializeField]
    private float smoothSpeed = 8f;

    [Header("Damage Effect")]
    [SerializeField]
    private RectTransform hpObject;

    [SerializeField]
    private float shakeAmount = 8f;

    [SerializeField]
    private float shakeDuration = 0.15f;

    private ChessPiece hero;
    private int lastHP;
    private Vector3 startPos;


    private void Start()
    {
        FindHero();

        if (hero != null)
        {
            lastHP = hero.CurrentHealth;
        }

        if (hpObject != null)
        {
            startPos =
                hpObject.localPosition;
        }

        RefreshInstant();
    }



    private void Update()
    {
        if (hero == null)
        {
            FindHero();
            return;
        }

        if (hero.CurrentHealth < lastHP)
        {
            StartCoroutine(Shake());

            lastHP =
                hero.CurrentHealth;
        }

        UpdateHP();
    }





    private void FindHero()
    {

        hero =
            FindFirstObjectByType<ChessPiece>();


        if (hero != null)
        {
            Debug.Log(
                "Hero HUD connected : "
                +
                hero.name
            );
        }

    }







    private void UpdateHP()
    {

        if (hero == null)
            return;



        if (heroHpSlider != null)
        {

            heroHpSlider.maxValue =
                hero.MaxHealth;



            heroHpSlider.value =
                Mathf.Lerp(
                    heroHpSlider.value,
                    hero.CurrentHealth,
                    Time.deltaTime *
                    smoothSpeed
                );

        }




        if (heroHpText != null)
        {

            heroHpText.text =
                hero.CurrentHealth
                +
                " / "
                +
                hero.MaxHealth;

        }

    }







    private void RefreshInstant()
    {

        if (hero == null)
            return;



        if (heroHpSlider != null)
        {
            heroHpSlider.maxValue =
                hero.MaxHealth;


            heroHpSlider.value =
                hero.CurrentHealth;
        }



        if (heroHpText != null)
        {
            heroHpText.text =
                hero.CurrentHealth
                +
                " / "
                +
                hero.MaxHealth;
        }

    }

    private IEnumerator Shake()
    {
        float timer = 0;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            hpObject.localPosition =
                startPos +
                new Vector3(
                    Random.Range(
                    -shakeAmount,
                    shakeAmount),
                    0,
                    0);

            yield return null;
        }


        hpObject.localPosition =
            startPos;
    }

}