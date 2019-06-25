using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VanillaPickup : PickupObject
{
    public override void ApplyEffect(GameObject target)
    {
        Debug.Log(description);
    }
}
