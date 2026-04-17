using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed;
    public bool enemyBullet = false;
    public int damage = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyBullet) rb.linearVelocityX = -speed;
        else rb.linearVelocityX = speed;
    }

    public int GetDamage() { return damage; }

    public void SetDamage(int dmg) { damage = dmg; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("End") || collider.gameObject.CompareTag("EndBullet"))
        {
            Destroy(gameObject);
        }
    }
}
