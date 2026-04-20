using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject credits;
    public GameObject rules;

    void Start()
    {
        mainPage.SetActive(true);
        credits.SetActive(false);
        rules.SetActive(false);
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
        rules.SetActive(false);
    }

    public void MainPage()
    {
        mainPage.SetActive(true);
        credits.SetActive(false);
        rules.SetActive(false);
    }

    public void Rules()
    {
        mainPage.SetActive(false);
        credits.SetActive(false);
        rules.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
