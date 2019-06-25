using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.Find("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // player.OnTriggerEnter(other);
    }
}
