using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IScoreListener
{
    void UpdateScore(int newScore);
}

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreboard;

    public int score;

    public List<IScoreListener> scoreListeners;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        scoreListeners = new List<IScoreListener>();
    }

    public void AddPoints(int points)
    {
        score += points;
        foreach (IScoreListener i in scoreListeners) { i.UpdateScore(score); }
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreboard.text = "Score: " + score + "\nLevel: " + "Level?";
    }
}
