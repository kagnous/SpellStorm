using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // StatsManager de la cible de l'effet
    protected StatsManager target; public StatsManager Target { get { return target; } set { target = value; } }

    // Effect de r�f�rence de ce controller
    protected EffectMother effet; public EffectMother Effet { get { return effet; } set { effet = value; } }

    // Type de l'effet (pour les combinaisons �l�mentaires)
    protected EffectMother.TypeEffect typeEffect; public EffectMother.TypeEffect TypeOfTheEffect { get { return typeEffect; } set { typeEffect = value; } }

    // ParticleSystem pour la gestion graphique du sort
    protected ParticleSystem particleSystemEffect; public ParticleSystem ParticleSystemEffect { get { return particleSystemEffect; } set { particleSystemEffect = value; } }

    public void Awake()
    {
        particleSystemEffect = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Termine l'effet, retire le Controller de la liste de Stats puis se d�truit
    /// </summary>
    public virtual void EndEffect()
    {
        // Applique les consignes de fin d'effet dans le scriptable d'effet associ�
        effet.EndEffect(target);
        Debug.Log("Fin de l'effet");

        // Fouille dans la List des EffectsCOntrollers qui affectent la cible
        for (int i = 0; i < target.Effects.Count; i++)
        {
            // S'enl�ve de la List quand il se trouve lui m�me
            if (target.Effects[i] == this)
            {
                target.Effects.RemoveAt(i);
                break;
            }
        }
        // Destruction du gameObject
        Destroy(gameObject);
    }

    /// <summary>
    /// R�initialise la dur�e de l'effet
    /// </summary>
    public virtual void RefreshEffect() { }
}