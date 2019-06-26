using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilHealth : PowerupObject
{
    [Range(0, 1)]
    [Tooltip("of max health")]
    public float healthRefillPercentage;

    public override void ApplyEffect()
    {
        Player p = FindObjectOfType<Player>();
        p.RefillHealth((int)(p.maxHealth * healthRefillPercentage));
    }

    public override void ReverseEffect()
    {
        // no
    }
}
