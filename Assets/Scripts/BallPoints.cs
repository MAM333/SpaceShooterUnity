using UnityEngine;

public class BallPoints : MonoBehaviour
{
    private int points = 1;

    public void SetPoints(int pts)
    {
        points = pts;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Mejoras.instance.AddPoints(points);
            Destroy(gameObject);
        }
    }
}
