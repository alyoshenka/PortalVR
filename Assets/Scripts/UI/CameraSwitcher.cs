using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera vrCam;
    List<Camera> cams;
    int currentCam;

    // Start is called before the first frame update
    void Start()
    {
        cams = new List<Camera>(GameObject.FindObjectsOfType<Camera>());
        cams.RemoveAll(c => c.gameObject.CompareTag("MirrorCam"));
        foreach(Camera c in cams) { c.enabled = false; }
        cams.Add(vrCam);
        currentCam = cams.IndexOf(vrCam);
        cams[currentCam].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (cams.IndexOf(vrCam) != currentCam) { cams[currentCam].enabled = false; }
            currentCam++;
            if(currentCam >= cams.Count) { currentCam = 0; }
            cams[currentCam].enabled = true;
        }
    }
}
