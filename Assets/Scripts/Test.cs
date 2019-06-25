using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // enemy types
    public enum Type { }
    // current enemy state
    public enum State { }

    [Tooltip("Movement speed")]
    public float speed;
    [Tooltip("Angular speed")]
    public float rotSpeed;
    [Tooltip("Am I rotating clockwise?")]
    public bool isCW;
    [Tooltip("The height to float at")]
    public float range;
    [Tooltip("Am I bobbing up and down?")]
    public float isBobbing;
    [Tooltip("The height range to bob")]
    public float heightRange;
    [Tooltip("The bob speed")]
    public float bobSpeed;

    [HideInInspector]
    public Vector3 goalPos;

    // values

    // components
    Rigidbody rb;

    // reusable
    Vector3 rotation, movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotation = movement = new Vector3();
    }

    void Update()
    {
        
        Circle();
    }

    #region State Updates

    // get to circling pos
    public void Setup()
    {

    }

    // circle around
    public void Circle()
    {
        rotation.y = rotSpeed * Time.deltaTime;
        if (!isCW) { rotation.y *= -1; }
        transform.Rotate(rotation);
        movement.z = speed * Time.deltaTime;
        // rb.AddRelativeForce(movement);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // arrange
    public void Arrange()
    {
        if(Vector3.Distance(transform.position, goalPos) > 0.1f) { transform.position += transform.forward * speed * Time.deltaTime; }        
    }

    // fire
    public void Fire()
    {

    }

    #endregion

}
