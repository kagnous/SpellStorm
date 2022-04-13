using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConstantController : EffectController
{
    // Dur�e de l'effet
    private float duration; public float Duaration { get { return duration; } set { duration = value; } }

    void Start()
    {
        // Applique l'effet d�s le d�but
        effet.Effect(Target);
        StartCoroutine(ApplyCoroutine());
    }

    /// <summary>
    /// Attend X secondes avant de mettre fin � l'effet
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