using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    private float strengthModifier;

    public float Effect()
    {
        TickEffect();
        return strengthModifier;
    }
}
