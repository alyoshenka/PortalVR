using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("How far the enemies are from each other")]
    public float enemyOffset;
    public GameObject marker;

    public void Initialize(List<Enemy> enemies)
    {
        Vector3[] enemyPositions = new Vector3[enemies.Count];
        Vector3 start, assign;
        assign = start = transform.position;
        int dimension = Mathf.CeilToInt(Mathf.Sqrt(enemies.Count));
        start.x -= 0.5f * dimension * enemyOffset;
        start.y -= 0.5f * dimension * enemyOffset;
        
        float x, y;
        x = start.x;
        y = start.y;
        for(int i = 0; i < enemies.Count; i++)
        {
            x += enemyOffset;
            if(i % dimension == 0)
            {
                x = start.x;
                y += enemyOffset;                
            }
            
            assign = start;
            assign.x += x;
            assign.y = y;
            
            enemyPositions[i] = assign;            
            Instantiate(marker, assign, Quaternion.identity);
        }

        for (int i = 0; i < enemies.Count; i++) { enemies[i].goalPos = enemyPositions[i]; }
    }
}
