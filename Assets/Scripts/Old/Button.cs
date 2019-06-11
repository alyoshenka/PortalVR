using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using VRTK.Examples;

public class Button : ControllableReactor
{
    public enum ButtonAction { spawn, arrange };

    public ButtonAction action;

    protected override void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        switch (action)
        {
            case ButtonAction.spawn:
                StartGame();
                break;
            case ButtonAction.arrange:
                Arrange();
                break;
            default:
                Debug.LogError("invalid state");
                break;
        }
    }
       
    /*
    protected override void MinLimitReached(object sender, ControllableEventArgs e)
    {
        Debug.Log("button min");
    }
    */

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
}
