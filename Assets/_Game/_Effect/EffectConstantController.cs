using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConstantController : EffectController
{
    private float duration; public float Duaration { get { return duration; } set { duration = value; } }

    void Start()
    {
        StartCoroutine(ApplyCoroutine());
    }

    IEnumerator ApplyCoroutine()
    {
        effet.Effect(Target);

        yield return new WaitForSeconds(duration);
        Debug.Log("Fin de l'effet");
        effet.EndEffect(target);

        for (int i = 0; i < target.Effects.Count; i++)
        {
            if (target.Effects[i] == effet)
                target.Effects.RemoveAt(i);
        }
        Destroy(gameObject);
    }

    public override void RefreshEffect()
    {
        // A trouver
    }
}
