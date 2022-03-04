using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoneSkin", menuName = "Magic/Spell/StoneSkin")]
public class StoneSkin : SelfSpell
{
    [SerializeField]
    private List<EffectMother> effects;

    public override void Cast(GameObject caster)
    {
        // On récupère et modifie le sprite du caster
        //caster.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);

        Debug.Log("Peau de pierre !");

        StatsManager casterStats = caster.GetComponent<StatsManager>();
        for (int i = 0; i < effects.Count; i++)
        {
            casterStats.AddEffect(effects[i]);
        }
    }
}