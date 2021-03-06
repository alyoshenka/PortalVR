﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
public class HeadModel : VRTK_InteractableObject
{
    public int layer = 14;

    Player p;

    private void Start()
    {
        p = FindObjectOfType<Player>();
    }

    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);
        DoTheThings();       
    }

    public void DoTheThings()
    {
        Debug.Log("starting to use " + gameObject.name);

        if (null == HeadModelSetter.head)
        {
            p.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            Destroy(HeadModelSetter.head.gameObject);
        }

        GameObject h = Instantiate(gameObject, p.transform.position,
            p.transform.rotation, p.transform.parent);
        h.GetComponent<VRTK_InteractObjectHighlighter>().enabled = false;
        // foreach(GameObject g in h.transform.GetComponentsInChildren<GameObject>()) { g.layer = layer; }
        h.gameObject.layer = layer;
        //p.head = h;
        HeadModelSetter.head = h.GetComponent<HeadModel>();

        Debug.Log("set head to " + HeadModelSetter.head.name);
    }

}
