using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractible : MonoBehaviour
{
    // Player interragissant avec l'object
    protected GameObject _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerController>().eventInterract += Interract;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = null;
            collision.gameObject.GetComponent<PlayerController>().eventInterract -= Interract;
        }
    }

    protected virtual void Interract() { }
}
