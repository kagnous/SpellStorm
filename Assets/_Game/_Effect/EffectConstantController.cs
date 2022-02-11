using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConstantController : EffectController
{
    private float duration; public float Duaration { get { return duration; } set { duration = value; } }

    void Start()
    {
        StartCoroutine(ApplyCoroutine());
        effet.Effect(Target);
    }

    IEnumerator ApplyCoroutine()
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Fin coroutine");
        EndEffect();
    }

    public override void RefreshEffect()
    {
        //Debug.Log("Refresh" + effet.name);
        // A trouver
    }
}
