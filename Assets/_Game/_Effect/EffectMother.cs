using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectMother", menuName = "Game/EffectMother")]
public class EffectMother : ScriptableObject
{
    [SerializeField]
    protected GameObject EffectPrefab;

    public virtual void Apply(StatsManager target)
    {
        GameObject effect = Instantiate(EffectPrefab,target.transform);
        EffectController effectController =  effect.GetComponent<EffectController>();
        effectController.Target = target;
        effectController.Effet = this;
        AddApply(effect);
    }

    public virtual void AddApply(GameObject effect) { }

    public virtual void Effect(StatsManager target) { }

    public virtual void EndEffect(StatsManager taget) { }
}