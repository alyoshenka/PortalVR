using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreboard;

    int score;
    // list of listeners?

    // Start is called before the first frame update
    void Start()
    {
        score = 0; // add functionality to bind listeners
        UpdateText();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateText();
    }
        

    public void UpdateText()
    {
        scoreboard.text = "Score: " + score;
    }
}
