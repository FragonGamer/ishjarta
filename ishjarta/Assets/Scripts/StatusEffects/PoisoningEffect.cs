using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisiningEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    private int poisoning;

    public int Effect(int health)
    {
        TickEffect();
        return health - poisoning;
    }
}
