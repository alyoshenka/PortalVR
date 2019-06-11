using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PickupGun(Gun gun, VRTK.VRTK_InteractGrab grabber)
    {
        Debug.Log("Gun picked up");
    }
}
