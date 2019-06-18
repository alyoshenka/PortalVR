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
    bool buttonPressed;

    public ProcessState()
    {
        buttonPressed = false;
    }

    public void AddReadyButton(ControllableReactor _readyButton)
    {
        readyButton = _readyButton;
        readyButton.controllable.MaxLimitReached += EndStateButtonPress;
    }

    public override void OnEnter()
    {
        Debug.Log("enter process");
    }

    public override void OnUpdate()
    {
        // nothing
    }

    public override void OnExit()
    {
        Debug.Log("exit process");
        buttonPressed = false;
    }

    public override bool ReadyForNextState()
    {
        return buttonPressed || Input.GetKeyDown(KeyCode.Alpha1);
    }

    public void EndStateButtonPress(object sender, ControllableEventArgs e)
    {
        buttonPressed = true;
    }
}

public class SpawnState : EnemyState
{
    List<Spawner> spawners;
    int waitingSpawnerCount;

    public SpawnState(List<Spawner> _spawners)
    {
        if(null == _spawners) { Debug.LogError("spawners null"); }

        spawners = _spawners;
        waitingSpawnerCount = spawners.Count;
        foreach(Spawner s in spawners) { s.spawnState = this; }
    }

    public override void OnEnter()
    {
        Debug.Log("enter spawn");

        Enemy.Initialize();
    }

    public override void OnUpdate()
    {
        float t = Time.deltaTime;
        foreach (Spawner s in spawners) { s.Spawn(t); }
        if (null != Enemy.enemies) { foreach (Enemy e in Enemy.enemies) { e.Move(t); } }
    }

    public override void OnExit()
    {
        Debug.Log("exit spawn");
        waitingSpawnerCount = spawners.Count;
        foreach(Spawner s in spawners) { s.Reset(); }
    }

    public override bool ReadyForNextState()
    {
        return waitingSpawnerCount <= 0;
    }

    public void RemoveSpawner()
    {
        waitingSpawnerCount--;
    }
}

public class ArrangeState : EnemyState
{
    public GridManager gm;

    public ArrangeState(GridManager _gm)
    {
        gm = _gm;
    }

    public override void OnEnter()
    {
        Debug.Log("enter arrange");

        gm.Initialize(Enemy.enemies);
    }

    public override void OnUpdate()
    {
        foreach (Enemy e in Enemy.enemies) { e.Arrange(); }
    }

    public override void OnExit()
    {
        Debug.Log("exit arrange");
    }

    public override bool ReadyForNextState()
    {
        return Enemy.circlingEnemies.Count == 0;
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
        float t = Time.deltaTime;
        foreach(Enemy e in Enemy.enemies) { e.Shoot(t); }
    }

    public override void OnExit()
    {
        Debug.Log("exit fire");

        Enemy.enemies.Clear();
    }

    public override bool ReadyForNextState()
    {
        return Enemy.enemies.Count == 0;
    }
}

#endregion



