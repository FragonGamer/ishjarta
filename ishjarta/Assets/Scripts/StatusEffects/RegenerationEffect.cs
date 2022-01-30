using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    private int regeneration;

    public int Effect(int health)
    {
        TickEffect();
        return health + regeneration;
    }
}
