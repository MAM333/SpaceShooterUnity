using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;

    public bool ended = false;

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
                SceneManagement.instance.LoadImprovements(); 
            }
        }
    }

    public bool GetEnded() { return ended; }

    public void EndGame()
    {
        if (ended) return;

        EndUI.instance.End();
        ended = true;
    }
}
