using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PickupObjectUI : MonoBehaviour
{
    public PowerupObject obj;
    [Tooltip("Can be null if Button is childed")]
    public Button button;

    InventoryManager im;

    void Start()
    {
        if(null == button) { button = GetComponentInChildren<Button>(); }
        button.onClick.AddListener(delegate { AddToInventory(); }); // anonymous function?

        transform.Find("Name").GetComponent<Text>().text = obj.title;
        transform.Find("Cost").GetComponent<Text>().text = "$ " + obj.cost;
        transform.Find("Description").GetComponent<Text>().text = obj.description;

        im = FindObjectOfType<InventoryManager>();
    }

    public void AddToInventory()
    {
        im.AddToInventory(this);
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
