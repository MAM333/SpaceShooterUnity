using System.Collections.Generic;
using UnityEngine;

public class EnemyLittleAnt : EnemyBase
{
    public float speedX = 3f;
    public float speedY = 1.0f;
    public GameObject enemyBullet;
    public Transform bulletSpawnTransform;
    public List<float> bulletRates;

    private Rigidbody2D rb;
    private bool movingUp = true;
    private float timeAct;
    private bool movementSeted = false;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        SetRandomTime();
    }

    public override void Start()
    {
        base.Start();
        rb.linearVelocityX = -speedX;

        if (!movementSeted) movingUp = Random.Range(0, 2) == 0 ? true : false;
    }

    public override void Update()
    {
        base.Update();

        if (movingUp)
        {
            rb.linearVelocityY = speedY;
        }
        else
        {
            rb.linearVelocityY = -speedY;
        }

        if (timeAct > 0)
        {
            timeAct -= Time.deltaTime;
            if (timeAct <= 0)
            {
                GameObject bullet = Instantiate(enemyBullet, bulletSpawnTransform);
                bullet.transform.parent = null;
                SetRandomTime();
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("LimiteEnemy"))
        {
            if (collision.transform.position.y > 0) movingUp = false;
            else movingUp = true;
        }
    }

    public void GoUp(bool up)
    {
        movingUp = up;
        movementSeted = true;
    }

    private void SetRandomTime()
    {
        float time = bulletRates[Random.Range(0, bulletRates.Count)];
        timeAct = time;
    }
}
