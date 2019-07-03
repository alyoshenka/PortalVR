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
        int currentScore = scores.Length > 0 ? scores[scores.Length - 1] : -1; // 0
        Array.Sort(scores);
        Array.Reverse(scores);
        GameObject.Find("ScoreNumber").GetComponent<Text>().text = currentScore +  "";
        GameObject scoreDisp = GameObject.Find("Score");
        if (scores.Length > 0) { scoreDisp.GetComponent<Text>().text = scores[0] + ""; }

        for(int i = 1; i < numTopScores && i < scores.Length; i++)
        {
            Debug.Log(i);
            GameObject newScore = Instantiate(scoreDisp, scoreDisp.GetComponent<RectTransform>().position, scoreDisp.transform.rotation, scoreDisp.transform.parent);
            // newScore.GetComponent<RectTransform>().position += Vector3.down * spacing;
            Text t = newScore.GetComponent<Text>();
            t.text = scores[i] + "";
            if(scores[i] == currentScore && !myScoreDisplayed)
            {
                myScoreDisplayed = true;
                t.color = myScoreColor;
                t.fontStyle = FontStyle.Bold;
            }
        }
    }
}
