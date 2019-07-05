using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBoard : PowerupObject
{
    public float clearTime;
    EnemyStateMachine e;

    public override void ApplyEffect()
    {
        if (used) { return; }
        used = true;

        Explode();

        e = FindObjectOfType<EnemyStateMachine>();
        e.enabled = false;
        StartCoroutine("ClearAll");
    }

    IEnumerator ClearAll()
    {
        float time = clearTime / Enemy.enemies.Count;
        while(Enemy.enemies.Count > 0)
        {
            Enemy e = Enemy.enemies[0];
            e.sk.AddPoints(-e.points);

            e.OnDeath(); // kill off one by one
            yield return new WaitForSeconds(time);
        }
        e.enabled = enabled = true;
        Destroy(gameObject);
    }

    public override void ReverseEffect() { } // no
}
