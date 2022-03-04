using UnityEngine;

[CreateAssetMenu(fileName = "NewImmolation", menuName = "Magic/Spell/Immolation")]
public class Immolation : SelfSpell
{
    [SerializeField]
    private EffectMother effect;

    public override void Cast(GameObject caster)
    {
        // On récupère et modifie le sprite du caster
        //caster.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

        Debug.Log("Immolation !");

        StatsManager casterStats = caster.GetComponent<StatsManager>();
        casterStats.AddEffect(effect);
    }
}