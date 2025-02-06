using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCannon : MonoBehaviour
{
    [SerializeField, Tooltip("Durée entre les tirs")]
    private float cadenceCast;

    [SerializeField, Tooltip("Liste des spells lancés par l'objet")]
    private List<MagicSpell> spells;

    void Start()
    {
        StartCoroutine(CastCoroutine());
    }

    // Toute les X secondes, caster un sort random de la liste
    IEnumerator CastCoroutine()
    {
        while(true)
        {
        yield return new WaitForSeconds(cadenceCast);
        spells[Random.Range(0, spells.Count)].Cast(gameObject);
        }
    }
}
