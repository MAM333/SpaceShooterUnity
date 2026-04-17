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

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadImprovements()
    {
        SceneManager.LoadScene(2);
    }
}
