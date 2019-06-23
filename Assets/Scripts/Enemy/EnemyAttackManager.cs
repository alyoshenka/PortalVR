using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    enum Attack { swap, circle, zoom };

    #region Design Vars

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

    #endregion

    int attackCount;
    Attack currentAttack;

    // I'm sorry this script is so gross

    public void Initialize()
    {
        attackCount = 3;

        if(null != swaps) { swaps.Clear(); }
        else { swaps = new List<SwapHolder>(); }
        if (null != circles) { circles.Clear(); }
        else { circles = new List<CircleHolder>(); }
        if (null != zooms) { zooms.Clear(); }
        else { zooms = new List<ZoomHolder>(); }

        currentAttack = ChooseRandomAttack();

        swapTimeElapsed = swapIntervalElapsed = 0f;
        circleTimeElapsed = circleIntervalElapsed = 0f;
    }

    public void UpdateAttacks()
    {
        if (RunState()) { currentAttack = ChooseRandomAttack(); }
    }

    bool RunState()
    {
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
        return false;
    }

    Attack ChooseRandomAttack()
    {
        Attack newAttack = (Attack)Random.Range(0, attackCount);
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
        Debug.Log(newAttack);
        return newAttack;
    }

    #region Swap

    void SwapInit()
    {
        swapTimeElapsed = swapIntervalElapsed = 0f;
        swaps.Clear();
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

            bool contains = false;
            Transform ent1, ent2;
            do
            {
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
            
            do
            {
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
        if (s.swapStage > 5) { Debug.Log("Invalid"); }
        else if(s.swapStage > 4)
        {
            s.ent1.GetComponent<Renderer>().material.color = Color.red;
            s.ent2.GetComponent<Renderer>().material.color = Color.red;

            s.ent1.rotation = Quaternion.identity;
            s.ent2.rotation = Quaternion.identity;

            s.swapStage = 6;
        }
        else if(s.swapStage > 3)
        {
            s.ent1.LookAt(s.ent2pos);
            s.ent1.position += s.ent1.forward * swapSpeed * Time.deltaTime;
            s.ent2.LookAt(s.ent1pos);
            s.ent2.position += s.ent2.forward * swapSpeed * Time.deltaTime;

            if(Vector3.Distance(s.ent1.position, s.ent2pos) < swapEpsilon
                && Vector3.Distance(s.ent2.position, s.ent1pos) < swapEpsilon)
            { s.swapStage = 5; }
        }
        else if(s.swapStage > 2)
        {
            s.ent1.LookAt(s.ent2forward);
            s.ent2.LookAt(s.ent1forward);
            s.ent1.position += s.ent1.forward * swapSpeed * Time.deltaTime;
            s.ent2.position += s.ent2.forward * swapSpeed * Time.deltaTime;

            if(Vector3.Distance(s.ent2.position, s.ent1forward) < swapEpsilon 
                && Vector3.Distance(s.ent1.position, s.ent2forward) < swapEpsilon)
            { s.swapStage = 4; }
        }
        else if(s.swapStage > 1)
        {
            s.ent2.position += s.ent2.forward * Time.deltaTime * swapSpeed;
            s.ent1.position += s.ent1.forward * Time.deltaTime * swapSpeed;

            if(Vector3.Distance(s.ent1.position, s.ent1pos) > swapDepth 
                && Vector3.Distance(s.ent2.position, s.ent2pos) > swapDepth + swapDepthDifference)
            { s.swapStage = 3; }
        }
        else
        {
            s.ent1.GetComponent<Renderer>().material.color = Color.green;
            s.ent2.GetComponent<Renderer>().material.color = Color.blue;

            s.ent2.position += s.ent2.forward * Time.deltaTime * swapSpeed;

            if (Vector3.Distance(s.ent2.position, s.ent2pos) >= swapDepth) { s.swapStage = 2; }
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

        for (int i = 0; i < circles.Count; i++) { circles[i] = Circle(circles[i]); }
        circles.RemoveAll(z => z.circleStage > 3);
        if (circleTimeElapsed >= circleTime) { return circles.Count == 0; }

        if(circleIntervalElapsed >= circleInterval)
        {
            circleIntervalElapsed = 0f;

            Transform t;
            bool contains;
            do
            {
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
            c.cnt++;
            if (c.cnt > 30 && Vector3.Distance(c.entity.position, c.origPos) <= c.curDist + circleEpsilon) { c.circleStage = 3; }
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

        for (int i = 0; i < zooms.Count; i++) { zooms[i] = Zoom(zooms[i]); }
        zooms.RemoveAll(c => c.zoomStage > 3);
        if (zoomTimeElapsed >= zoomTime) { return zooms.Count == 0; }

        if (zoomIntervalElapsed >= zoomInterval)
        {
            zoomIntervalElapsed = 0f;

            Transform t;
            bool contains;
            do
            {
                t = Enemy.enemies[Random.Range(0, Enemy.enemies.Count - 1)].transform;
                contains = false;
                foreach (ZoomHolder z in zooms)
                {
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

}




