using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageableEnity
{
    public enum Type { circling }
    public enum Stage { shooting, resting, circling }

    public int points;
    public Transform target;
    public GameObject bullet;
    public Type type;
    public float speed;
    public float shotTimer;
    public float radius;

    public static Stage stage = Stage.circling; // how do we want to control stages
    public bool IsShooting { get; set; }

    float shotElapsed;
    float angle; // deg

    // holders
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        shotElapsed = 0f;
        angle = 0f;
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case Stage.shooting:
                Shoot();
                break;
            case Stage.resting:
                Rest();
                break;
            case Stage.circling:
                Circle();
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    void Shoot()
    {
        switch (type)
        {
            case Type.circling:
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    void Rest()
    {

    }

    void Circle()
    {
        // figure out best way to do this
        /*
        angle += Time.deltaTime;
        newPosition.x += Mathf.Cos(angle * Mathf.Deg2Rad * radius);
        newPosition.z += Mathf.Sin(angle * Mathf.Deg2Rad * radius);
        newPosition = newPosition.normalized;
        newPosition *= speed;
        transform.position = newPosition;
        */

        //transform.Rotate(angle * Time.deltaTime, Vector3.up, Vector3.zero);
        transform.RotateAround(Vector3.zero, transform.up, speed * Time.deltaTime);
    }

    public override void OnDeath()
    {
        Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
        GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().AddPoints(points);
        Destroy(gameObject);
    }

}
