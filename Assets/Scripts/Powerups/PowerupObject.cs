using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
[RequireComponent(typeof(VRTK_ChildOfControllerGrabAttach))]
[RequireComponent(typeof(VRTK_SwapControllerGrabAction))]
public abstract class PowerupObject : VRTK_InteractableObject, IComparable
{
    public int cost;
    public string title; // name
    public string description;

    public ParticleSystem explosion;
    public float minY = -5;
    public float notSeenSeconds = 5;

    // public InventoryManager IM { get; set; }

    protected bool used;

    void Start()
    {
        used = false;
    }

    public int CompareTo(object obj)
    {
        if (null == obj) { return 1; }

        PowerupObject po = obj as PowerupObject;

        if (cost < po.cost) { return -1; }
        else if (po.cost < cost) { return 1; }
        else { return 0; }
    }

    public abstract void ApplyEffect();

    public abstract void ReverseEffect();

    protected void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    protected override void Update()
    {
        base.Update();
        if (transform.position.y < minY) { StartCoroutine("WaitUntilNotSeen"); }
    }

    IEnumerator WaitUntilNotSeen()
    {
        yield return new WaitForSeconds(notSeenSeconds);
        Destroy(gameObject);
    }
}
