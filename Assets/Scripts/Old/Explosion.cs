using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    public float lifetime;

    float elasedTime;

    // Start is called before the first frame update
    void Start()
    {
        elasedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        elasedTime += Time.deltaTime;
        if(elasedTime >= lifetime) { Destroy(gameObject); }
    }
}
