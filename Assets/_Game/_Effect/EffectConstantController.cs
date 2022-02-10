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
        EndEffect();
    }

    public override void RefreshEffect()
    {
        // A trouver
    }
}
