using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectConstant", menuName = "Game/EffectConstant")]
public class EffectConstant : EffectMother
{
    [SerializeField]
    private float _duration;

    public override void AddApply(GameObject effect)
    {
        EffectConstantController effectController = effect.GetComponent<EffectConstantController>();
        effectController.Duaration = _duration;
    }

    public override void Effect(StatsManager target)
    {
        target.Armor += 2;
        target.Speed /= 2;
        target.gameObject.GetComponent<Rigidbody2D>().gravityScale *= 2;
    }

    public override void EndEffect(StatsManager target)
    {
        target.Armor -= 2;
        target.Speed *= 2;
        target.gameObject.GetComponent<Rigidbody2D>().gravityScale /= 2;
    }
}