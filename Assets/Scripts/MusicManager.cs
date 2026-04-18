using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource gameTheme, bossTheme;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // A veces hay errores al reproducir la cancion directamente
        bossTheme.Play();
        bossTheme.Stop();

        gameTheme.Play();
    }

    public void PlayGameTheme()
    {
        if (bossTheme.isPlaying) bossTheme.Stop();
        if (!gameTheme.isPlaying) gameTheme.Play();
    }

    public void PlayBossTheme()
    {
        if (gameTheme.isPlaying) gameTheme.Stop();
        if (!bossTheme.isPlaying) bossTheme.Play();
    }
}
