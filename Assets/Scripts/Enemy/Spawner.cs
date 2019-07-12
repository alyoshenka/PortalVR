using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    [Tooltip("The type of enemy to spawn")]
    public List<Enemy> enemies;
    [Tooltip("The number of enemies to spawn")]
    public int enemyCount;
    [Tooltip("The rate at which to spawn enemies")]
    public float spawnTime;

    public SpawnState spawnState { private get; set; }

    int spawnedCount;
    float spawnElapsed;

    void Start()
    {
        spawnedCount = 0;
        spawnElapsed = 0f;
    }

    public void Spawn(float deltaTime)
    {
        if (spawnedCount >= enemyCount)
        {
            spawnState.RemoveSpawner();
            return;
        }

        spawnElapsed += deltaTime;
        if (spawnElapsed >= spawnTime)
        {
            // Spawn();
            Enemy e = Instantiate(enemies[Random.Range(0, enemies.Count)].gameObject, transform.position, Quaternion.identity).GetComponent<Enemy>();
            Enemy.circlingEnemies.Add(e);

            spawnedCount++;
            spawnTime = 0f;
        }        
    }

    public void Reset()
    {
        spawnedCount = 0;
    }
}
