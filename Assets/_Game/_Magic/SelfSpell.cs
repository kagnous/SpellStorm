using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSelfSpell", menuName = "Magic/SpecialSpell/DefaultSelfSpell")]
public class SelfSpell : MagicSpell
{
    public override void Cast(MagicSpell spell, GameObject caster)
    {
        base.Cast(spell, caster);
    }
}