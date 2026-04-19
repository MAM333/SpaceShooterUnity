using UnityEngine;
using System.Collections;

public class Trophy : MonoBehaviour
{
    public float secondsWaiting = 2f;
    public float speed = 3f, acceleration = 5f;

    private GameObject nave = null;
    private string letter = "z";

    private void Awake()
    {
        NaveController naveC = FindFirstObjectByType<NaveController>();
        nave = naveC.gameObject;
    }

    public void SetLetter(string lett)
    {
        letter = lett;
    }

    private void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(secondsWaiting);

        speed = -speed;

        while (true)
        {
            yield return null;
            MoveTrophy(nave.transform.position, speed);
            speed += acceleration * Time.deltaTime;
        }
    }

    private void MoveTrophy(Vector3 direction, float speed)
    {
        Vector3 movement = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        transform.position = movement;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Mejoras.instance.SetTrophy(letter);
            EndManager.instance.GameCompleted();
            Destroy(gameObject);
        }
    }
}
