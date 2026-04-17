using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class NaveController : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform inicioBala1;
    public Transform inicioBala2;
    public Transform inicioBala3;
    public Transform inicioBala4;

    public InputActionReference move;
    public InputActionReference shoot;

    // Atributos dependientes de mejoras
    private float speedX = 3f;
    private float speedY = 1f;
    private float shootTime = 0.2f;
    private int bulletDamage = 1;

    // No dependientes
    private float powerUpTimer = 0f;
    private PowerUpType powerUpAct = PowerUpType.None;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Color initColor;
    private float actDelayShoot = 0f;
    private NaveLife naveLife;
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        naveLife = GetComponent<NaveLife>();
    }

    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();
    }

    void Start()
    {
        initColor = spr.color;

        float speed = Mejoras.instance.CheckUpgrade("speed");
        speedX = speed;
        speedY = speed;

        float shootT = Mejoras.instance.CheckUpgrade("shootRate");
        shootTime = shootT;

        int damage = (int)Mejoras.instance.CheckUpgrade("damage");
        bulletDamage = damage;
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    public void NotMove()
    {
        canMove = false;
        rb.linearVelocity = new Vector2(0, 0);
    }

    private void PowerUpBehaviour(PowerUpType type, float duration, Color newColor)
    {
        powerUpAct = type;
        powerUpTimer = duration;
        spr.color = newColor;

        switch (type)
        {
            case PowerUpType.Nuke: 
                //Destruir a los enemigos
                break;

            case PowerUpType.Bullets:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PowerUp"))
        {
            PowerUp powerUpScript = collider.gameObject.GetComponent<PowerUp>();
            PowerUpType type = powerUpScript.GetPowerUpType();
            float duration = powerUpScript.GetDuration();
            Color newColor = powerUpScript.GetColor();
            PowerUpBehaviour(type, duration, newColor);

            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("EnemyBullet"))
        {
            Bala bulletScript = collider.gameObject.GetComponent<Bala>();
            int damage = bulletScript.GetDamage();
            if (powerUpAct != PowerUpType.Inmune) naveLife.GetDamage(damage);
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("LaserEnemy"))
        {
            LaserBoss lb = collider.gameObject.GetComponent<LaserBoss>();
            int damage = lb.GetDamage();
            if (powerUpAct != PowerUpType.Inmune) naveLife.GetDamage(damage);
        }
    }

    private void Movement()
    {
        if (!canMove) return;

        Vector2 input = move.action.ReadValue<Vector2>();
        rb.linearVelocity = input.normalized * speedX;
    }

    private void Shoot()
    {
        if (!canMove) return;

        if (actDelayShoot > 0)
        {
            actDelayShoot -= Time.deltaTime;
            if (actDelayShoot < 0) actDelayShoot = 0;
        }

        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer < 0) 
            { 
                powerUpTimer = 0; 
                spr.color = initColor;
                powerUpAct = PowerUpType.None;
            }
        }

        float disparo = shoot.action.ReadValue<float>();
        if (disparo == 1 && actDelayShoot == 0)
        {
            InstantiateBullet(inicioBala1.transform);
            InstantiateBullet(inicioBala2.transform);
            

            if (powerUpAct == PowerUpType.Bullets && powerUpTimer > 0)
            {
                InstantiateBullet(inicioBala3.transform);
                InstantiateBullet(inicioBala4.transform);
            }

            actDelayShoot = shootTime;
            SfxManager.instance.Shooting(true);
        }
    }

    private void InstantiateBullet(Transform trf)
    {
        GameObject bullet = Instantiate(balaPrefab, trf);
        bullet.transform.parent = null;
        Bala bulletScript = bullet.GetComponent<Bala>();
        bulletScript.SetDamage(bulletDamage);
    }
}
