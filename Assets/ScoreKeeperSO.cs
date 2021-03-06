﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScoreKeeperSO", order = 1)]
public class ScoreKeeperSO : ScriptableObject
{
    // test push

    [HideInInspector]
    public List<IScoreListener> scoreListeners;
    public List<int> Scores { get; private set; }
    public bool hasRecordedScore;
    int currentScore;

    public void Init()
    {
        if (null == scoreListeners)
        {
            scoreListeners = new List<IScoreListener>();
            Scores = new List<int>();
            currentScore = 0;
        }
    }

    public void AddPoints(int points)
    {
        hasRecordedScore = true;
        currentScore += points;
        Debug.Log("current score: " + currentScore);
        foreach (IScoreListener i in scoreListeners) { i.UpdateScore(currentScore); }
    }

    public void AddScore()
    {
        Scores.Add(currentScore);
        currentScore = 0;
    }
}
