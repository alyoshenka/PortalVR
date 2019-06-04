using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;
using VRTK.Examples;

public class Button : ControllableReactor
{
    public enum ButtonAction { start };

    public ButtonAction action;

    protected override void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        switch (action)
        {
            case ButtonAction.start:
                StartGame();
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
