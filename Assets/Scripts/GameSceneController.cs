using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSceneController : MonoBehaviour
{
    public InputActionReference restart;
    public InputActionReference pause;
    public InputActionReference goToUpgrades;

    private float timer = 0;

    private void OnEnable()
    {
        restart.action.Enable();
        pause.action.Enable();
        goToUpgrades.action.Enable();
    }

    private void OnDisable()
    {
        restart.action.Disable();
        pause.action.Disable();
        goToUpgrades.action.Disable();
    }

    void Update()
    {
        if (restart.action.ReadValue<float>() > 0)
        {
            SceneManagement.instance.LoadGame();
        }

        if (pause.action.ReadValue<float>() > 0 && timer <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(ToggleMenu());
        }

        if (goToUpgrades.action.ReadValue<float>() > 0)
        {
            Mejoras.instance.SaveToBd();

            SceneManagement.instance.LoadImprovements();
        }
    }

    IEnumerator ToggleMenu()
    {
        PauseMenu.instance.ToggleMenu();
        
        timer = 0.1f;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }
    }
}
