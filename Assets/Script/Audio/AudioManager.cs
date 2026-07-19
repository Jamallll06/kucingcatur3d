using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip battleMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip moveSfx;
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private AudioClip bossHitSfx;
    [SerializeField] private AudioClip parrySfx;
    [SerializeField] private AudioClip transformSfx;
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip gameOverSfx;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (battleMusic != null)
        {
            musicSource.clip = battleMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMove() => PlaySfx(moveSfx);
    public void PlayHit() => PlaySfx(hitSfx);
    public void PlayBossHit() => PlaySfx(bossHitSfx);
    public void PlayParry() => PlaySfx(parrySfx);
    public void PlayTransform() => PlaySfx(transformSfx);
    public void PlayVictory() => PlaySfx(victorySfx);
    public void PlayGameOver() => PlaySfx(gameOverSfx);

    private void PlaySfx(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }
}