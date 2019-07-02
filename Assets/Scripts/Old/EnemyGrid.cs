using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrid : MonoBehaviour
{
    public float offset;
    public GameObject marker;
    public float shotTimer;

    public static Vector3[,] positions;
    public static List<Enemy> enemies;

    static List<Enemy> waitingEnemies;
    static List<Enemy> readyEnemies;
    static Enemy currentEnemy;
    static int curEnemIdx;

    static int height;
    static int width;
    float shotElapsed;
    static bool ready;

    Vector3 orig;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
        waitingEnemies = new List<Enemy>();
        readyEnemies = new List<Enemy>();
        shotElapsed = 0f;
        ready = false;
        curEnemIdx = 0;
    }

    public void MakeNextLevel(int _width, int _height)
    {
        enemies.Clear();
        height = _height;
        width = _width;
        orig = transform.position;
        positions = new Vector3[width, height];
        for (int x = 0; x < width; x++)           
        {
            for (int y = height - 1; y >= 0; y--)
            {
                positions[x, y] = new Vector3(x * offset + orig.x, y * offset + orig.y, orig.z);
                Instantiate(marker, positions[x, y], Quaternion.identity);
            }
        }
    }

    public static Vector3 IntToV3(int tryIdx)
    {

        if(tryIdx == 0) { return positions[0, 0]; }

        int x = tryIdx / width;
        int y = tryIdx % height;

        return positions[y, x];
    }

    public static void MoveToReady(Enemy e)
    {
        if (readyEnemies.Contains(e)) { return; }

        readyEnemies.Add(e);
        waitingEnemies.Remove(e);
        if (waitingEnemies.Count <= 0) { ready = true; }
    }

    public static void Spawn(Enemy e)
    {
        waitingEnemies.Add(e);
    }
}
