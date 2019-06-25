using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IScoreListener
{
    public int startingBalance;    

    [Header("UI Elements")]
    public Text scoreText;

    public static int Balance { get; private set; }
    public static Text ScoreText;

    static List<PickupObjectUI> inventory;
    static List<PickupObjectUI> possiblePickups;
    static PickupGenerator pg;

    // Start is called before the first frame update
    void Start()
    {
        Balance = startingBalance;
        FindObjectOfType<ScoreKeeper>().score = Balance;
        FindObjectOfType<ScoreKeeper>().scoreListeners.Add(this);
        ScoreText = scoreText;
        ScoreText.text = "$ " + Balance;
        inventory = new List<PickupObjectUI>();
       
        pg = FindObjectOfType<PickupGenerator>();
        possiblePickups = new List<PickupObjectUI>(GetComponentsInChildren<PickupObjectUI>());
        possiblePickups.Sort();
        OrganizePickups();
    }

    // processing stage, update ui
    static void OrganizePickups()
    {
        ScoreText.text = "$ " + Balance;
        for (int i = 0; i < possiblePickups.Count; i++)
        {
            if(possiblePickups[i].obj.cost <= Balance) { possiblePickups[i].Enable(); }
            else { possiblePickups[i].Disable(); }
        }
    }

    public static void AddToInventory(PickupObjectUI po)
    {
        inventory.Add(po);
        Balance -= po.obj.cost; // update ui, etc from there
        OrganizePickups();
        pg.GeneratePickup(po);
    }

    public void UpdateScore(int score)
    {
        Balance = score;
        OrganizePickups();
    }
}
