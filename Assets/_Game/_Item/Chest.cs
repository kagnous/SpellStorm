using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interractible
{
    [SerializeField, Tooltip("Nombre d'orbe dans le coffre")]
    private int _orbContains;

    protected override void Interract()
    {
        Debug.Log("Ouverture");
        _player.GetComponent<PlayerInventory>().AddOrb(_orbContains);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}