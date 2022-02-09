using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPeauDePierre", menuName = "Magic/SpecialSpell/PeauDePierre")]
public class StoneSkin : SelfSpell
{
    [SerializeField]
    private EffectMother effect;

    public override void Cast(MagicSpell spell, GameObject caster)
    {
        // On r�cup�re et modifie le sprite du caster
        //caster.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);

        Debug.Log("Peau de pierre !");

        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.AddEffect(effect);
    }
}