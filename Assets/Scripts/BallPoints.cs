using UnityEngine;
using System.Collections;

public class BallPoints : MonoBehaviour
{
    public float acceleration = 3f;
    public float speed = 0.5f;

    private int points = 1;
    private GameObject nave = null;
    private BoxCollider2D naveBoxC;

    public void SetPoints(int pts)
    {
        points = pts;
    }

    private void Start()
    {
        NaveController naveScript = FindFirstObjectByType<NaveController>();
        if (naveScript == null) Debug.Log("NO ENCUENTRA A PLAYER");
        else 
        { 
            nave = naveScript.gameObject;
            naveBoxC = nave.GetComponent<BoxCollider2D>();
            StartCoroutine(MoveToPlayer());
        }
    }

    IEnumerator MoveToPlayer()
    {
        speed = -speed;

        while (true)
        {
            yield return null;
            MoveBall(nave.transform.position, speed);
            speed += acceleration * Time.deltaTime;

            // Por si el player se muere antes de recibir el punto
            if (!naveBoxC.enabled)
            {
                if (transform.position == nave.transform.position)
                {
                    AddPoints();
                }
            }
        }
    }

    private void MoveBall(Vector3 direction, float speed)
    {
        Vector3 movement = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        transform.position = movement;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            AddPoints();
        }
    }

    private void AddPoints()
    {
        Mejoras.instance.AddPoints(points);

        SfxManager.instance.BallPoint();
        Numbers.instance.ShowNumber(points);

        Destroy(gameObject);
    }
}
