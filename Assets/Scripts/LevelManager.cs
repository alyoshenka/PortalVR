using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Level { get; private set; }
    public EnemySpawner spawnerInstance;
    public EnemyGrid gridManager;
    [SerializeField]
    public static EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        Level = 0;
        spawner = spawnerInstance;
    }

    public void StartNextLevel()
    {
        Level++;
        gridManager.MakeNextLevel(10, 10);
        spawner.InitLevel();
    }
}
