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
        Physical,
        Fire,
        Cold
    }

    [SerializeField, Tooltip("Type de de l'effet (none si rien de spécial)")]
    protected TypeEffect typeEffect; public TypeEffect TypeOfTheEffect => typeEffect;

    [SerializeField, Tooltip("Couleur des particules")]
    protected Color colorEffect; public Color ColorEffect => colorEffect;

    [SerializeField, Tooltip("Prefab de l'objet pour les effets")]
    protected GameObject effectPrefab;

    [SerializeField]
    protected ModifyStatEffect[] effects;

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

        ParticleSystem.MainModule newMainModule = effectController.ParticleSystemEffect.main;
        newMainModule.startColor = colorEffect;

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
        for (int i = 0; i < effects.Length; i++)
        {
            switch (effects[i].affectedValue)
            {
                case AffectedValue.HP:
                    target.ElementalDamage(effects[i].value);
                    Debug.Log($"-{effects[i].value} HP");
                    break;
                case AffectedValue.Mana:
                    target.Mana += effects[i].value;
                    Debug.Log($"+{effects[i].value} Mana");
                    break;
                case AffectedValue.Armor:
                    target.Armor += effects[i].value;
                    Debug.Log($"+{effects[i].value} armor");
                    break;
                case AffectedValue.Speed:
                    target.Speed += effects[i].value;
                    Debug.Log($"+{effects[i].value} speed");
                    break;
                case AffectedValue.Jump:
                    target.JumpForce += effects[i].value;
                    Debug.Log($"+{effects[i].value} jump");
                    break;
                case AffectedValue.GravityScale:
                    target.gameObject.GetComponent<Rigidbody2D>().gravityScale += effects[i].value;
                    Debug.Log($"+{effects[i].value} gravityScale");
                    break;
                case AffectedValue.Paralyse:
                    // Propetry Drawer pour masquer la value qui sert à rien ici
                    if (target.gameObject.tag == "Player")
                    {
                        target.GetComponent<Animator>().enabled = false;
                        target.GetComponent<PlayerController>().StopMoveInput();
                        target.GetComponent<SpriteRenderer>().color = new Color(0, 138, 255, 255);
                    }
                    else if(target.gameObject.tag == "Mob")
                    {
                        target.GetComponent<EnnemiController>().State = EnnemiController.EnnemiState.freeze;
                        target.GetComponent<Animator>().enabled = false;
                        target.GetComponent<SpriteRenderer>().color = new Color(0, 138, 255, 255);
                    }
                    Debug.Log($"Paralyse");
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Se lance à la fin de l'effet (principalement pour dissiper des effets fixes)
    /// </summary>
    /// <param name="taget">StatsManager de la cible de l'effet</param>
    public virtual void EndEffect(StatsManager taget) { }
}


// Pour compacter la valeur affectée et le nombre, afin de pouvoir avoir une list de ça et donc des effets qui touchent plusieurs valeurs d'un coup
[System.Serializable]
public struct ModifyStatEffect
{
    public EffectMother.AffectedValue affectedValue;
    public int value;
}