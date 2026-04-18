using UnityEngine;
using System.Collections;

public class Trophy : MonoBehaviour
{
    public float secondsWaiting = 2f;
    public float speed = 3f, acceleration = 5f;

    private GameObject nave = null;

    private void Awake()
    {
        NaveController naveC = FindFirstObjectByType<NaveController>();
        nave = naveC.gameObject;
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
            EndManager.instance.GameCompleted();
            Destroy(gameObject);
        }
    }
}
