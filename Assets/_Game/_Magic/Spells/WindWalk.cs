using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWindWalk", menuName = "Magic/SpecialSpell/WindWalk")]
public class WindWalk : SelfSpell
{
    [SerializeField]
    private List<EffectMother> effects;

    public override void Cast(GameObject caster)
    {
        // On récupère et modifie le sprite du caster
        //caster.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);

        Debug.Log("Marche du vent !");

        StatsManager casterStats = caster.GetComponent<StatsManager>();
        for (int i = 0; i < effects.Count; i++)
        {
            casterStats.AddEffect(effects[i]);
        }
    }
}