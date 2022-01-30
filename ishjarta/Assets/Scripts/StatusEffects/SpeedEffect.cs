using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    private float speedModifier;

    public float Effect()
    {
        TickEffect();
        return speedModifier;
    }
}
