using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPeauDePierre", menuName = "Magic/SpecialSpell/PeauDePierre")]
public class StoneSkin : SelfSpell
{
    [SerializeField]
    private int _bonusArmor = 2;

    [SerializeField]
    private TokenEffect effect;

    /////////////////////////////// Trouver un moyen de savoir que la cible est déjà affectée par peau de pierre pour ne l'appliquer que si c'est pas déjà le cas ///////////

    public override void Cast(MagicSpell spell, GameObject caster)
    {
        /*
        // On récupère et modifie le sprite du caster
        caster.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);

        // On doule sa sensibilité à la gravité
        caster.GetComponent<Rigidbody2D>().gravityScale *= 2;                   // Mieux ? : caster.GetComponent<Rigidbody2D>().mass *= 2;

        // On modifie sa vitesse
        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.Speed /= 2;
                                                                                //casterStats.JumpForce /= 2;

        // On augmente son armure
        casterStats.Armor += _bonusArmor;
        Debug.Log("Peau de pierre !");
        */

        StatsManager casterStats = caster.GetComponent<StatsManager>();
        //casterStats.AddEffect(effect);
    }

    public override void EndSpell(MagicSpell spell, GameObject caster)
    {
        /*
        // On refait tout les effets de Cast dans l'autre sens
        caster.GetComponent<SpriteRenderer>().color = Color.white;
        caster.GetComponent<Rigidbody2D>().gravityScale /= 2;
        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.Speed *= 2;
                                                                                //casterStats.JumpForce *= 2;
        casterStats.Armor -= _bonusArmor;
        */
    }
}