using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Tooltip("How far the enemies are from each other")]
    public float enemyOffset;

    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3[] Initialize(int enemyCount)
    {
        Vector3[] enemyPositions = new Vector3[enemyCount];
        Vector3 start = transform.position;
        Vector3 assign = new Vector3();
        start.x -= 0.5f * enemyCount * enemyOffset;
        start.y -= 0.5f * enemyCount * enemyOffset;
        int dimension = (int)Mathf.Ceil(Mathf.Sqrt(enemyCount));
        int x, y;
        x = y = 0;
        for(int i = 0; i < enemyCount; i++)
        {
            x++;
            if(x >= dimension)
            {
                x = 0;
                y++;
            }
            assign = start;
            assign.x += x * enemyOffset;
            assign.y -= y * enemyOffset;
            enemyPositions[i] = assign;
            
            Instantiate(marker, Vector3.zero, Quaternion.identity);
        }

        return enemyPositions;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
