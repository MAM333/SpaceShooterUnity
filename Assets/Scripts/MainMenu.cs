using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject credits;

    void Start()
    {
        mainPage.SetActive(true);
        credits.SetActive(false);
    }

    public void NewGame()
    {
        // Borrar y continuar
        Mejoras.instance.RestartAll();

        Continue();
    }

    public void Continue()
    {
        MusicManager.instance.PlayGameTheme();
        SceneManagement.instance.LoadGame();
    }

    public void Credits()
    {
        credits.SetActive(true);
        mainPage.SetActive(false);
    }

    public void MainPage()
    {
        mainPage.SetActive(true);
        credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
