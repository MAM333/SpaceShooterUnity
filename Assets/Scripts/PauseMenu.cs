using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public GameObject container;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        container.SetActive(false);
    }

    public void ToggleMenu()
    {
        Time.timeScale = container.activeSelf ? 1 : 0;

        container.SetActive(!container.activeSelf);
    }
}
