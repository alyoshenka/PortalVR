using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]
public class HeadModel : VRTK_InteractableObject
{
    Player p;
    static ScoreKeeperSO sk;

    private void Start()
    {
        p = FindObjectOfType<Player>();
        sk = p.sk;
        sk.Head = p.head;
    }

    public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
    {
        base.StartUsing(currentUsingObject);

        if(null == p.head)
        {
            p.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            Destroy(p.head);
        }

        GameObject h = Instantiate(gameObject, p.transform.position, 
            p.transform.rotation, p.transform.parent);

        p.head = h;
        sk.Head = h;

        Debug.Log("picked up " + gameObject.name);
    }
}
