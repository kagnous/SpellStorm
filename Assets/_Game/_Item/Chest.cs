using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interractible
{
    [SerializeField, Tooltip("Nombre d'orbe dans le coffre")]
    private int _orbContains;

    [SerializeField, Tooltip("Quantitée de mana restauré")]
    private int _mana;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Interract()
    {
        _player.GetComponent<PlayerInventory>().AddOrb(_orbContains);
        _player.GetComponent<StatsPlayerManager>().SetMana(_mana);

        gameObject.GetComponent<Collider2D>().enabled = false;

        _animator.Play("Chest");
    }
}