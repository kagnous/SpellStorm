using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCannon : MonoBehaviour
{
    [SerializeField]
    private float cadenceCast;

    [SerializeField]
    private List<MagicSpell> spells;

    void Start()
    {
        StartCoroutine(CastCoroutine());
    }

    IEnumerator CastCoroutine()
    {
        while(true)
        {
        yield return new WaitForSeconds(cadenceCast);
        spells[Random.Range(0, spells.Count)].Cast(gameObject);
        }
    }
}
