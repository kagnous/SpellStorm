using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectConstant", menuName = "Game/EffectConstant")]
public class EffectConstant : EffectMother
{
    [SerializeField, Tooltip("Durée de l'application de l'effet")]
    private float _duration;

    // Pour quand le Property drawer fonctionnera
    //[SerializeField]
    //private TemporaryModifyStatEffect effect;

    public override void AddEffectController(GameObject effect)
    {
        EffectConstantController effectController = effect.AddComponent<EffectConstantController>();
        effectController.Duaration = _duration;
    }

    // Refait Apply() à l'envert pour dissiper le modificateur activé
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
            case AffectedValue.Jump:
                target.JumpForce -= value;
                Debug.Log($"-{value} jump");
                break;
            case AffectedValue.GravityScale:
                target.gameObject.GetComponent<Rigidbody2D>().gravityScale -= value;
                Debug.Log($"-{value} gravityScale");
                break;
            case AffectedValue.Paralyse:
                target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Debug.Log($"Freeze axe X");
                break;
            default:
                break;
        }
    }
}