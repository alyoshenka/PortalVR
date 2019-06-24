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

    #region SwapStages

    public void One(Transform t, Vector3 pos, float swapSpeed, float swapDepth)
    {
        if(null == t) { swapStage = 2; }
        else
        {
            t.position += t.forward * Time.deltaTime * swapSpeed;
            if (Vector3.Distance(t.position, pos) >= swapDepth) { swapStage = 2; }
        }       
    }

    public bool Two(Transform t, Vector3 pos, float swapSpeed, float swapDepth)
    {
        if(null == t) { return true; }

        t.position += t.forward * Time.deltaTime * swapSpeed;
        return Vector3.Distance(t.position, pos) > swapDepth;
    }

    public bool Three(Transform t, Vector3 forward, float swapSpeed, float eps)
    {
        if(null == t) { return true; }

        t.LookAt(forward);
        t.position += t.forward * swapSpeed * Time.deltaTime;
        return Vector3.Distance(t.position, forward) < eps;
    }

    public bool Four(Transform t, Vector3 pos, float swapSpeed, float eps)
    {
        if(null == t) { return true; }        

        t.LookAt(pos);
        t.position += t.forward * swapSpeed * Time.deltaTime;
        return Vector3.Distance(t.position, pos) < eps;
    }

    #endregion
}

public struct CircleHolder
{
    public Transform entity;
    public float radius;
    public Vector3 center, origPos;
    public float curDist;
    public int circleStage;
    public int cnt;

    public void Initialize(Transform _ent, float rad, Vector3 cent)
    {
        entity = _ent;
        origPos = entity.position;
        radius = rad;
        center = cent;
        circleStage = 1;
        cnt = 0;
    }
}

public struct ZoomHolder
{
    public Transform entity;
    public Vector3 center, origPos;
    public float waitElapsed;
    public int zoomStage;

    public void Initialize(Transform ent, Vector3 cent)
    {
        entity = ent;
        center = cent;
        origPos = entity.position;
    }
}
