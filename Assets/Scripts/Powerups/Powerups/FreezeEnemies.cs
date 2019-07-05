using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemies : PowerupObject
{
    public float freezeTime = 5f;

    EnemyStateMachine a;

    public override void ApplyEffect()
    {
        if (used) { return; }
        used = true;

        Explode();

        StartCoroutine("Freeze");
        Debug.Log("explode");
    }

    public override void ReverseEffect()
    {
        a.enabled = true;
        Destroy(gameObject);
    }

    IEnumerator Freeze()
    {
        a = FindObjectOfType<EnemyStateMachine>();
        a.enabled = false;

        for(float i = 0; i < freezeTime; i += Time.deltaTime) { yield return null; }

        ReverseEffect();
    }
}