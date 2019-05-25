using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyStage { init, circle, shoot }
    public static EnemyStage enemyStage;
    public static bool levelGoing;

    public List<SpawnedEntity> entities;

    static List<GameObject> aliveEntities;

    // [HideInInspector]
    // public List<SpawnedEntity> activeEntities;

    // Start is called before the first frame update
    void Start()
    {
        levelGoing = false;
        aliveEntities = new List<GameObject>();
        // activeEntities = new List<SpawnedEntity>();
        // foreach(SpawnedEntity ent in entities) { ent.Spawner = this; }
    }

    public void InitLevel()
    {
        levelGoing = true;
        foreach (SpawnedEntity ent in entities) { ent.Active = true; }
    }

    public static void AddAliveEntity(GameObject go)
    {
        aliveEntities.Add(go);
    }

    public static void RemoveAliveEntity(GameObject go)
    {
        aliveEntities.Remove(go);
        if(aliveEntities.Count <= 0) { levelGoing = false; }
    }
}
