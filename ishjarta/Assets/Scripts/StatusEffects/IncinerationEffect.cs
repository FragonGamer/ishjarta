using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerationEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = true;
    }

    private float resistanceReduction;
    private int incinerationDamage;
    public bool HasIncinerated= false;

    public float Effect(float resistance, ref int health)
    {
        TickEffect();
        if(!HasIncinerated)
        {
            health -= incinerationDamage;
            HasIncinerated = true;
        }
        return resistance - resistanceReduction;
    }
}
