using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    protected StatsManager target; public StatsManager Target { get { return target; } set { target = value; } }

    protected EffectMother effet; public EffectMother Effet { get { return effet; } set { effet = value; } }

    protected EffectMother.TypeEffect typeEffect; public EffectMother.TypeEffect TypeOfTheEffect { get { return typeEffect; } set { typeEffect = value; } }

    protected ParticleSystem particleSystemEffect; public ParticleSystem ParticleSystemEffect { get { return particleSystemEffect; } set { particleSystemEffect = value; } }

    public void Awake()
    {
        particleSystemEffect = GetComponent<ParticleSystem>();
    }

    public virtual void EndEffect()
    {
        effet.EndEffect(target);
        Debug.Log("Fin de l'effet");

        for (int i = 0; i < target.Effects.Count; i++)
        {
            if (target.Effects[i] == this)
                target.Effects.RemoveAt(i);
        }
        Destroy(gameObject);
    }

    public virtual void RefreshEffect() { }
}