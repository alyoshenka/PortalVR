using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using VRTK.Examples;

public class EnemyStateMachine : MonoBehaviour
{
    public ControllableReactor processButton;

    IEnemyState currentState;
    ProcessState process;
    SpawnState spawn;
    ArrangeState arrange;
    FireState fire;

    List<Enemy> enemies;
    List<Spawner> spawners;

    void Start()
    { 
        enemies = new List<Enemy>(); // not sure
        spawners = new List<Spawner>(FindObjectsOfType<Spawner>());

        process = new ProcessState();
        spawn = new SpawnState(spawners);
        arrange = new ArrangeState(FindObjectOfType<GridManager>());
        fire = new FireState(enemies);

        if (null == processButton) { Debug.Log("no process button"); }
        else { process.AddReadyButton(processButton); }

        process.nextState = spawn;
        spawn.nextState = arrange;
        arrange.nextState = fire;
        fire.nextState = process;

        currentState = process;
    }

    void Update()
    {
        currentState.OnUpdate();
        currentState = currentState.NextState();
    }

    public void RegisterEnemy(Enemy e)
    {
        enemies.Add(e);
    }
}

