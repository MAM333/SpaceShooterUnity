using UnityEngine;
using System.Collections;

public class EnemyBoss : EnemyBase
{
    [Header("Apearing Info")]
    public float speedOnApearing = 1;
    public Vector3 posToStartAttacking;

    [Header("Attacking Info")]
    public float timeBetweenAttacks = 2;
    public float timeBetweenBullets = 0.5f;
    public GameObject littleAnt;
    public GameObject enemyBullet;
    public float speed = 3f;
    public Vector3 posRight;
    public Vector3 posUp;
    public Vector3 posLeft;
    public Vector3 posDown;
    public LaserBoss laser;

    private bool apearing = true;

    private int lastAttack = 0;
    private bool isAttacking = false;
    private Vector3 nextMove;
    private CircleCollider2D circleC;

    public override void Awake()
    {
        base.Awake();

        circleC = GetComponent<CircleCollider2D>();
    }

    public override void Start()
    {
        base.Start();

        circleC.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (apearing) Apearing();
        else 
        { 
            if (!laser.IsAttacking()) Move();
        }
    }

    private void Apearing()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, posToStartAttacking, speedOnApearing * Time.deltaTime);
        transform.position = newPos;

        if (newPos == posToStartAttacking)
        {
            circleC.enabled = true;
            apearing = false;
            nextMove = posUp;
            StartCoroutine(BossFight());
        }
    }

    private void Move()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, nextMove, speed * Time.deltaTime);
        transform.position = newPos;

        if (transform.position == nextMove)
        {
            if (nextMove == posUp) nextMove = posLeft;
            else if (nextMove == posLeft) nextMove = posDown;
            else if (nextMove == posDown) nextMove = posRight;
            else nextMove = posUp; // Esta derecha
        }
    }

    /*
       Posibilidades de ataque del jefe:

        - Tirar 4 balas seguidas

        - Spawnear par de enemigos

        - Rayo laser matador (nunca dos veces seguidas)
    */
    IEnumerator BossFight()
    {
        float timer = 0;

        while (health > 0)
        {
            if (isAttacking)
            {
                yield return null;
                continue;
            }
            else if (timer > 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                int bonoloto = Random.Range(0, 3);

                switch (bonoloto)
                {
                    case 0:
                        StartCoroutine(Attack1());
                        break;
                    case 1:
                        StartCoroutine(Attack2());
                        break;
                    case 2:
                        if (lastAttack == 3)
                        {
                            int bonoloto2 = Random.Range(0, 2);
                            if (bonoloto2 == 0) StartCoroutine(Attack1());
                            else StartCoroutine(Attack2());
                        }
                        else StartCoroutine(Attack3());
                        break;
                }

                timer = timeBetweenAttacks;
            }
        }
    }

    private void SpawnAnt(bool goUp)
    {
        GameObject ant = Instantiate(littleAnt);
        ant.transform.position = transform.position;

        EnemyLittleAnt antScript = ant.GetComponent<EnemyLittleAnt>();
        antScript.GoUp(goUp);
        antScript.SetEarningPoints(0);
    }

    IEnumerator Attack1() 
    {
        isAttacking = true;

        SpawnAnt(true);
        SpawnAnt(false);

        float timer = timeBetweenAttacks;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }

        isAttacking = false;
    }

    IEnumerator Attack2()
    {
        isAttacking = true;

        int numOfBullets = Random.Range(3, 6);

        int bulletsSpawned = 0;
        float timer = 0;
        while (bulletsSpawned < numOfBullets)
        {
            if (timer <= 0)
            {
                Instantiate(enemyBullet, transform.position, Quaternion.identity);
                bulletsSpawned++;
                timer = timeBetweenBullets;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            yield return null;
        }

        isAttacking = false;
    }

    IEnumerator Attack3()
    {
        isAttacking = true;

        laser.Attack();
        lastAttack = 3;

        while (laser.IsAttacking())
        {
            yield return null;
        }

        isAttacking = false;
    }
}
