using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTarget : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerupObject p = other.GetComponent<PowerupObject>();
        if(null != p) { p.ApplyEffect(); }
    }
}
