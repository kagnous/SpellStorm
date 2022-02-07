using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPeauDePierre", menuName = "Magic/SpecialSpell/PeauDePierre")]
public class PeauDePierre : SelfSpell
{
    [SerializeField]
    private int _armorBonus = 2;

    /////////////////////////////// Trouver un moyen de savoir que la cible est déjà affectée par peau de pierre pour ne l'appliquer que si c'est pas déjà le cas ///////////

    public override void Cast(MagicSpell spell, GameObject caster)
    {
        caster.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        caster.GetComponent<Rigidbody2D>().gravityScale *= 2;
        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.Speed /= 2;
        casterStats.JumpForce /= 2;
        casterStats.Armor += _armorBonus;
        Debug.Log("Peau de pierre !");
    }

    public override void EndSpell(MagicSpell spell, GameObject caster)
    {
        base.EndSpell(spell, caster);
        caster.GetComponent<SpriteRenderer>().color = Color.white;
        caster.GetComponent<Rigidbody2D>().gravityScale /= 2;
        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.Speed *= 2;
        casterStats.JumpForce *= 2;
        casterStats.Armor -= _armorBonus;
    }
}