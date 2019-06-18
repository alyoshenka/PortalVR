using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : DamageableEnity
        
{    public enum Type { circling, waving }

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

    [SerializeField]
    public Vector3 goalPos;

    public static List<Enemy> enemies;
    public static List<Enemy> circlingEnemies;

    public bool IsShooting { get; set; }
    public bool InPosition { get; private set; }

    float shotElapsed;
    float angle; // deg
    float upVal;   

    // holders
    Vector3 newPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();       
        enemies.Add(this);
        Debug.Log(enemies.Count);

        shotElapsed = 0f;
        angle = 0f;
        newPosition = transform.position;
        upVal = 0f;
        goalPos = Vector3.zero;
        InPosition = false;
    }

    public static void Initialize()
    {
        if (null == enemies) { enemies = new List<Enemy>(); }
        else { enemies.Clear(); }
        if (null == circlingEnemies) { circlingEnemies = new List<Enemy>(); }
        else { circlingEnemies.Clear(); }
    }

    public void Move(float deltaTime)
    {
       if(type == Type.circling) { Circle(); }
       else { Wave(); }
    }

    public void Shoot(float dt)
    {
        shotElapsed += dt;
        if (shotElapsed >= shotTimer)
        {
            shotElapsed = 0f;
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.identity);
            bul.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
 
    }

    void Rest() { }

    void Circle()
    {    
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
        enemies.Remove(this);
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
            InPosition = true;
            circlingEnemies.Remove(this);
        }       
    }
}
