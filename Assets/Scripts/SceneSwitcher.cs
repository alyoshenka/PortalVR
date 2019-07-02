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

    private void Start()
    {
        if (toMenu) { switchButton.controllable.MaxLimitReached += SwitchToMenu; }
        else { switchButton.controllable.MaxLimitReached += SwitchToGame; }
    }

    void SwitchToMenu(object sender, ControllableEventArgs e)
    {
        // score
        SceneManager.LoadScene("Menu");
    }

    void SwitchToGame(object sender, ControllableEventArgs e)
    {
        SceneManager.LoadScene("Game");
    }
}
