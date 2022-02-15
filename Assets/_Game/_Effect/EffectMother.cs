using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewEffectMother", menuName = "Game/EffectMother")]
public class EffectMother : ScriptableObject
{
    /// <summary>
    /// Enum des différents effets potentiels
    /// </summary>
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

    /// <summary>
    /// Enum des types d'effets (pour les effets qui interragissent entre eux)
    /// </summary>
    public enum TypeEffect
    {
        None,
        Fire,
        Cold
    }

    [SerializeField, Tooltip("Type de de l'effet (none si rien de spécial)")]
    protected TypeEffect typeEffect; public TypeEffect TypeOfTheEffect => typeEffect;

    [SerializeField, Tooltip("Couleur des particules")]
    protected Color colorEffect; public Color ColorEffect => colorEffect;

    [SerializeField, Tooltip("Prefab de l'objet pour les effets")]
    protected GameObject effectPrefab;

    [SerializeField, Tooltip("Stat affectée par l'effet")]
    protected AffectedValue affectedValue;

    [SerializeField, Tooltip("Modificateur de la stat (dégâts/bonus, pas la durée)")]
    protected int value;

    [SerializeField]
    protected ModifyStatEffect[] effectsNewVer;

    /// <summary>
    /// Crée et applique un GameObject d'effet à la cible
    /// </summary>
    /// <param name="target">StatsManager de la cible de l'effet</param>
    /// <returns>L'objet d'effet créé, setté et appliqué</returns>
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

        effectController.ParticleSystemEffect.startColor = colorEffect; // A corriger
        //effectController.ParticleSystemEffect.main = new ParticleSystem.MainModule();   // = colorEffect;

        return effect;
    }

    /// <summary>
    /// Ajoute le script d'effetController approprié au script et set les variables propres aux filles du script choisi
    /// </summary>
    /// <param name="effect">L'object d'effet créé</param>
    public virtual void AddEffectController(GameObject effect)
    {
        effect.AddComponent<EffectController>();
    }

    /// <summary>
    /// Modifie le StatManager selon les paramètres choisis
    /// </summary>
    /// <param name="target">StatsManager de la cible de l'effet</param>
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
                // La value ne change absolument rien ici
                target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                //target.GetComponent<CharacterMovement>() // Desactive
                Debug.Log($"Paralyse on X axis");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Se lance à la fin de l'effet (principalement pour dissiper des effets fixes)
    /// </summary>
    /// <param name="taget">StatsManager de la cible de l'effet</param>
    public virtual void EndEffect(StatsManager taget) { }
}


// Pour compacter la valeur affectée et le nombre, afin de pouvoir avoir une list de ça et donc des effets qui touchent plusieurs valeurs d'un coup
// (terminer le custom inspector pour être mieux utilisable)
[System.Serializable]
public struct ModifyStatEffect
{
    public EffectMother.AffectedValue typeValue;
    public float value;
}