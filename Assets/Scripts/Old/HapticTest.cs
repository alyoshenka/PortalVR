using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HapticTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            VRTK_ControllerReference r = VRTK_ControllerReference.GetControllerReference(
                    GameObject.Find("UnityXR").GetComponent<VRTK_SDKSetup>().controllerSDK.GetControllerModel(SDK_BaseController.ControllerHand.Right));
            Debug.Log(r.scriptAlias.name);
            VRTK_ControllerHaptics.TriggerHapticPulse(r, 100000000f, 1000000000000f, 1f);
            Debug.Log("trying");
        }
    }
}
