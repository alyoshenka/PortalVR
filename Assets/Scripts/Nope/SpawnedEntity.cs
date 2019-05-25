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
        spawnElapsed = 0f;
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
        spawnElapsed = 0;
        spawnCounter++;
        if(spawnCounter >= spawnNumber) { Active = false; } // done
    }
}
