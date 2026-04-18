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

        ProgressBarUI.instance.ActualizeBar(deadEnemies, numEnemiesUntilBoss[actBoss]);

        StartCoroutine(SpawnerBehaviour());
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
                
                    switch (actBoss)
                    {
                        case 0:
                            SpawnLittleEnemy(enemy1, pointsEnemy1);
                            break;

                        case 1:
                            int randomNum = UnityEngine.Random.Range(0, 4);
                            if (randomNum == 3) SpawnLittleEnemy(enemy1, pointsEnemy1);
                            else SpawnLittleEnemy(enemy2, pointsEnemy2);
                            break;

                        case 2:
                            int randomNum2 = UnityEngine.Random.Range(0, 4);
                            if (randomNum2 == 3) SpawnLittleEnemy(enemy1, pointsEnemy1);
                            else if (randomNum2 == 2) SpawnLittleEnemy(enemy2, pointsEnemy2);
                            else SpawnLittleEnemy(enemy3, pointsEnemy3);
                            break;
                    }

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
            DangerPanelUI.instance.ShowPanel();
        }
        else if (!nextIsBossFight)
        {
            actWave = waves[UnityEngine.Random.Range(0, waves.Count)];

            if (deadEnemies + actWave.waveInfo.Count > numEnemiesUntilBoss[actBoss]) nextIsBossFight = true;
            changedWave = true;
        }
    }

    public void EnemyDied(Vector3 position)
    {
        if (inBossFight)
        {
            MusicManager.instance.PlayGameTheme();
            
            actBoss++;
            inBossFight = false;

            if (actBoss < 3)
            {
                ProgressBarUI.instance.ActualizeBar(deadEnemies, numEnemiesUntilBoss[actBoss]);
            
                ChangeWave(false);
            }
            else
            {
                float numWaves = 30;
                string letter = "s";
                if (numWaves > 80) letter = "e";
                else if (numWaves > 70) letter = "d";
                else if (numWaves > 60) letter = "c";
                else if (numWaves > 50) letter = "b";
                else if (numWaves > 40) letter = "a";

                TrophySpawner.instance.SpawnTrophy(letter, position);
            }

        }
        else
        {
            deadEnemies++;

            bool lastEnemy = (transform.childCount == 2 && actWave.waveInfo == null);

            // Actualizar progress bar
            int num = deadEnemies;
            if (deadEnemies >= numEnemiesUntilBoss[actBoss] && !lastEnemy) num = numEnemiesUntilBoss[actBoss] - 1;
            ProgressBarUI.instance.ActualizeBar(num, numEnemiesUntilBoss[actBoss]);

            // Spawnear al jefe o a la proxima oleada
            if (nextIsBossFight)
            {
                if (lastEnemy) ChangeWave(true);
            }
            else if (lastEnemy)
            {
                ChangeWave(false);
            }
        }
    }
}
