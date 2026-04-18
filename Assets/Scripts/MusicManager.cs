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

        PlayMainMenuTheme();
    }

    private void PlayTrack(AudioClip track)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = track;
        audioSource.Play();
    }

    public void PlayMainMenuTheme()
    {
        PlayTrack(mainMenuTheme);
    }

    public void PlayGameTheme()
    {
        PlayTrack(gameTheme);
    }

    public void PlayBossTheme()
    {
        PlayTrack(bossTheme);
    }

    public void PlayWinTheme()
    {
        PlayTrack(winTheme);
    }
}
