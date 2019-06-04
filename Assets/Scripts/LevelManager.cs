using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Level { get; private set; }
    public EnemySpawner spawnerInstance;
    public EnemyGrid GridManager;
    public static EnemyGrid gridManager;
    [SerializeField]
    public static EnemySpawner spawner;

    static List<List<GameObject>> allEnemies;

    // Start is called before the first frame update
    void Start()
    {
        Level = 0;
        spawner = spawnerInstance;
        allEnemies = new List<List<GameObject>>();
        gridManager = GridManager;
    }

    static void StartNextLevel()
    {
        Level++;
        gridManager.MakeNextLevel(10, 10);
        spawner.InitLevel();
    }

    static void EndLevel()
    {
        Debug.Log("ended level");
    }

    public static void AddEnemyList(List<GameObject> l)
    {
        allEnemies.Add(l);
    }

    public static void RemoveEnemyList(List<GameObject> l)
    {
        allEnemies.Remove(l);
        if(l.Count <= 0)
        {
            EndLevel();
        }
    }

    public static void TryNextLevel()
    {
        StartNextLevel();
    }
}
