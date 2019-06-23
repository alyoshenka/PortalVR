using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SwapHolder
{
    public Transform ent1, ent2;
    public Vector3 ent1pos, ent2pos, ent1forward, ent2forward;
    public int swapStage;

    public void Initialize(Transform _ent1, Transform _ent2, float swapDepth, float swapDepthDifference)
    {
        ent1 = _ent1;
        ent2 = _ent2;

        ent1pos = ent1.position;
        ent2pos = ent2.position;
        ent1forward = ent1pos + ent1.forward * (swapDepth + swapDepthDifference);
        ent2forward = ent2pos + ent2.forward * swapDepth;
        swapStage = 1;
    }
}
