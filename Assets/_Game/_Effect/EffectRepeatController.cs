using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRepeatController : EffectController
{
    private int coup = 10; public int Coup { get { return coup; } set { coup = value; } }
    private int cadence = 1; public int Cadence { get { return cadence; } set { cadence = value; } }
    private int actualCoup;

    void Start()
    {
        actualCoup = 0;
        StartCoroutine(BurnCoroutine());
    }

    IEnumerator BurnCoroutine()
    {
        for ( ; actualCoup < coup; actualCoup ++)
        {
            yield return new WaitForSeconds(cadence);
            effet.Effect(target);
        }
        EndEffect();
    }

    public override void RefreshEffect()
    {
        actualCoup = coup;
    }
}