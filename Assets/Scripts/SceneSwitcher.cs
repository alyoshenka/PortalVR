using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK.Examples;
using VRTK.Controllables;

public class SceneSwitcher : MonoBehaviour
{
    public ControllableReactor switchButton;
    public bool toMenu;
    public bool initialization;

    private void Start()
    {
        if (initialization)
        {
            SwitchToMenu();
            return;
        }
        if (toMenu) { switchButton.controllable.MaxLimitReached += SwitchToMenuListener; }
        else { switchButton.controllable.MaxLimitReached += SwitchToGameListener; }
    }

    public void SwitchToMenu()
    {
        // score
        SceneManager.LoadScene("Menu");
    }
    void SwitchToGame()
    {
        SceneManager.LoadScene("Game");
    }

    void SwitchToMenuListener(object sender, ControllableEventArgs e)
    {
        SwitchToMenu();
    }
    void SwitchToGameListener(object sender, ControllableEventArgs e)
    {
        SwitchToGame();
    }
}
