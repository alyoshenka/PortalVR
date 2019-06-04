using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageableEnity
{
    public enum Type { circling, waving }
    public enum Stage { shooting, resting, circling, arranging }

    public int points;
    public Transform target;
    public GameObject bullet;
    public Type type;
    public float speed;
    public float shotTimer;
    public float radius;
    public float upSpeed = 10f;
    public float upMultiplier = 0.1f;

    [HideInInspector]
    public static int currentIdx;

    public static Stage stage = Stage.circling; // how do we want to control stages
    public bool IsShooting { get; set; }

    float shotElapsed;
    float angle; // deg
    float upVal;
    Vector3 goalPos;

    // holders
    Vector3 newPosition;

    // public static List<Enemy> enemies;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // if(null == enemies) { enemies = new List<Enemy>(); }
        // enemies.Add(this);

        shotElapsed = 0f;
        angle = 0f;
        newPosition = transform.position;
        upVal = 0f;
        goalPos = EnemyGrid.IntToV3(currentIdx++);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { stage = Stage.arranging; }

        shotElapsed += Time.deltaTime;

        switch (stage)
        {
            case Stage.shooting:
                Shoot();
                break;
            case Stage.resting:
                Rest();
                break;
            case Stage.circling:
                Move();
                break;
            case Stage.arranging:
                Arrange();
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    void Move()
    {
        switch (type)
        {
            case Type.circling:
                Circle();
                break;
            case Type.waving:
                Wave();
                break;
            default:
                Debug.LogError("Invalid State");
                break;
        }
    }

    public void Shoot()
    {
        // GameObject bul = Instantiate(bullet, transform.position, Quaternion.identity);
        // bul.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        // shotElapsed += Time.deltaTime;

        if (shotElapsed >= shotTimer)
        {
            shotElapsed = 0f;
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.identity);
            bul.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
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

    void Wave()
    {
        Circle();
        Vector3 pos = transform.position;
        upVal += Time.deltaTime * upSpeed;
        pos.y += Mathf.Sin(upVal) * upMultiplier;
        transform.position = pos;
    }

    public override void OnDeath()
    {
        Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
        GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().AddPoints(points); // change
        // enemies.Remove(this);
        // if(enemies.Count <= 0) { Debug.Log("no more enemies"); }
        EnemySpawner.RemoveAliveEntity(gameObject);
        Destroy(gameObject);        
    }

    public void Arrange()
    {
        if(Vector3.Distance(transform.position, goalPos) > 0.1f)
        {
            transform.LookAt(goalPos);
            transform.position += transform.forward * (speed / 10) * Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            EnemyGrid.MoveToReady(this);
        }
        
    }
}
