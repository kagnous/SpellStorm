using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSelfSpell", menuName = "Magic/SpecialSpell/DefaultSelfSpell")]
public class SelfSpell : MagicSpell
{
    [SerializeField, Tooltip("Durée de l'effet")]
    protected float _duration; public float Duration => _duration;

    protected GameObject player;

    public override void Cast(MagicSpell selfSpell)
    {
        base.Cast(selfSpell);
        if(player == null) player = FindObjectOfType<CharacterMovement>().gameObject;
    }
}