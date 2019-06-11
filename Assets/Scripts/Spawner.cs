using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
    [Tooltip("The type of enemy to spawn")]
    public Test enemy;
    [Tooltip("The number of enemies to spawn")]
    public int enemyCount;
    [Tooltip("The rate at which to spawn enemies")]
    public float spawnTime;

    EnemyStateMachine_bad stateMachine;
    bool isSpawning;
    int spawnedCount;
    float spawnElapsed;
    List<Test> enemies;

    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
        spawnElapsed = 0;
        enemies = new List<Test>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) { return; }

        switch (stateMachine.stage)
        {
            case EnemyStateMachine.State.circle:
                SpawnEnemy();
                break;
            case EnemyStateMachine_bad.State.arrange:
                UpdateEnemies();
                break;
            case EnemyStateMachine_bad.State.fire:
                break;
            case EnemyStateMachine_bad.State.process:
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }

        spawnElapsed += Time.deltaTime;
        if(spawnElapsed >= spawnTime) { SpawnEnemy(); }
    }

    public void StartSpawning(EnemyStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        isSpawning = true;
    }

    void SpawnEnemy()
    {
        spawnElapsed = 0f;
        spawnedCount++;        

        GameObject newEnemy = Instantiate(enemy.gameObject, transform.position, transform.rotation);
        Test enemyScript = newEnemy.GetComponent<Test>();
        enemies.Add(enemyScript);
        stateMachine.AddEnemy(enemyScript);
        stateMachine.AddToWaiting(enemyScript);

        if (spawnedCount >= enemyCount)
        {
            isSpawning = false;
            // remove spawner from active list
            stateMachine.MoveToWaiting(this);
        }
    }

    void UpdateEnemies()
    {
        foreach (Test e in enemies) { e.Arrange(); }
    }
    */
}
