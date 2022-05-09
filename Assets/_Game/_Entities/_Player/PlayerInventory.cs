using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField, Tooltip("Nombre d'orbes dans l'inventaires")]
    private int _orb = 0; public int Orb => _orb;


    // Event
    public delegate void InventoryChangeDelegate();
    public event InventoryChangeDelegate eventInventoryChange;

    public void AddOrb(int value)
    {
        _orb += value;
        if (_orb < 0)
            _orb = 0;

        eventInventoryChange?.Invoke();
    }
}