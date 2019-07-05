using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemies : PowerupObject
{
    public float slowTime = 10f;
    [Range(0, 1)]
    public float slowPercent = 0.5f;

    EnemyAttackManager eam;

    public override void ApplyEffect() // add to list
    {
        if (used) { return; }
        used = true;

        Explode();

        eam = FindObjectOfType<EnemyAttackManager>();

        eam.swapSpeed *= slowPercent;
        eam.circleRotateSpeed *= slowPercent;
        eam.circleRotateSpeed *= slowPercent;
        eam.zoomSpeed *= slowPercent;

        StartCoroutine("Slow");
    }

    IEnumerator Slow()
    {
        for(float i = 0; i < slowTime; i += Time.deltaTime) { yield return null; }

        ReverseEffect();
    }

    public override void ReverseEffect() // remove from list
    {
        eam.swapSpeed /= slowPercent;
        eam.circleRotateSpeed /= slowPercent;
        eam.circleRotateSpeed /= slowPercent;
        eam.zoomSpeed /= slowPercent;

        Destroy(gameObject);
    }
}