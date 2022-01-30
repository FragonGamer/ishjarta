using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    private float speedDelay;

    public float Effect()
    {
        TickEffect();
        return speedDelay;
    }
}
