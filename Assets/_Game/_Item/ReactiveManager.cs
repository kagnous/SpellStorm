using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveManager : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    private EffectMother.TypeEffect[] _sensitivity;
    
    public void SufferEffect(EffectMother.TypeEffect effect)
    {
        for (int i = 0; i < _sensitivity.Length; i++)
        {
            if(effect == _sensitivity[i])
            {
                Destroy(gameObject);
            }
        }
    }
}