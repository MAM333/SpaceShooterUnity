using UnityEngine;
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
        MusicManager.instance.PlayMainMenuTheme();

        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        // +1 on embarcations
        Mejoras.instance.SumAndSaveEmbarcations();

        SceneManager.LoadScene(1);
    }
    
    public void LoadImprovements()
    {
        SceneManager.LoadScene(2);
    }
}
