using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScoreKeeperSO", order = 1)]
public class ScoreKeeperSO : ScriptableObject
{
    [HideInInspector]
    public List<IScoreListener> scoreListeners;
    public List<int> Scores { get; private set; } 
    int currentScore;

    public void Init()
    {
        if (null == scoreListeners) { scoreListeners = new List<IScoreListener>(); }
        if (null == Scores) { Scores = new List<int>(); }
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        foreach (IScoreListener i in scoreListeners) { i.UpdateScore(currentScore); }
    }

    public void AddScore()
    {
        Scores.Add(currentScore);
        currentScore = 0;
    }
}
