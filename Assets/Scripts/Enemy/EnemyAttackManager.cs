using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    enum Attack { swap, circle, zoom };

    #region Design Vars

    [Header("Swap")]
    public float swapSpeed;
    public float swapDepth;
    public float swapDepthDifference;
    public float swapEpsilon;
    [Space(10)]
    public float swapTime;
    public float swapInterval;
    float swapTimeElapsed;
    float swapIntervalElapsed;

    [Header("Zoom")]

    [Header("Circle")]

    #endregion

    int attackCount;
    Attack currentAttack;

    // I'm sorry this script is so gross
    #region Holders 

    List<SwapHolder> swaps;

    #endregion

    public void Initialize()
    {
        attackCount = 3;
        if(null != swaps) { swaps.Clear(); }
        else { swaps = new List<SwapHolder>(); }
        currentAttack = ChooseRandomAttack();

        swapTimeElapsed = swapIntervalElapsed = 0f;
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
                break;
            case Attack.zoom:
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }
        return false;
    }

    Attack ChooseRandomAttack()
    {
        // Attack newAttack = (Attack)Random.Range(0, attackCount);
        Attack newAttack = Attack.swap;
        switch (newAttack)
        {
            case Attack.swap:
                SwapInit();
                break;
            case Attack.circle:
                break;
            case Attack.zoom:
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }
        return newAttack;
    }

    #region Attacks

    void SwapInit()
    {
        swaps.Clear();
    }

    // return whether state is over
    bool SwapUpdate()
    {
        swapIntervalElapsed += Time.deltaTime;
        swapTimeElapsed += Time.deltaTime;

        if(swapTimeElapsed >= swapTime)
        {
            return swaps.Count == 0;
        }

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

        
        for(int i = 0; i < swaps.Count; i++) // whyyyyyyy
        {
            //SwapHolder s = Swap(swaps[i]);
            //if(s.swapStage > 5) { swaps.Remove(s); }
            //else { swaps[i] = s; }
            swaps[i] = Swap(swaps[i]);
        }

        swaps.RemoveAll(s => s.swapStage > 5);

        return false;
    }

    SwapHolder Swap(SwapHolder s)
    {
        if (s.swapStage > 5)
        {
            Debug.Log(6);
        }
        else if(s.swapStage > 4)
        {
            s.ent1.GetComponent<Renderer>().material.color = Color.red;
            s.ent2.GetComponent<Renderer>().material.color = Color.red;

            s.ent1.Rotate(new Vector3(0, 180, 0));
            s.ent2.Rotate(new Vector3(0, 180, 0));

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

    bool Zoom()
    {
        return true;
    }

    bool Circle()
    {
        return true;
    }

    #endregion
}




