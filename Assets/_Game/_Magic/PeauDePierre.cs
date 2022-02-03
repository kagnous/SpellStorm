using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPeauDePierre", menuName = "Magic/SpecialSpell/PeauDePierre")]
public class PeauDePierre : SelfSpell
{
    public override void Cast()
    {
        base.Cast();
        player.GetComponent<SpriteRenderer>().color = Color.black;
        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        characterMovement.Speed /= 2;
        characterMovement.JumpForce /= 2;
        Debug.Log("Peau de pierre !");
    }
}