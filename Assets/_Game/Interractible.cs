using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().eventInterract += Interract;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().eventInterract -= Interract;
        }
    }

    protected virtual void Interract() { }
}
