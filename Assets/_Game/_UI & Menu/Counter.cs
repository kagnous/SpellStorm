using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private Text text;
    private PlayerInventory inventory;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        inventory = FindObjectOfType<PlayerInventory>();
    }

    private void OnEnable()
    {
        inventory.eventInventoryChange += SetValue;
    }
    private void OnDisable()
    {
        inventory.eventInventoryChange -= SetValue;
    }

    private void SetValue()
    {
        text.text = " " + inventory.Orb.ToString();
    }
}