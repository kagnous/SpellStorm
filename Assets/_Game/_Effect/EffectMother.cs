using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewEffectMother", menuName = "Game/EffectMother")]
public class EffectMother : ScriptableObject
{
    public enum AffectedValue
    {
        HP,
        Mana,
        Armor,
        Speed,
        Jump,
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
    protected Color colorEffect; public Color ColorEffect => colorEffect;

    [SerializeField]
    protected GameObject effectPrefab;

    [SerializeField]
    protected AffectedValue affectedValue;

    [SerializeField]
    protected int value;

    public virtual GameObject Apply(StatsManager target)
    {
        // Création du GameObject
        GameObject effect = Instantiate(effectPrefab,target.transform);

        // Ajout du script, overridable par les filles qui pourront mettre un script plus complexe et initialiser leurs valeurs propres
        AddEffectController(effect);

        // Settings des valeurs d'EffectController de base
        EffectController effectController =  effect.GetComponent<EffectController>();
        effectController.Target = target;
        effectController.Effet = this;
        effectController.TypeOfTheEffect = typeEffect;
        effectController.ParticleSystemEffect.startColor = colorEffect;
        //effectController.ParticleSystemEffect.main = new ParticleSystem.MainModule();   // = colorEffect;

        return effect;
    }

    public virtual void AddEffectController(GameObject effect)
    {
        effect.AddComponent<EffectController>();
    }

    public virtual void Effect(StatsManager target)
    {
        switch (affectedValue)
        {
            case AffectedValue.HP:
                target.ElementalDamage(value);
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
            case AffectedValue.Jump:
                target.JumpForce += value;
                Debug.Log($"+{value} jump");
                break;
            case AffectedValue.GravityScale:
                target.gameObject.GetComponent<Rigidbody2D>().gravityScale += value;
                Debug.Log($"+{value} gravityScale");
                break;
            case AffectedValue.Paralyse:
                target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                Debug.Log($"Paralyse on X axis");
                break;
            default:
                break;
        }
    }

    public virtual void EndEffect(StatsManager taget) { }
}

[System.Serializable]
public struct TemporaryModifyStatEffect
{
    EffectMother.AffectedValue typeValue;
    public float value;
}