using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IScoreListener
{    
    public Text scoreText;
    public ScoreKeeperSO sk;

    int balance;

   List<PickupObjectUI> possiblePickups;
   PickupGenerator pg;

    void Start()
    {
        balance = sk.Scores.Count > 0 ? sk.Scores[sk.Scores.Count - 1] : 0; // should always be 0
        sk.scoreListeners.Add(this);
        sk.hasRecordedScore = false;
       
        pg = FindObjectOfType<PickupGenerator>();
        possiblePickups = new List<PickupObjectUI>(GetComponentsInChildren<PickupObjectUI>());
        possiblePickups.Sort();
        OrganizePickups();
    }

    void Update()
    {
        scoreText.text = "$ " + balance;
        possiblePickups.Sort();
        OrganizePickups();
    }

    // processing stage, update ui
    void OrganizePickups()
    {
        scoreText.text = "$ " + balance;
        for (int i = 0; i < possiblePickups.Count; i++)
        {
            if(possiblePickups[i].obj.cost <= balance) { possiblePickups[i].Enable(); }
            else { possiblePickups[i].Disable(); }
        }
    }

    public void AddToInventory(PickupObjectUI po)
    {
        if(balance < po.obj.cost) { return; }

        pg.GeneratePickup(po);
        sk.AddPoints(-po.obj.cost); // this will update score
    }

    public void UpdateScore(int score)
    {
        balance = score;
        OrganizePickups();
    }
}
