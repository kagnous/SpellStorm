using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSelfSpell", menuName = "Magic/SpecialSpell/DefaultSelfSpell")]
public class SelfSpell : MagicSpell
{
    [SerializeField, Tooltip("Dur�e de l'effet")]
    protected float _duration; public float Duration => _duration;

    protected GameObject player;

    public void Awake()
    {
        player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    public override void Cast()
    {
        if(player == null) player = FindObjectOfType<CharacterMovement>().gameObject;
    }
}