using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Gun : VRTK.VRTK_InteractableObject
{
    public GameObject bullet;
    public float firingSpeed;

    VRTK_ControllerEvents left;
    VRTK_ControllerEvents right;

    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        left = HandRefs.Left;
        right = HandRefs.Right;
        canShoot = false;
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        Debug.Log("gun grabbed");

        // GunHolder.PickupGun(this, currentGrabbingObject);
        // left.TriggerPressed += OnTriggerPress;
        // right.TriggerPressed += OnTriggerPress;

        currentGrabbingObject.controllerEvents.TriggerPressed += Shoot;
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
    {
        base.Ungrabbed(previousGrabbingObject);

        Debug.Log("gun dropped");

        // left.TriggerPressed -= OnTriggerPress;
        // right.TriggerPressed -= OnTriggerPress;

        previousGrabbingObject.controllerEvents.TriggerPressed -= Shoot;
    }
    

    void OnTriggerPress(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("trigger");
        // Shoot(); // should only call if subscribed to event
        // if (canShoot) { Shoot(); }
    }

    
    void Shoot(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("shooting");

        GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
    }
    
}
