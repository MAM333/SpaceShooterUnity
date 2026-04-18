using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[Serializable]
public struct WaveInfo
{
    public float timeToSpawn;
};

[Serializable]
public struct Wave
{
    public List<WaveInfo> waveInfo;
};

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;

    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2, enemy3;
    public GameObject boss1, boss2, boss3;

    [Header("EnemiesSpawn")]
    public Transform spawnEnemies;
    public Transform top;
    public Transform bottom;
    public List<float> probabilitesOfSpawners = new List<float>();

    [Header("WaveThings")]
    // Las oleadas spawnearan con timeBetweenWaves de diferencia una vez se haya instanciado el ultimo enemigo. Pero si el player
    // ejecuta a todos los enemigos saldran directamente para no hacerlo aburrido una vez se escale mucho el poder de la nave
    public float timeBetweenWaves = 2;
    public float substractTimeBetweenWaves = 0.5f;
    public float minTimeBetweenWaves = 0.5f;
    public List<int> numEnemiesUntilBoss;
    // List<WaveInfo> es un listado de timers que indican cuanto tardan los enemigos en spawnear
    // Por lo tanto tenemos varios listados de timers, de manera que podamos controlar cuando
    // pueden aparecer los enemigos. Al principio iba a poner tambien una posicion en Y, pero
    // me gustaba mas la idea de poner una posicion random entre Top y Bottom. Es por eso que cree
    // WaveInfo, y lo he dejado asi por si quiero escalarlo a ponerle algo mas de informacion
    // en el futuro.
    [SerializeField]
    public List<Wave> waves = new List<Wave>();

    private Wave actWave;
    public float actTimeBetweenWaves;
    private bool changedWave = false;

    // Boss
    private bool inBossFight = false;
    private int actBoss = 0;
    private int deadEnemies = 0;
    private bool nextIsBossFight = false;

    //Mejoras
    private int pointsEnemy1 = 1;
    private int pointsEnemy2 = 1;
    private int pointsEnemy3 = 1;

    private void Awake()
    {
        instance = this;

        ChangeWave(false);
    }

    private void Start()
    {
        float pointsEnem1 = Mejoras.instance.CheckUpgrade("earnPointsOfEnemy1");
        pointsEnemy1 = (int)pointsEnem1;

        float pointsEnem2 = Mejoras.instance.CheckUpgrade("earnPointsOfEnemy2");
        pointsEnemy2 = (int)pointsEnem2;
        
        float pointsEnem3 = Mejoras.instance.CheckUpgrade("earnPointsOfEnemy3");
        pointsEnemy3 = (int)pointsEnem3;

        StartCoroutine(SpawnerBehaviour());
    }

    private void Update()
    {
        if (inBossFight) return;

        //SpawnerBehaviour();
    }

    IEnumerator SpawnerBehaviour()
    {
        while (true)
        {
            if (inBossFight)
            {
                yield return null;
                continue;
            }

            if (actWave.waveInfo != null)
            {
                int actI = 0;
                while (actI < actWave.waveInfo.Count)
                {
                    yield return new WaitForSeconds(actWave.waveInfo[actI].timeToSpawn);
                
                    SpawnLittleEnemy(enemy1, pointsEnemy1);

                    ++actI;
                }
            }

            changedWave = false;

            actWave = new Wave();

            float timerBtwWaves = timeBetweenWaves;
            while (timerBtwWaves > 0 && !changedWave) // Puede ser que se me cambie la oleada estando aqui
            {
                yield return null;
                timerBtwWaves -= Time.deltaTime;
            }

            timeBetweenWaves -= substractTimeBetweenWaves;
            if (timeBetweenWaves < minTimeBetweenWaves) timeBetweenWaves = minTimeBetweenWaves;

            if (changedWave || inBossFight) continue;

            ChangeWave(false);
        }
    }

    private void SpawnLittleEnemy(GameObject littleAnt, int points)
    {
        GameObject spawnedEnemy = Instantiate(littleAnt);

        // He cambiado esto asi Enrique porque has dicho que habia una funcion de Mathf.Lerp para valores en vez de Vector3.Lerp como hiciste tu
        float positionY = Mathf.Lerp(top.position.y, bottom.position.y, UnityEngine.Random.Range(0.1f, 1f));
        spawnedEnemy.transform.position = new Vector3(spawnEnemies.position.x, positionY, 0);
        spawnedEnemy.transform.parent = transform;

        EnemyBase enemyScript = spawnedEnemy.GetComponent<EnemyBase>();
        enemyScript.SetEarningPoints(points);
        enemyScript.SetCreatedBySpawner();
    }

    private void ChangeWave(bool canSpawnBoss)
    {
        if (nextIsBossFight && canSpawnBoss)
        {
            inBossFight = true;
            deadEnemies = 0;
            nextIsBossFight = false;

            GameObject boss = null;
            switch (actBoss)
            {
                case 0: boss = boss1; break;
                case 1: boss = boss2; break;
                case 2: boss = boss3; break;
            }


            // Instanciar boss
            GameObject spawnedEnemy = Instantiate(boss);
            spawnedEnemy.transform.position = new Vector3(spawnEnemies.position.x + 5, 0, 0);
            
            EnemyBase enemyBase = spawnedEnemy.GetComponent<EnemyBase>();
            enemyBase.SetCreatedBySpawner();

            MusicManager.instance.PlayBossTheme();
        }
        else if (!nextIsBossFight)
        {
            actWave = waves[UnityEngine.Random.Range(0, waves.Count)];

            if (deadEnemies + actWave.waveInfo.Count > numEnemiesUntilBoss[actBoss]) nextIsBossFight = true;
            changedWave = true;
        }
    }

    public void EnemyDied()
    {
        if (inBossFight)
        {
            MusicManager.instance.PlayGameTheme();
            actBoss++;
            inBossFight = false;
            ChangeWave(false);
        }
        else
        {
            deadEnemies++;

            if (nextIsBossFight)
            {
                if (transform.childCount == 2 && actWave.waveInfo == null) ChangeWave(true);
            }
            else if (transform.childCount == 2 && actWave.waveInfo == null)
            {
                ChangeWave(false);
            }
        }
    }
}
