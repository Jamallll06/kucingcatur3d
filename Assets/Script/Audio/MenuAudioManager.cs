using System.Collections;
using UnityEngine;



public class MenuAudioManager : MonoBehaviour
{

    public static MenuAudioManager Instance { get; private set; }



    [Header("Audio Source")]
    [SerializeField]
    private AudioSource musicSource;


    [SerializeField]
    private AudioSource sfxSource;



    [Header("Music")]
    [SerializeField]
    private AudioClip menuMusic;



    [Header("Button SFX")]
    [SerializeField]
    private AudioClip hoverSound;


    [SerializeField]
    private AudioClip clickSound;



    [Header("Fade")]
    [SerializeField]
    private float fadeSpeed = 1f;



    private float targetVolume;






    private void Awake()
    {

        if (Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

    }









    private void Start()
    {

        LoadVolume();


        PlayMenuMusic();

    }









    private void PlayMenuMusic()
    {

        if (menuMusic == null)
            return;



        musicSource.clip =
            menuMusic;


        musicSource.loop =
            true;


        musicSource.volume =
            0;



        musicSource.Play();



        StartCoroutine(
            FadeMusicIn()
        );

    }









    private IEnumerator FadeMusicIn()
    {

        while (
            musicSource.volume <
            targetVolume
        )
        {

            musicSource.volume =
                Mathf.MoveTowards(
                    musicSource.volume,
                    targetVolume,
                    Time.deltaTime *
                    fadeSpeed
                );


            yield return null;

        }

    }









    public void FadeOutMusic()
    {

        StartCoroutine(
            FadeMusicOut()
        );

    }









    private IEnumerator FadeMusicOut()
    {

        while (
            musicSource.volume > 0
        )
        {

            musicSource.volume =
                Mathf.MoveTowards(
                    musicSource.volume,
                    0,
                    Time.deltaTime *
                    fadeSpeed
                );


            yield return null;

        }


        musicSource.Stop();

    }









    public void PlayHover()
    {

        if (hoverSound == null)
            return;


        sfxSource.PlayOneShot(
            hoverSound
        );

    }









    public void PlayClick()
    {

        if (clickSound == null)
            return;


        sfxSource.PlayOneShot(
            clickSound
        );

    }









    public void SetMusicVolume(float value)
    {

        musicSource.volume =
            value;


        PlayerPrefs.SetFloat(
            "MusicVolume",
            value
        );


        PlayerPrefs.Save();

    }









    public void SetSFXVolume(float value)
    {

        sfxSource.volume =
            value;


        PlayerPrefs.SetFloat(
            "SFXVolume",
            value
        );


        PlayerPrefs.Save();

    }









    private void LoadVolume()
    {

        targetVolume =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                1f
            );


        musicSource.volume =
            targetVolume;



        sfxSource.volume =
            PlayerPrefs.GetFloat(
                "SFXVolume",
                1f
            );

    }

}