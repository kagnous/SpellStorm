using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    [SerializeField, Tooltip("Valeur de l'orbe")]
    private int _orbValue = 1;

    [SerializeField, Tooltip("Mana restauré")]
    private int _mana;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInventory>().AddOrb(_orbValue);
            collision.gameObject.GetComponent<StatsPlayerManager>().SetMana(_mana);
            Destroy(gameObject);
        }
    }
}