using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // vrtk interactable
public abstract class PickupObject : MonoBehaviour, IPickup, IComparable
{
    public int cost;
    public string title; // name
    public string description;

    public abstract void ApplyEffect(GameObject target);

    public int CompareTo(object obj)
    {
        if(null == obj) { return 1; }

        PickupObject po = obj as PickupObject;

        if(cost < po.cost) { return -1; }
        else if(po.cost < cost) { return 1; }
        else { return 0; }
    }
}

