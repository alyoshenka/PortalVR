using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHealth : PowerupObject
{
    public override void ApplyEffect()
    {
        Player p = FindObjectOfType<Player>();
        p.RefillHealth(p.maxHealth);
    }

    public override void ReverseEffect()
    {
        // no
    }
}
