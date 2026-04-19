using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;

    private bool ended = false;
    private bool gameCompleted = false;
    private NaveEnergy naveEnergy;

    private void Awake()
    {
        instance = this;
        naveEnergy = FindFirstObjectByType<NaveEnergy>();
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

        naveEnergy.StopLossingEnergy(9999);
        MusicManager.instance.PlayGameTheme();
        EndUI.instance.End();
        ended = true;
    }

    public void GameCompleted()
    {
        naveEnergy.StopLossingEnergy(9999);
        MusicManager.instance.PlayWinTheme();
        EndUI.instance.End();
        ended = true;
        gameCompleted = true;
    }
}
