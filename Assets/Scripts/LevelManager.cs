using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// im sorry
public class LevelManager : MonoBehaviour
{
    public float enemyIncrease;
    public float intervalDecrease;
    // public float timeDecrease;
    public float speedIncrease;
    public float waitDecrease;
    public float shotIncrease;
    public float shotTimeIncrease;

    public int Level { get; private set; }

    EnemyAttackManager eam;

    // Start is called before the first frame update
    void Start()
    {
        Level = 0;
        eam = FindObjectOfType<EnemyAttackManager>();
    }

    public void NextLevel()
    {
        // reverse
        eam.circleInterval /= 1 - (intervalDecrease * Level);
        eam.swapInterval /= 1 - (intervalDecrease * Level);
        eam.zoomInterval /= 1 - (intervalDecrease * Level);
        eam.circleMoveSpeed /= 1 + (speedIncrease * Level);
        eam.circleRotateSpeed /= 1 + (speedIncrease * Level);
        eam.swapSpeed /= 1 + (speedIncrease * Level);
        eam.zoomSpeed /= 1 + (speedIncrease * Level);
        eam.zoomWait /= 1 - (waitDecrease * Level);
        eam.shotTime /= 1 - (shotTimeIncrease * Level);

        Level++;

        // apply
        eam.circleInterval *= 1 - (intervalDecrease * Level);
        eam.swapInterval *= 1 - (intervalDecrease * Level);
        eam.zoomInterval *= 1 - (intervalDecrease * Level);
        eam.circleMoveSpeed *= 1 + (speedIncrease * Level);
        eam.circleRotateSpeed *= 1 + (speedIncrease * Level);
        eam.swapSpeed *= 1 + (speedIncrease * Level);
        eam.zoomSpeed *= 1 + (speedIncrease * Level);
        eam.zoomWait *= 1 - (waitDecrease * Level);
        eam.shotTime *= 1 - (shotTimeIncrease * Level);

        GameObject.FindObjectOfType<Spawner>().enemyCount += (int)enemyIncrease; // not final
    }
}
