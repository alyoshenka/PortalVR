using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupGenerator : MonoBehaviour
{
    public AudioClip generationSound;
    AudioSource a;

    void Start()
    {
        a = GetComponent<AudioSource>();
        a.loop = false;
        a.playOnAwake = false;
        a.clip = generationSound;
    }

    public void GeneratePickup(PickupObjectUI pou)
    {
        Debug.Log("generate " + pou.name);

        GameObject g = Instantiate(pou.obj.gameObject, transform.position, Quaternion.identity);
        a.Play();
    }
}
