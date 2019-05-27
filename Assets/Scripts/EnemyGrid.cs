using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrid : MonoBehaviour
{
    public float offset;
    public GameObject marker;

    public static Vector3[,] positions;

    static int height;
    static int width;

    Vector3 orig;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeNextLevel(int _width, int _height)
    {
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

        Debug.Log(x + " " + y);

        return positions[y, x];
    }
}
