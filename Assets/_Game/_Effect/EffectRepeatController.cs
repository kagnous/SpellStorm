using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRepeatController : EffectController
{
    // Nombre de fois que Apply seras appel�
    private int coup = 10; public int Coup { get { return coup; } set { coup = value; } }
    // Intervalle de temps entre les Apply
    private int cadence = 1; public int Cadence { get { return cadence; } set { cadence = value; } }

    // Compteur de coup appliqu�
    private int actualCoup;

    void Start()
    {
        actualCoup = 0;
        StartCoroutine(BurnCoroutine());
    }

    IEnumerator BurnCoroutine()
    {
        // Tant qu'on a pas donn� autant de coup que demand�
        for ( ; actualCoup < coup; actualCoup ++)
        {
            // On attend
            yield return new WaitForSeconds(cadence);
            // Puis on r�applique l'effet
            effet.Effect(target);
        }
        EndEffect();
    }

    public override void RefreshEffect()
    {
        Debug.Log("Refresh" + effet.name);
        actualCoup = 0;
    }
}