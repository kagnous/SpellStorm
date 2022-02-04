using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPeauDePierre", menuName = "Magic/SpecialSpell/PeauDePierre")]
public class PeauDePierre : SelfSpell
{
    [SerializeField]
    private int _armorBonus = 2;

    public override void Cast(MagicSpell ds)
    {
        base.Cast(ds);
        player.GetComponent<SpriteRenderer>().color = Color.black;
        //CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        StatsManager playerStats = player.GetComponent<StatsManager>();
        playerStats.Speed /= 2;
        playerStats.JumpForce /= 2;
        playerStats.Armor += _armorBonus;
        Debug.Log("Peau de pierre !");
    }
}