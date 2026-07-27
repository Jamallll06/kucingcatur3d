using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }



    [Header("Audio Source")]

    [SerializeField]
    private AudioSource musicSource;


    [SerializeField]
    private AudioSource sfxSource;




    [Header("Music")]

    [SerializeField]
    private AudioClip menuMusic;


    [SerializeField]
    private AudioClip battleMusic;





    [Header("SFX")]

    [SerializeField]
    private AudioClip moveSfx;


    [SerializeField]
    private AudioClip hitSfx;


    [SerializeField]
    private AudioClip bossHitSfx;


    [SerializeField]
    private AudioClip parrySfx;


    [SerializeField]
    private AudioClip transformSfx;


    [SerializeField]
    private AudioClip victorySfx;


    [SerializeField]
    private AudioClip gameOverSfx;



    private void Awake()
    {

        if (Instance != null &&
           Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        DontDestroyOnLoad(gameObject);

    }







    private void Start()
    {

        LoadVolume();

    }









    // ==========================
    // MUSIC
    // ==========================


    public void PlayMenuMusic()
    {

        PlayMusic(
            menuMusic
        );

    }





    public void PlayBattleMusic()
    {

        PlayMusic(
            battleMusic
        );

    }






    private void PlayMusic(
        AudioClip clip
    )
    {

        if (musicSource == null ||
           clip == null)
            return;



        if (musicSource.clip == clip &&
           musicSource.isPlaying)
            return;



        musicSource.clip = clip;

        musicSource.loop = true;

        musicSource.Play();

    }









    // ==========================
    // SFX
    // ==========================


    public void PlayMove()
    {
        PlaySfx(moveSfx);
    }



    public void PlayHit()
    {
        PlaySfx(hitSfx);
    }



    public void PlayBossHit()
    {
        PlaySfx(bossHitSfx);
    }



    public void PlayParry()
    {
        PlaySfx(parrySfx);
    }



    public void PlayTransform()
    {
        PlaySfx(transformSfx);
    }



    public void PlayVictory()
    {
        PlaySfx(victorySfx);
    }



    public void PlayGameOver()
    {
        PlaySfx(gameOverSfx);
    }





    private void PlaySfx(
        AudioClip clip
    )
    {

        if (sfxSource == null ||
           clip == null)
            return;



        sfxSource.PlayOneShot(
            clip
        );

    }









    // ==========================
    // VOLUME
    // ==========================


    public void SetMusicVolume(
        float value
    )
    {

        if (musicSource != null)
            musicSource.volume = value;



        if (SaveManager.Instance != null)
        {
            SaveManager.Instance
            .SaveBGMVolume(value);
        }

    }








    public void SetSFXVolume(
        float value
    )
    {

        if (sfxSource != null)
            sfxSource.volume = value;



        if (SaveManager.Instance != null)
        {
            SaveManager.Instance
            .SaveSFXVolume(value);
        }

    }









    private void LoadVolume()
    {

        if (SaveManager.Instance == null)
            return;



        if (musicSource != null)
        {

            musicSource.volume =
                SaveManager.Instance
                .GetBGMVolume();

        }



        if (sfxSource != null)
        {

            sfxSource.volume =
                SaveManager.Instance
                .GetSFXVolume();

        }

    }

}