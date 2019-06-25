using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRefs : MonoBehaviour
{
    public VRTK.VRTK_ControllerEvents left;
    public VRTK.VRTK_ControllerEvents right;

    public static VRTK.VRTK_ControllerEvents Left;
    public static VRTK.VRTK_ControllerEvents Right;

    private void Start()
    {
        Left = left;
        Right = right;
    }
}
