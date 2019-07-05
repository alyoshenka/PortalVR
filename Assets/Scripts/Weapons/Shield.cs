using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Shield : VRTK_InteractableObject
{
    [SerializeField]
    public Vector3 grabPos;
    [SerializeField]
    public Vector3 grabRot;

    bool active;
    bool nextFrame;
    VRTK_ControllerReference hand;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        nextFrame = false;
        hand = null;
    }

    protected override void Update()
    {
        base.Update();
        if (!active) { return; }

        if (nextFrame) { SetOnNextFrame(); }
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject = null)
    {
        base.Grabbed(currentGrabbingObject);

        hand = VRTK_ControllerReference.GetControllerReference(currentGrabbingObject.gameObject);
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
        active = false;
    }
}
