using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;

    private bool ended = false;
    private bool gameCompleted = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (ended)
        {
            if (EndUI.instance.Ended())
            {
                Mejoras.instance.SaveToBd();

                if (!gameCompleted) SceneManagement.instance.LoadImprovements(); 
                else SceneManagement.instance.GoToMainMenu();
            }
        }
    }

    public bool GetEnded() { return ended; }

    public void EndGame()
    {
        if (ended) return;

        MusicManager.instance.PlayGameTheme();
        EndUI.instance.End();
        ended = true;
    }

    public void GameCompleted()
    {
        MusicManager.instance.PlayWinTheme();
        EndUI.instance.End();
        ended = true;
        gameCompleted = true;
    }
}
