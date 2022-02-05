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

    public int Regeneration { get; protected set; }

    public override int Effect(int health)
    {
        TickEffect();
        return health + Regeneration;
    }

    private void Init(float duration, int regeneration)
    {
        Duration = duration;
        DurationRemaining = Duration;
        Regeneration = regeneration;
    }

    public static RegenerationEffect CreateInstance(float duration, int regeneration)
    {
        var effect = ScriptableObject.CreateInstance<RegenerationEffect>();
        effect.Init(duration, regeneration);
        return effect;
    }
}
