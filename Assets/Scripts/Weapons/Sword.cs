using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Sword : VRTK_InteractableObject
{
    public int damage = 20;
    [SerializeField]
    public Vector3 grabPos;
    [SerializeField]
    public Vector3 grabRot;

    bool active;
    bool nextFrame;
    VRTK_ControllerReference hand;
    AudioSource a;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        nextFrame = false;
        hand = null;
        a = GetComponent<AudioSource>();
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

    public void OnTriggerEnter(Collider other)
    {
        a?.Play();
    }

    public void OnTriggerExit(Collider other)
    {
        IDamageable d = other.GetComponent<IDamageable>();
        if(null != d)
        {
            Explosion e = d.TakeDamage(damage);
            Instantiate(e, other.transform.position, other.transform.rotation);
        }
    }
}
