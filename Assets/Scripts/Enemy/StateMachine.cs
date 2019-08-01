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
    LevelManager lm;

    bool buttonPressed;

    public ProcessState()
    {
        buttonPressed = false;
        lm = GameObject.FindObjectOfType<LevelManager>();
    }

    public void AddReadyButton(ControllableReactor _readyButton)
    {
        readyButton = _readyButton;
        readyButton.controllable.MaxLimitReached += EndStateButtonPress;
        readyButton.displayText.text = "Start Game";
    }

    public override void OnEnter()
    {
        readyButton.displayText.text = "Next Level";
    }

    public override void OnUpdate()
    {
        // nothing
    }

    public override void OnExit()
    {
        buttonPressed = false;
        readyButton.displayText.text = "Kill All Enemies"; // maybe too much????
        lm.NextLevel();

        // close score and pickup ui
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
        gm.Initialize(Enemy.enemies);

        // this is how i was trying to fix early shooting problem
        // foreach(Gun g in GameObject.FindObjectsOfType<Gun>()) { g.bullet.GetComponent<Collider>().enabled = false; }
        // foreach (AutomaticGun g in GameObject.FindObjectsOfType<AutomaticGun>()) { g.bullet.GetComponent<Collider>().enabled = false; }
    }

    public override void OnUpdate()
    {
        foreach (Enemy e in Enemy.enemies) { e.Arrange(); }
    }

    public override void OnExit()
    {
        // this is how i was trying to fix early shooting problem
        // foreach (Gun g in GameObject.FindObjectsOfType<Gun>()) { g.bullet.GetComponent<Collider>().enabled = true; }
        // foreach (AutomaticGun g in GameObject.FindObjectsOfType<AutomaticGun>()) { g.bullet.GetComponent<Collider>().enabled = true; }
    }

    public override bool ReadyForNextState()
    {
        return Enemy.circlingEnemies.Count == 0;
    }
}

public class FireState : EnemyState
{
    EnemyAttackManager eam;

    public FireState()
    {
        eam = GameObject.FindObjectOfType<EnemyAttackManager>();
    }

    public override void OnEnter()
    {
        eam.Initialize();
        foreach(Enemy e in Enemy.enemies)
        {
            foreach(Collider c in e.GetComponentsInChildren<Collider>())
            {
                c.enabled = true;
            }
        }
    }

    public override void OnUpdate()
    {
        eam.UpdateAttacks();
    }

    public override void OnExit()
    {

        Enemy.enemies.Clear();
    }

    public override bool ReadyForNextState()
    {
        return Enemy.enemies.Count == 0;
    }
}

#endregion



