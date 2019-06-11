using UnityEngine;
using System.Collections.Generic;

using VRTK.Controllables;
using VRTK.Examples;


public interface IEnemyState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
    bool ReadyForNextState();
    IEnemyState NextState();
}

public abstract class EnemyState : IEnemyState
{
    public IEnemyState nextState { get; set; }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract bool ReadyForNextState();
    public IEnemyState NextState()
    {
        if (ReadyForNextState())
        {
            OnExit();
            nextState.OnEnter();
            return nextState;
        }
        else { return this; }
       
    }
}

#region States

public class ProcessState : EnemyState
{
    ControllableReactor readyButton;

    public void AddReadyButton(ControllableReactor _readyButton)
    {
        readyButton = _readyButton;
        readyButton.controllable.MaxLimitReached += Test;
    }

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

    public override bool ReadyForNextState()
    {
        return Input.GetMouseButtonDown(1);
    }

    public void Test(object sender, ControllableEventArgs e)
    {
        Debug.Log("worked");
    }
}

public class SpawnState : EnemyState
{
    List<Spawner> spawners;

    public SpawnState(List<Spawner> _spawners)
    {

    }

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

    public override bool ReadyForNextState()
    {
        return Input.GetMouseButtonDown(1);
    }
}


public class ArrangeState : EnemyState
{
    public ArrangeState()
    {

    }

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

    public override bool ReadyForNextState()
    {
        return Input.GetMouseButtonDown(1);
    }
}


public class FireState : EnemyState
{
    public FireState(List<Enemy> es) { }

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

    public override bool ReadyForNextState()
    {
        return Input.GetMouseButtonDown(1);
    }
}

#endregion

