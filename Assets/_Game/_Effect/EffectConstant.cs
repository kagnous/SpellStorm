using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectConstant", menuName = "Game/EffectConstant")]
public class EffectConstant : EffectMother
{
    [SerializeField, Tooltip("Durée de l'application de l'effet")]
    private float _duration;

    public override void AddEffectController(GameObject effect)
    {
        EffectConstantController effectController = effect.AddComponent<EffectConstantController>();
        effectController.Duaration = _duration;
    }

    // Refait Apply() à l'envert pour dissiper le modificateur activé
    public override void EndEffect(StatsManager target)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            switch (effects[i].affectedValue)
            {
                case AffectedValue.HP:
                    target.Heal(effects[i].value);
                    Debug.Log($"-{effects[i].value} HP");
                    break;
                case AffectedValue.Mana:
                    target.Mana -= effects[i].value;
                    Debug.Log($"-{effects[i].value} Mana");
                    break;
                case AffectedValue.Armor:
                    target.Armor -= effects[i].value;
                    Debug.Log($"-{effects[i].value} armor");
                    break;
                case AffectedValue.Speed:
                    target.Speed -= effects[i].value;
                    Debug.Log($"-{effects[i].value} speed");
                    break;
                case AffectedValue.Jump:
                    target.JumpForce -= effects[i].value;
                    Debug.Log($"-{effects[i].value} jump");
                    break;
                case AffectedValue.GravityScale:
                    target.gameObject.GetComponent<Rigidbody2D>().gravityScale -= effects[i].value;
                    Debug.Log($"-{effects[i].value} gravityScale");
                    break;
                case AffectedValue.Paralyse:
                    // La value ne change absolument rien ici
                    if (target.gameObject.tag == "Player")
                    {
                        target.GetComponent<CharacterMovement>().enabled = true;
                        target.GetComponent<CharacterMovement>().DirectionMovment = Vector2.zero;
                    }
                    else if (target.gameObject.tag == "Mob")
                    {
                        target.GetComponent<GoblinController>().IsFreeze = false;
                    }
                    Debug.Log($"End paralyse");
                    break;
                default:
                    break;
            }
        }
    }
}