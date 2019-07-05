using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealth : PowerupObject
{
    public override void ApplyEffect()
    {
        if (used) { return; }
        used = true;

        Explode();
        Player p = FindObjectOfType<Player>();
        p.RefillHealth(p.maxHealth);
        Destroy(gameObject);
    }

    public override void ReverseEffect()
    {
        // no
    }
}
