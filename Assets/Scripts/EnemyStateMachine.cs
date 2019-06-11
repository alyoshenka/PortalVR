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

    public EnemyStateMachine()
    {
        enemies = new List<Enemy>();
        spawners = new List<Spawner>();

        process = new ProcessState();
        spawn = new SpawnState(spawners);
        arrange = new ArrangeState();
        fire = new FireState(enemies);

        if(null == processButton) { Debug.Log("fbhasdipgh"); }
        process.AddReadyButton(processButton);

        process.nextState = spawn;
        spawn.nextState = arrange;
        arrange.nextState = fire;
        fire.nextState = process;
        currentState = process;

        if(null == currentState) { Debug.LogError("no"); }
    }

    void Update()
    {
        /*
        if (null == currentState) { Debug.LogWarning("ummmm"); }
        else { currentState.OnUpdate();
            currentState = currentState.NextState();
        }
        */

    }

    public void RegisterEnemy(Enemy e)
    {
        enemies.Add(e);
    }
}

