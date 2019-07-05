using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
public class AutomaticGun : VRTK_InteractableObject
{
    public GameObject bullet;
    public float shotTime;
    [SerializeField]
    public Vector3 grabPos;
    [SerializeField]
    public Vector3 grabRot;
    [Range(0, 1)]
    public float shotStrength;
    public AudioClip fireSound;

    protected bool active;
    protected bool isDown;
    protected bool nextFrame;
    protected VRTK_ControllerReference hand;
    protected Transform firePoint;
    protected float fireElapsed;

    protected void Start()
    {
        active = false;
        nextFrame = false;
        hand = null;
        firePoint = transform.Find("FirePoint");
        fireElapsed = 0f;
        isDown = false;
    }

    protected override void Update()
    {
        base.Update();

        if (!active) { return; }


        if (isDown)
        {
            fireElapsed += Time.deltaTime;
            if(fireElapsed >= shotTime) { Shoot(); }
        }

        if (nextFrame) { SetOnNextFrame(); }
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        hand = VRTK_ControllerReference.GetControllerReference(currentGrabbingObject.gameObject);

        currentGrabbingObject.controllerEvents.TriggerPressed += ShootStart;
        currentGrabbingObject.controllerEvents.TriggerReleased += ShootEnd;
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
        previousGrabbingObject.controllerEvents.TriggerPressed -= ShootStart;
        previousGrabbingObject.controllerEvents.TriggerReleased -= ShootEnd;
        active = false;
    }

    void ShootStart(object sender, ControllerInteractionEventArgs e)
    {
        isDown = true;
    }

    void Shoot()
    {
        GameObject bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
        fireElapsed = 0f;
    }

    void ShootEnd(object sender, ControllerInteractionEventArgs e)
    {
        isDown = false;
        fireElapsed = 0f;
    }

}
