using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectConstant", menuName = "Game/EffectConstant")]
public class EffectConstant : EffectMother
{
    [SerializeField]
    private float _duration;

    [SerializeField]
    private int value;

    // Pour quand le Property drawer fonctionnera
    //[SerializeField]
    //private TemporaryModifyStatEffect effect;

    public override void AddApply(GameObject effect)
    {
        EffectConstantController effectController = effect.GetComponent<EffectConstantController>();
        effectController.Duaration = _duration;
    }

    public override void Effect(StatsManager target)
    {
        switch (affectedValue)
        {
            case AffectedValue.HP:
                target.HP += value;
                Debug.Log($"+{value} HP");
                break;
            case AffectedValue.Mana:
                target.Mana += value;
                Debug.Log($"+{value} Mana");
                break;
            case AffectedValue.Armor:
                target.Armor += value;
                Debug.Log($"+{value} armor");
                break;
            case AffectedValue.Speed:
                target.Speed += value;
                Debug.Log($"+{value} speed");
                break;
            case AffectedValue.GravityScale:
                target.gameObject.GetComponent<Rigidbody2D>().gravityScale += value;
                Debug.Log($"+{value} gravityScale");
                break;
            case AffectedValue.Paralyse:
                target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                Debug.Log($"+{value} gravityScale");
                break;
            default:
                break;
        }
    }

    public override void EndEffect(StatsManager target)
    {
        switch (affectedValue)
        {
            case AffectedValue.HP:
                target.HP -= value;
                Debug.Log($"-{value} HP");
                break;
            case AffectedValue.Mana:
                target.Mana -= value;
                Debug.Log($"-{value} Mana");
                break;
            case AffectedValue.Armor:
                target.Armor -= value;
                Debug.Log($"-{value} armor");
                break;
            case AffectedValue.Speed:
                target.Speed -= value;
                Debug.Log($"-{value} speed");
                break;
            case AffectedValue.GravityScale:
                target.gameObject.GetComponent<Rigidbody2D>().gravityScale -= value;
                Debug.Log($"-{value} gravityScale");
                break;
            case AffectedValue.Paralyse:
                target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Debug.Log($"+{value} gravityScale");
                break;
            default:
                break;
        }
    }
}