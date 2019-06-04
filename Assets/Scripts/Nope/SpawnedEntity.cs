using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEntity : MonoBehaviour
{
    public GameObject spawnObject;
    public int spawnNumber;
    public float spawnInterval;

    public EnemySpawner Spawner { get; set; }
    public bool Active { get; set; }

    int spawnCounter;
    float spawnElapsed;

    void Start()
    {
        Active = false;
        spawnElapsed = Random.Range(0f, spawnInterval);
        spawnCounter = 0;
    }

    void Update()
    {
        if (!Active) { return; }

        spawnElapsed += Time.deltaTime;
        if (spawnElapsed >= spawnInterval) { Spawn(); }
    }

    public void Spawn()
    {
        GameObject g = Instantiate(spawnObject.gameObject, transform.position, transform.rotation);
        EnemySpawner.AddAliveEntity(g);
        EnemyGrid.Spawn(g.GetComponent<Enemy>());
        spawnElapsed = 0;
        spawnCounter++;
        if(spawnCounter >= spawnNumber) { Active = false; } // done
    }

}
