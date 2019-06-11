using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine_bad : MonoBehaviour
{
    // enemy states
    public enum State { process, circle, arrange, fire }

    public State stage { get; private set; }

    GridManager gridManager;
    List<Spawner> allSpawners, readySpawners, unreadySpawners;
    bool readyForNextStage;
    List<Test> allEnemies, readyEnemies, waitingEnemies;

    // Start is called before the first frame update
    void Start()
    {
        readyForNextStage = false;
        allEnemies = readyEnemies = waitingEnemies = new List<Test>();
        readySpawners = allSpawners = new List<Spawner>(FindObjectsOfType<Spawner>());
        unreadySpawners = new List<Spawner>();
        gridManager = FindObjectOfType<GridManager>();
        stage = State.process;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) { ToNextState(); }

        switch (stage)
        {
            case State.process:
                break;
            case State.circle:
                break;
            case State.arrange:
                break;
            case State.fire:
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }
    }

    #region State Changes

    public void ToNextState()
    {
        switch (stage)
        {
            case State.process:
                ToCircle();
                break;
            case State.circle:
                ToArrange();
                break;
            case State.arrange:
                ToFire();
                break;
            case State.fire:
                ToProcess();
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }

    }

    void ToProcess()
    {
        stage = State.process;
        // clear all old data   
        readySpawners.Clear();
        unreadySpawners.Clear();

        Debug.Log("to process");
    }

    void ToCircle()
    {
        stage = State.circle;
        if(readySpawners.Count > 0) { Debug.LogWarning("Spawner list not empty"); }
        readySpawners = allSpawners;
        for(int i = 0; i < readySpawners.Count; i++)
        {
            readySpawners[i].StartSpawning(this);
        }
        unreadySpawners = readySpawners;
        readySpawners.Clear();

        Debug.Log("to circle");
    }

    void ToArrange()
    {      
        if(unreadySpawners.Count > 0)
        {
            Debug.Log("not ready to arrange");
            return;
        }

        stage = State.arrange;

        int enemyCount = allEnemies.Count;
        readyEnemies = allEnemies;
        Vector3[] positions = gridManager.Initialize(enemyCount);
        for(int i = 0; i < enemyCount; i++)
        {
            readyEnemies[i].goalPos = positions[i];
            readyEnemies[i].transform.LookAt(positions[i]);
        }

        Debug.Log("to arrange");
    }

    void ToFire()
    {
        stage = State.fire;
        // set up to fire
        // cue firing trigger
        // initalize grid/firing manager

        Debug.Log("to fire");
    }

    #endregion

    #region List Management

    public void AddToReady()
    {

    }

    public void AddToWaiting(Test enemyToAdd)
    {
        if (null != enemyToAdd) { waitingEnemies.Add(enemyToAdd); }
    }

    public void MoveToWaiting(Spawner spawner)
    {
        unreadySpawners.Remove(spawner);
        readySpawners.Add(spawner);
        if(unreadySpawners.Count <= 0)
        {
            Debug.Log("done spawning");
            // ToNextState();

        }
    }

    void MoveToUnready()
    {
        unreadySpawners = readySpawners;
        readySpawners.Clear();
    }

    public void AddEnemy(Test enemy)
    {
        if(null != enemy) { allEnemies.Add(enemy); }
    }

    #endregion

}
