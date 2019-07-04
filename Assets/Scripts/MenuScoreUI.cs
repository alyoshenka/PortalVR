using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuScoreUI : MonoBehaviour
{
    public int numTopScores;
    public float spacing;
    public Color myScoreColor;
    public ScoreKeeperSO sk;

    bool myScoreDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        myScoreDisplayed = false;
        sk.Init();
        int[] scores = sk.Scores.ToArray();
        int currentScore = -1;
        if (!sk.hasRecordedScore) { currentScore = 0; }
        else if (scores.Length > 0) { currentScore = scores[scores.Length - 1]; }
        Array.Sort(scores);
        Array.Reverse(scores);
        GameObject.Find("ScoreNumber").GetComponent<Text>().text = currentScore +  "";
        GameObject scoreDisp = GameObject.Find("Score");
        if (scores.Length > 0) { scoreDisp.GetComponent<Text>().text = scores[0] + ""; }

        for(int i = 0; i < numTopScores && i < scores.Length; i++)
        {
            GameObject newScore = Instantiate(scoreDisp, scoreDisp.GetComponent<RectTransform>().position, scoreDisp.transform.rotation, scoreDisp.transform.parent);
            newScore.GetComponent<RectTransform>().localPosition += Vector3.down * spacing * i;
            Text t = newScore.GetComponent<Text>();
            t.text = scores[i] + "";
            if (scores[i] == currentScore && !myScoreDisplayed)
            {
                myScoreDisplayed = true;
                t.color = myScoreColor;
                t.fontStyle = FontStyle.Bold;
            }
        }
    }

    // test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sk.Scores.Add(sk.Scores.Count);
            Start();
        }
    }
}
