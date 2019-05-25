using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Level { get; private set; }
    public EnemySpawner spawnerInstance;
    [SerializeField]
    public static EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        Level = 0;
        spawner = spawnerInstance;
    }

    public static void StartNextLevel()
    {
        spawner.InitLevel();
    }
}
