using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectRepeat", menuName = "Game/EffectRepeat")]
public class EffectRepeat : EffectMother
{
    [SerializeField]
    private int coup = 10;
    [SerializeField]
    private int cadence = 1;
    [SerializeField]
    private int value;

    public override void AddApply(GameObject effect)
    {
        EffectRepeatController effectController = effect.GetComponent<EffectRepeatController>();
        effectController.Coup = coup;
        effectController.Cadence = cadence;
    }

    public override void Effect(StatsManager target)
    {
        switch(affectedValue)
        {
            case AffectedValue.HP:
                target.HP -= value;
                Debug.Log($"-{value} HP");
                break;
            case AffectedValue.Mana:
                target.Mana -= value;
                Debug.Log($"-{value} Mana");
                break;
            default:
                break;
        }
    }
}