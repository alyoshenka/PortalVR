using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadModelSetter : MonoBehaviour
{
    public static HeadModel head;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    public void StartTheThings()
    {
        Debug.Log("setting head");
        head?.DoTheThings();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)){ head?.StartUsing(null); }
    }
}
