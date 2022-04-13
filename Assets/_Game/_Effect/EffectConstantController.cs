using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConstantController : EffectController
{
    // Durée de l'effet
    private float duration; public float Duaration { get { return duration; } set { duration = value; } }

    void Start()
    {
        // Applique l'effet dès le début
        effet.Effect(Target);
        StartCoroutine(ApplyCoroutine());
    }

    /// <summary>
    /// Attend X secondes avant de mettre fin à l'effet
    /// </summary>
    IEnumerator ApplyCoroutine()
    {
        yield return new WaitForSeconds(duration);
            //Debug.Log("Fin coroutine");
        EndEffect();
    }

    public override void RefreshEffect()
    {
            //Debug.Log("Refresh" + effet.name);
        StopAllCoroutines();
        StartCoroutine(ApplyCoroutine());
    }
}