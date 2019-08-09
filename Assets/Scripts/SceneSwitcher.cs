using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK.Examples;
using VRTK.Controllables;

public class SceneSwitcher : MonoBehaviour
{
    public ControllableReactor switchButton;
    // public bool toMenu;
    public bool initialization;

    public string toScene;

    private void Start()
    {
        if (initialization)
        {
            SwitchScene();
            return;
        }
        switchButton.controllable.MaxLimitReached += SwitchSceneListener;
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(toScene);
        
    }

    void SwitchSceneListener(object sender, ControllableEventArgs e)
    {
        SwitchScene();
    }

    private void Update()
    {
        // reload
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
