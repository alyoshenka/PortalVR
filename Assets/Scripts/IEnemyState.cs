using UnityEngine;
using System;

public interface IEnemyState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
    bool ReadyForNext();
}

public abstract class EnemyState : IEnemyState
{
    public IEnemyState nextState { get; set; }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract bool ReadyForNext();
}

#region States

public class ProcessState : EnemyState
{
    public override void OnEnter()
    {
        Debug.Log("enter process");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("exit process");
    }

    public override bool ReadyForNext()
    {
        throw new System.NotImplementedException();
    }
}

public class SpawnState : EnemyState
{
    public override void OnEnter()
    {
        Debug.Log("enter spawn");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("exit spawn");
    }

    public override bool ReadyForNext()
    {
        throw new System.NotImplementedException();
    }
}


public class ArrangeState : EnemyState
{
    public override void OnEnter()
    {
        Debug.Log("enter arrange");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("exit arrange");
    }

    public override bool ReadyForNext()
    {
        throw new System.NotImplementedException();
    }
}


public class FireState : EnemyState
{
    public override void OnEnter()
    {
        Debug.Log("enter fire");
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("exit fire");
    }

    public override bool ReadyForNext()
    {
        throw new System.NotImplementedException();
    }
}

#endregion

public class EnemyStateMachine
{
    IEnemyState currentState;
    ProcessState process;
    SpawnState spawn;
    ArrangeState arrange;
    FireState fire;

    // List<Enemy> enemies;

    public EnemyStateMachine()
    {
        process = new ProcessState();
        spawn = new SpawnState();
        arrange = new ArrangeState();
        fire = new FireState();

        process.nextState = spawn;
        spawn.nextState = arrange;
        arrange.nextState = fire;
        fire.nextState = process;
        currentState = process;
    }

    public void RegisterEnemy(Enemy e)
    {

    }
}
