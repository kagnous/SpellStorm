using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField, Tooltip("Valeur de l'orbe")]
    private int _orbValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().AddOrb(_orbValue);
            Destroy(gameObject);
        }
    }
}