using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Player interragissant avec l'object
    protected GameObject _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerController>().IsClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player = null;
            collision.gameObject.GetComponent<PlayerController>().IsClimb = false;
        }
    }
}