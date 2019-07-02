﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public float speed;
    public int damage;
    public float lifetime;
    public AudioClip hitSound;

    AudioSource a;
    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        elapsedTime = 0f;
        gameObject.AddComponent<AudioSource>();
        a = GetComponent<AudioSource>();
        a.clip = hitSound;
        a.playOnAwake = false;
        a.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifetime) { Destroy(gameObject); }

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable dam = other.gameObject.GetComponent<IDamageable>();
        if(null != dam)
        {
            Explosion otherExpl = dam.TakeDamage(damage);
            Instantiate(otherExpl, transform.position, transform.rotation);
        }
        else { Instantiate(explosion, transform.position, transform.rotation); }
        a.Play();
        Destroy(gameObject, hitSound.length); // use object pool
    }
}
