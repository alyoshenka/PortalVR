using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IScoreListener
{    
    public Text scoreText;
    public ScoreKeeperSO sk;

    public static int Balance { get; private set; }

    static List<PickupObjectUI> possiblePickups;
    static PickupGenerator pg;

    // Start is called before the first frame update
    void Start()
    {
        Balance = sk.Scores.Count > 0 ? sk.Scores[sk.Scores.Count - 1] : 0; // should always be 0
        sk.scoreListeners.Add(this);
        sk.hasRecordedScore = false;
       
        pg = FindObjectOfType<PickupGenerator>();
        possiblePickups = new List<PickupObjectUI>(GetComponentsInChildren<PickupObjectUI>());
        // foreach(PickupObjectUI pui in possiblePickups) { pui.obj.IM = this; }
        possiblePickups.Sort();
        OrganizePickups();
        Debug.Log("start");
    }

    void Update()
    {
        scoreText.text = "$ " + Balance;
        // foreach(PickupObjectUI pui in possiblePickups) { pui.obj.IM = this; }
        possiblePickups.Sort();
        OrganizePickups();
        Debug.Log("start");
    }

    // processing stage, update ui
    void OrganizePickups()
    {
        Debug.Log("Balance should be: " + Balance);
        scoreText.text = "$ " + Balance;
        Debug.Log("Text is: " + scoreText.text);
        for (int i = 0; i < possiblePickups.Count; i++)
        {
            if(possiblePickups[i].obj.cost <= Balance) { possiblePickups[i].Enable(); }
            else { possiblePickups[i].Disable(); }
        }
    }

    public void AddToInventory(PickupObjectUI po)
    {
        if(Balance < po.obj.cost) { return; }

        pg.GeneratePickup(po);
        sk.AddPoints(-po.obj.cost); // this will update score
    }

    public void UpdateScore(int score)
    {
        Debug.Log("updating im score");
        Balance = score;
        OrganizePickups();
    }
}
