using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using VRTK.Examples;

public class Button : ControllableReactor
{
    /*
    public delegate void PressEvent(object sender, ControllableEventArgs e);
    public event ControllableEventHandler ButtonPress;

    public void Initialize()
    {
        controllable.MaxLimitReached += ButtonPress;
    }

    /*
    protected override void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        if (null == ButtonPress) { Debug.Log("null"); }
        else { ButtonPress(sender, e); }
    }
       
    
    protected override void MinLimitReached(object sender, ControllableEventArgs e)
    {
        Debug.Log("button min");
    }
    

    void StartGame()
    {
        LevelManager.TryNextLevel();
    }

    void Arrange()
    {
        Enemy.stage = Enemy.Stage.arranging;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }


    protected override void ValueChanged(object sender, ControllableEventArgs e)
    {
        // do nothing
    }
    */
}
