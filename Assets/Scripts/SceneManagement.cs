using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;

        MusicManager.instance.PlayMainMenuTheme();

        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        Time.timeScale = 1;

        // +1 on embarcations
        Mejoras.instance.SumAndSaveEmbarcations();

        SceneManager.LoadScene(1);
    }
    
    public void LoadImprovements()
    {
        Time.timeScale = 1;

        MusicManager.instance.PlayGameTheme();

        SceneManager.LoadScene(2);
    }
}
