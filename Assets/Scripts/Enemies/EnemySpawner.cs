using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // a list of groups of enemies to spawn in this wave
        public int waveQuantity; // total number of enemies to spawn in this wave
        public float spawnInterval; // the time interval between each enemy spawn
        public int spawnCount; // the number of enemies spawned so far in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // the number of enemies to spawn this wave
        public int spawnCount; // the number of enemies of this type spawned so far in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // list of all the waves in the game
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer; // timer used to determine when to spawn next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed; // the max number of enemies allowed on the map
    public bool maxEnemiesReached = false; // a flag indicating if the max number of enemies has been reached
    public float waveInterval; // the interval between each wave

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // a list to store all relative spawn points of enemies

    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuantity();
    }

    void Update()
    {
        //check if the wave has ended and the next wave should start
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        // checks to see if it's time to spawn the next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        // there are more waves to start after the current wave, move on to the nextwave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuantity();
        }
    }

    void CalculateWaveQuantity()
    {
        int currentWaveQuantity = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuantity += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuantity = currentWaveQuantity;
    }

    void SpawnEnemies()
    {
        // check if the min number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuantity && !maxEnemiesReached)
        {
            // spawn each type of enmey until the quantity has been filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // check if the min number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    // spawn the enemy at a random position close to the player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        // reset the maxenemiesreached flag if the number of enemies alive has dropped below the max amount
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    // call this function when an enemy is killed
    public void OnEnemyKilled()
    {
        // decrement the number of enemies alive
        enemiesAlive--;
    }
}
