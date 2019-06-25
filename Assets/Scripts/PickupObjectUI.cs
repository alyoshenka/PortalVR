using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PickupObjectUI : MonoBehaviour
{
    public PickupObject obj;
    [Tooltip("Can be null if Button is childed")]
    public Button button;

    void Start()
    {
        if(null == button) { button = GetComponentInChildren<Button>(); }
        button.onClick.AddListener(delegate { AddToInventory(); }); // anonymous function?

        transform.Find("Name").GetComponent<Text>().text = obj.name;
        transform.Find("Cost").GetComponent<Text>().text = "$ " + obj.cost;
        transform.Find("Description").GetComponent<Text>().text = obj.description;
    }

    public void AddToInventory()
    {
        InventoryManager.AddToInventory(this);
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void Enable()
    {
        button.interactable = true;
    }

}
