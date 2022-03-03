using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interractible
{
    protected override void Interract()
    {
        Debug.Log("Ouverture");
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}