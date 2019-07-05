using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IScoreListener
{    
    public Text scoreText;
    public ScoreKeeperSO sk;

    public static int Balance { get; private set; }
    public static Text ScoreText;

    static List<PickupObjectUI> inventory;
    static List<PickupObjectUI> possiblePickups;
    static PickupGenerator pg;

    // Start is called before the first frame update
    void Start()
    {
        Balance = sk.Scores.Count > 0 ? sk.Scores[sk.Scores.Count - 1] : 0;
        sk.scoreListeners.Add(this);
        sk.hasRecordedScore = false;
        ScoreText = scoreText;
        ScoreText.text = "$ " + Balance;
        inventory = new List<PickupObjectUI>();
       
        pg = FindObjectOfType<PickupGenerator>();
        possiblePickups = new List<PickupObjectUI>(GetComponentsInChildren<PickupObjectUI>());
        // foreach(PickupObjectUI pui in possiblePickups) { pui.obj.IM = this; }
        possiblePickups.Sort();
        OrganizePickups();
        Debug.Log("start");
    }

    void Update()
    {
        Balance = sk.Scores.Count > 0 ? sk.Scores[sk.Scores.Count - 1] : 0;
        sk.scoreListeners.Add(this);
        sk.hasRecordedScore = false;
        ScoreText = scoreText;
        ScoreText.text = "$ " + Balance;
        inventory = new List<PickupObjectUI>();

        pg = FindObjectOfType<PickupGenerator>();
        possiblePickups = new List<PickupObjectUI>(GetComponentsInChildren<PickupObjectUI>());
        // foreach(PickupObjectUI pui in possiblePickups) { pui.obj.IM = this; }
        possiblePickups.Sort();
        OrganizePickups();
        Debug.Log("start");
    }

    // processing stage, update ui
    void OrganizePickups()
    {
        ScoreText.text = "$ " + Balance;
        for (int i = 0; i < possiblePickups.Count; i++)
        {
            if(possiblePickups[i].obj.cost <= Balance) { possiblePickups[i].Enable(); }
            else { possiblePickups[i].Disable(); }
        }
    }

    public void AddToInventory(PickupObjectUI po)
    {
        inventory.Add(po);
        Balance -= po.obj.cost; // update ui, etc from there
        sk.AddPoints(-po.obj.cost); // this will update score
        pg.GeneratePickup(po);
    }

    public void UpdateScore(int score)
    {
        Balance = score;
        OrganizePickups();
    }
}
