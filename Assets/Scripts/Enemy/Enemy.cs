using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageableEnity      
{
    public enum Type { circling, waving }

    public ScoreKeeperSO sk;

    public int points;
    public Transform target;
    public GameObject bullet;
    public Type type;
    public float speed;
    public float shotTimer;
    public float radius;
    public float upSpeed = 10f;
    public float upMultiplier = 0.1f;
    public float fallTime;

    [Header("Sounds")]
    public AudioClip fireSound;
    public AudioClip shotSound;

    [HideInInspector]
    public static int currentIdx;

    [SerializeField]
    public Vector3 goalPos;

    public static List<Enemy> enemies;
    public static List<Enemy> circlingEnemies;

    public bool IsShooting { get; set; }
    public bool InPosition { get; private set; }

    float shotElapsed, fallElapsed;
    float angle; // deg
    float upVal;   

    // holders
    Vector3 newPosition;
    AudioSource a;
    // Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();       
        enemies.Add(this);

        shotElapsed = fallElapsed = 0f;
        angle = 0f;
        newPosition = transform.position;
        upVal = 0f;
        goalPos = Vector3.zero;
        InPosition = false;

        // rb = GetComponent<Rigidbody>();
        // rb.useGravity = true;
        // rb.isKinematic = false;
        gameObject.AddComponent<AudioSource>();
        a = GetComponent<AudioSource>();
        a.minDistance = 0;
        a.maxDistance = 500;
        a.rolloffMode = AudioRolloffMode.Linear;
        a.playOnAwake = false;
        a.loop = false;
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

    public void Shoot()
    {
        GameObject bul = Instantiate(bullet, transform.position, Quaternion.identity);
        bul.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        a.clip = fireSound;
        a.Play();
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

    public override Explosion TakeDamage(int damage)
    {
        a.clip = shotSound;
        a.Play();
        return base.TakeDamage(damage);
    }

    public override void OnDeath()
    {
        Instantiate(deathEffect.gameObject, transform.position, transform.rotation);
        FindObjectOfType<EnemyAttackManager>().toRemove.Add(transform);
        sk.AddPoints(points); // change
        enemies.Remove(this);
        StartCoroutine("EOF");
    }

    IEnumerator EOF()
    {
        gameObject.SetActive(false);
        a.enabled = true;
        yield return new WaitForSeconds(deathEffect.GetComponent<AudioSource>().clip.length); // im sorry   
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