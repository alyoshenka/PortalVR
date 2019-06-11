using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Gun : VRTK.VRTK_InteractableObject
{
    public GameObject bullet;
    public float firingSpeed;
    [SerializeField]
    public Vector3 grabPos;
    [SerializeField]
    public Vector3 grabRot;
    // public float aimDist;
    // public GameObject crosshair;
    [Range(0, 1)]
    public float shotStrength;

    bool active;
    bool nextFrame;
    VRTK_ControllerReference hand;
    // Ray aim;

   //  RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        nextFrame = false;
        hand = null;
        // aim = new Ray(transform.position, transform.forward);
        // crosshair.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        if (!active) { return; }

        if (nextFrame) { SetOnNextFrame(); }
        //if (Physics.Raycast(aim, out hit, aimDist))
        //{
        //    crosshair.transform.position = hit.transform.position;
        //    crosshair.SetActive(true);
        //}
        //else { crosshair.SetActive(false); }
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        hand = VRTK_ControllerReference.GetControllerReference(currentGrabbingObject.gameObject);
        Debug.Log(currentGrabbingObject.gameObject.name);

        currentGrabbingObject.controllerEvents.TriggerPressed += Shoot;
        active = true;
        nextFrame = true;
    }

    private void SetOnNextFrame()
    {
        nextFrame = false;
        transform.localPosition = grabPos;
        transform.localRotation = Quaternion.Euler(grabRot);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null)
    {
        base.Ungrabbed(previousGrabbingObject);

        hand = null;
        previousGrabbingObject.controllerEvents.TriggerPressed -= Shoot;
        active = false;
    }
    
    void Shoot(object sender, ControllerInteractionEventArgs e)
    {
        // VRTK_ControllerHaptics.CancelHapticPulse(hand);
        GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
        // VRTK_ControllerHaptics.TriggerHapticPulse(hand, shotStrength);
    }
    
}
