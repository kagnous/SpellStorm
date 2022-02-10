using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectMother", menuName = "Game/EffectMother")]
public class EffectMother : ScriptableObject
{
    public enum AffectedValue
    {
        HP,
        Mana,
        Armor,
        Speed,
        GravityScale,
        Paralyse,
    }

    public enum TypeEffect
    {
        None,
        Fire,
        Cold
    }

    [SerializeField]
    protected TypeEffect typeEffect; public TypeEffect TypeOfTheEffect => typeEffect;

    [SerializeField]
    protected GameObject effectPrefab;

    [SerializeField]
    protected AffectedValue affectedValue;

    public virtual void Apply(StatsManager target)
    {
        GameObject effect = Instantiate(effectPrefab,target.transform);
        EffectController effectController =  effect.GetComponent<EffectController>();
        effectController.Target = target;
        effectController.Effet = this;
        AddApply(effect);
    }

    public virtual void AddApply(GameObject effect) { }

    public virtual void Effect(StatsManager target) { }

    public virtual void EndEffect(StatsManager taget) { }
}

[System.Serializable]
public struct TemporaryModifyStatEffect
{
    EffectMother.AffectedValue typeValue;
    public float value;
}