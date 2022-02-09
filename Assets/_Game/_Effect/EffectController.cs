using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    protected StatsManager target; public StatsManager Target { get { return target; } set { target = value; } }

    protected EffectMother effet; public EffectMother Effet { get { return effet; } set { effet = value; } }

    public virtual void RefreshEffect() { }
}