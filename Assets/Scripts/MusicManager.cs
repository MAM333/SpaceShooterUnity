using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip mainMenuTheme, gameTheme, bossTheme, winTheme;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // El juego da errores al reproducir la musica la primera vez asi que las reproduzco y las paro antes de empezar
        PlayGameTheme();
        PlayBossTheme();
        PlayWinTheme();

        PlayMainMenuTheme();
    }

    private void PlayTrack(AudioClip track, float volume)
    {
        audioSource.volume = volume;
        if (audioSource.clip == track && audioSource.isPlaying) return;
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = track;
        audioSource.Play();
    }

    public void PlayMainMenuTheme()
    {
        PlayTrack(mainMenuTheme, 0.166f);
    }

    public void PlayGameTheme()
    {
        PlayTrack(gameTheme, 0.166f);
    }

    public void PlayBossTheme()
    {
        PlayTrack(bossTheme, 0.166f);
    }

    public void PlayWinTheme()
    {

        PlayTrack(winTheme, 0.35f);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
