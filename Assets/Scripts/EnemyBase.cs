using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("EnemyBase")]
    public int health = 10;
    public float timeBeingWhite = 0.1f;
    public int valueOfPoints = 1;
    
    private float timeWhiteAct = 0;
    private SpriteRenderer spr;
    private SpriteRenderer sprWhite;

    private bool isAlive = true;
    private bool powerUps = false;
    private int powerUpProbabilites = 0;
    private bool createdBySpawner = false;

    public virtual void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        Transform spriteWhiteTransform = transform.Find("SpriteWhite");
        sprWhite = spriteWhiteTransform.GetComponent<SpriteRenderer>();
    }

    public virtual void Start()
    {
        float powUps = Mejoras.instance.CheckUpgrade("powerUps");
        if (powUps != 0) 
        { 
            powerUps = true;

            float probabilites = Mejoras.instance.CheckUpgrade("chanceOfPowerUps");
            powerUpProbabilites = (int)probabilites;
        }
    }

    public virtual void Update()
    {
        if (timeWhiteAct > 0)
        {
            timeWhiteAct -= Time.deltaTime;
            if (timeWhiteAct < 0)
            {
                timeWhiteAct = 0;
            }

            Color newColor = sprWhite.color;
            newColor.a = timeWhiteAct / timeBeingWhite;
            sprWhite.color = newColor;
        }
    }

    public void SetEarningPoints(int pts)
    {
        valueOfPoints = pts;
    }

    public void SetCreatedBySpawner()
    {
        createdBySpawner = true;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            Die(true);
        }
        else if (timeWhiteAct == 0)
        { 
            timeWhiteAct = timeBeingWhite;
            SfxManager.instance.DamageEnemy();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            int damage = (collision.gameObject.GetComponent<Bala>()).GetDamage();
            GetDamage(damage);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("End"))
        {
            Die(false);
        }
    }

    private void Die(bool byPlayer)
    {
        if (isAlive == false) return;

        isAlive = false;

        if (byPlayer)
        {
            Mejoras.instance.AddPoints(valueOfPoints); // En verdad creare un punto en el aire que vaya hacia el player y ahi le dara los puntos

            if (powerUps)
            {
                int random = Random.Range(0, 100);
                if (random <= powerUpProbabilites)
                {
                    PowerUpsManager.instance.SpawnRandom(transform.position);
                }
            }
        }

        if (createdBySpawner) EnemiesManager.instance.EnemyDied();

        Destroy(gameObject);
    }
}
