﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DamageableEnity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        Debug.Log("you died");
    }
}
