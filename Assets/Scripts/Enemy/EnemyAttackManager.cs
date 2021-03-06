﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    enum Attack { swap, circle, zoom };

    #region Design Vars

    public float shotTime;

    [Header("Swap")]
    public float swapTime;
    public float swapInterval;
    [Space(5)]
    public float swapSpeed;
    public float swapDepth;
    public float swapDepthDifference;
    public float swapEpsilon;

    List<SwapHolder> swaps;
    float swapTimeElapsed, swapIntervalElapsed;

    [Header("Circle")]
    public float circleTime;
    public float circleInterval;
    [Space(5)]
    public float circleRotateSpeed;
    public float circleMoveSpeed;
    public float circleEpsilon;
    public float circleRadius;

    List<CircleHolder> circles;
    float circleTimeElapsed, circleIntervalElapsed;

    [Header("Zoom")]
    public float zoomTime;
    public float zoomInterval;
    [Space(5)]
    public float zoomRadius;
    public float zoomSpeed;
    public float zoomWait;
    public float zoomEpsilon;

    List<ZoomHolder> zooms;
    float zoomTimeElapsed, zoomIntervalElapsed;

    [HideInInspector]
    public List<Transform> toRemove;
    float shotElapsed;

    #endregion

    int attackCount;
    Attack currentAttack;

    // I'm sorry this script is so gross

    void Start()
    {
        attackCount = 3;

        circles = new List<CircleHolder>();
        swaps = new List<SwapHolder>();
        zooms = new List<ZoomHolder>();
        toRemove = new List<Transform>();
    }

    public void Initialize()
    {
        circles.Clear();
        swaps.Clear();
        zooms.Clear();

        currentAttack = ChooseRandomAttack();

        shotElapsed = 0f;
        swapTimeElapsed = swapIntervalElapsed = 0f;
        circleTimeElapsed = circleIntervalElapsed = 0f;
    }

    public void UpdateAttacks()
    {
        shotElapsed += Time.deltaTime;
        if (RunState()) { currentAttack = ChooseRandomAttack(); }
    }

    bool RunState()
    {
        foreach (Transform t in toRemove) { RemoveAll(t); }
        switch (currentAttack)
        {
            case Attack.swap:
                return SwapUpdate();
            case Attack.circle:
                return CircleUpdate();
            case Attack.zoom:
                return ZoomUpdate();
            default:
                Debug.LogError("Invalid state");
                break;
        }
        toRemove.Clear();
        return false;
    }

    Attack ChooseRandomAttack()
    {
        int prevA = (int)currentAttack;
        int newA;
        do { newA = Random.Range(0, attackCount); }
        while (newA == prevA);
        Attack newAttack = (Attack)newA;
        switch (newAttack)
        {
            case Attack.swap:
                SwapInit();
                break;
            case Attack.circle:
                CircleInit();
                break;
            case Attack.zoom:
                ZoomInit();
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }
        return newAttack;
    }

    #region Swap

    void SwapInit()
    {
        swapTimeElapsed = swapIntervalElapsed = 0f;
        swapIntervalElapsed = swapInterval;
        swaps.Clear();

        swapIntervalElapsed = swapInterval;
    }

    // return whether state is over
    bool SwapUpdate()
    {
        swapIntervalElapsed += Time.deltaTime;
        swapTimeElapsed += Time.deltaTime;

        for (int i = 0; i < swaps.Count; i++) { swaps[i] = Swap(swaps[i]); }
        swaps.RemoveAll(s => s.swapStage > 5);
        if (swapTimeElapsed >= swapTime) { return swaps.Count == 0; }

        if(swapIntervalElapsed >= swapInterval)
        {
            swapIntervalElapsed = 0f;
            int itrs = 0;
            bool contains = false;
            Transform ent1, ent2;
            do
            {
                if (++itrs > Enemy.enemies.Count) { return false; }
                ent1 = Enemy.enemies[Random.Range(0, Enemy.enemies.Count - 1)].transform;
                contains = false;
                foreach (SwapHolder s in swaps)
                {
                    if (s.ent1 == ent1 || s.ent2 == ent1)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);
            itrs = 0;
            do
            {
                if (++itrs > Enemy.enemies.Count) { return false; }
                ent2 = Enemy.enemies[Random.Range(0, Enemy.enemies.Count - 1)].transform;
                contains = false;
                foreach (SwapHolder s in swaps)
                {
                    if (s.ent1 == ent2 || s.ent2 == ent2)
                    {
                        contains = true;
                        break;
                    }
                }
            }
            while (ent1 == ent2 || contains);

            SwapHolder swap = new SwapHolder();
            swap.Initialize(ent1, ent2, swapDepth, swapDepthDifference);
            swaps.Add(swap);
        }


        return false;
    }

    SwapHolder Swap(SwapHolder s)
    {
        bool one, two;
        one = two = true;
        if (s.swapStage > 5) { Debug.Log("Invalid"); }
        else if (s.swapStage > 4)
        {
            if (null != s.ent1) { s.ent2.rotation = Quaternion.identity; }

            s.swapStage = 6;
        }
        else if (s.swapStage > 3)
        {
            if (null != s.ent1) {
                if(Vector3.Distance(s.ent1.position, s.ent2pos) > swapEpsilon)
                {
                    s.ent1.position += s.ent1.forward * swapSpeed * Time.deltaTime;
                    s.ent1.LookAt(s.ent2pos);
                    one = false;
                }
                else
                {
                    s.ent1.rotation = Quaternion.identity;
                    one = true;
                }

            }
            if (null != s.ent2)
            {
                s.ent2.LookAt(s.ent1pos);
                s.ent2.position += s.ent2.forward * swapSpeed * Time.deltaTime;
                two = Vector3.Distance(s.ent2.position, s.ent1pos) < swapEpsilon;
            }
           
            if (one && two) { s.swapStage = 5; }
        }
        else if (s.swapStage > 2)
        {
            if(null != s.ent1) {
                s.ent1.LookAt(s.ent2forward);
                s.ent1.position += s.ent1.forward * swapSpeed * Time.deltaTime;
                one = Vector3.Distance(s.ent2.position, s.ent1forward) < swapEpsilon;
            }
            if(null != s.ent2)
            {
                s.ent2.LookAt(s.ent1forward);
                s.ent2.position += s.ent2.forward * swapSpeed * Time.deltaTime;
                two = Vector3.Distance(s.ent1.position, s.ent2forward) < swapEpsilon;
            }

            if (one && two) { s.swapStage = 4; }
        }
        else if (s.swapStage > 1)
        {
            if(null != s.ent1)
            {
                s.ent1.position += s.ent1.forward * Time.deltaTime * swapSpeed;
                one = Vector3.Distance(s.ent1.position, s.ent1pos) > swapDepth;
            }
            if(null != s.ent2)
            {
                s.ent2.position += s.ent2.forward * Time.deltaTime * swapSpeed;
                two = Vector3.Distance(s.ent2.position, s.ent2pos) > swapDepth + swapDepthDifference;
            }

            if (one && two) { s.swapStage = 3; }
        }
        else
        {
            if (null == s.ent2) { s.swapStage =2; }
            else
            {
                s.ent2.position += s.ent2.forward * Time.deltaTime * swapSpeed;
                if (Vector3.Distance(s.ent2.position, s.ent2pos) >= swapDepth) { s.swapStage = 2; }
            }
        }

        return s;
    }

    #endregion

    #region Circle

    void CircleInit()
    {
        circleTimeElapsed = circleIntervalElapsed = 0f;
        circles.Clear();
    }

    bool CircleUpdate()
    {
        circleTimeElapsed += Time.deltaTime;
        circleIntervalElapsed += Time.deltaTime;
        if (shotElapsed >= shotTime)
        {
            shotElapsed = 0f;
            circles[Random.Range(0, circles.Count - 1)].entity.GetComponent<Enemy>().Shoot();
        }

        for (int i = 0; i < circles.Count; i++) { circles[i] = Circle(circles[i]); }
        circles.RemoveAll(z => z.circleStage > 3);
        if (circleTimeElapsed >= circleTime) { return circles.Count == 0; }

        if(circleIntervalElapsed >= circleInterval)
        {
            circleIntervalElapsed = 0f;

            Transform t;
            bool contains;
            int itrs = 0;
            do
            {
                if(++itrs > Enemy.enemies.Count) { return false; }
                t = Enemy.enemies[Random.Range(0, Enemy.enemies.Count - 1)].transform;
                contains = false;
                foreach (CircleHolder c in circles)
                {
                    if (t == c.entity)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);

            CircleHolder circle = new CircleHolder();
            circle.Initialize(t, circleRadius, FindObjectOfType<Player>().transform.position);
            circles.Add(circle);
        }
        return false;
    }

    CircleHolder Circle(CircleHolder c)
    {
        if (c.circleStage > 3) { Debug.Log("Invalid"); }
        else if (c.circleStage > 2)
        {
            c.entity.LookAt(c.origPos);
            c.entity.position += c.entity.forward * circleMoveSpeed * Time.deltaTime;
            if (Vector3.Distance(c.entity.position, c.origPos) <= circleEpsilon)
            {
                c.circleStage = 4;
                c.entity.rotation = Quaternion.identity;
            }
        }
        else if (c.circleStage > 1)
        {
            c.entity.RotateAround(c.center, c.entity.up, circleRotateSpeed * Time.deltaTime);
            if (Vector3.Distance(c.entity.position, c.origPos) > c.curDist + circleEpsilon) { c.passed = true; }
            if (c.passed && Vector3.Distance(c.entity.position, c.origPos) <= c.curDist + circleEpsilon) { c.circleStage = 3; }
        }
        else
        {
            c.entity.LookAt(c.center);
            c.entity.position += c.entity.forward * circleMoveSpeed * Time.deltaTime;
            if (Vector3.Distance(c.entity.position, c.center) <= c.radius + circleEpsilon)
            {
                c.entity.rotation = Quaternion.identity;
                c.curDist = Vector3.Distance(c.entity.position, c.origPos);
                c.circleStage = 2;
            }
        }

        return c;
    }

    #endregion

    #region Zoom

    void ZoomInit()
    {
        zoomTimeElapsed = zoomIntervalElapsed = 0f;
        zooms.Clear();
    }

    bool ZoomUpdate()
    {
        zoomTimeElapsed += Time.deltaTime;
        zoomIntervalElapsed += Time.deltaTime;
        if (shotElapsed >= shotTime)
        {
            shotElapsed = 0f;
            zooms[Random.Range(0, zooms.Count - 1)].entity.GetComponent<Enemy>().Shoot();
        }

        for (int i = 0; i < zooms.Count; i++) { zooms[i] = Zoom(zooms[i]); }
        zooms.RemoveAll(c => c.zoomStage > 3);
        if (zoomTimeElapsed >= zoomTime) { return zooms.Count == 0; }

        if (zoomIntervalElapsed >= zoomInterval)
        {
            zoomIntervalElapsed = 0f;

            Transform t;
            bool contains;
            int itrs = 0;
            do
            {
                t = Enemy.enemies[Random.Range(0, Enemy.enemies.Count - 1)].transform;
                contains = false;
                foreach (ZoomHolder z in zooms)
                {
                    if(++itrs > Enemy.enemies.Count) { return false; }
                    if (t == z.entity)
                    {
                        contains = true;
                        break;
                    }
                }
            } while (contains);

            ZoomHolder zoom = new ZoomHolder();
            zoom.Initialize(t, FindObjectOfType<Player>().transform.position);
            zooms.Add(zoom);
        }
        return false;
    }

    ZoomHolder Zoom(ZoomHolder z)
    {
        if (z.zoomStage > 3) { Debug.Log("Invalid"); }
        else if (z.zoomStage > 2)
        {
            z.entity.LookAt(z.origPos);
            z.entity.position += z.entity.forward * zoomSpeed * Time.deltaTime;
            if (Vector3.Distance(z.entity.position, z.origPos) <= zoomEpsilon)
            {
                z.entity.rotation = Quaternion.identity;
                z.zoomStage = 4;
            }
        }
        else if (z.zoomStage > 1)
        {
            z.waitElapsed += Time.deltaTime;
            if(z.waitElapsed >= zoomWait) { z.zoomStage = 3; }
        }
        else
        {
            z.entity.LookAt(z.center);
            z.entity.position += z.entity.forward * zoomSpeed * Time.deltaTime;
            if(Vector3.Distance(z.entity.position, z.center) <= zoomRadius + zoomEpsilon) { z.zoomStage = 2; }
        }

        return z;
    }

    #endregion

    void RemoveAll(Transform t)
    {
        zooms.RemoveAll(z => z.entity == t);
        circles.RemoveAll(c => c.entity == t);
        swaps.RemoveAll(s => s.ent1 == s.ent2 == t);
    }

}




