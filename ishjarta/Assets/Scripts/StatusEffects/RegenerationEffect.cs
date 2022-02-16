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
        TickPerSecondEffect();

        return health + Regeneration;
    }

    private void Init(float duration, int regeneration)
    {
        Duration = duration;
        DurationRemaining = Duration;
        Regeneration = regeneration;
    }
    private void Init(bool isPermanent, int regeneration)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        Regeneration = regeneration;
    }

    public static RegenerationEffect CreateInstance(float duration, int regeneration)
    {
        var effect = ScriptableObject.CreateInstance<RegenerationEffect>();
        effect.Init(duration, regeneration);
        return effect;
    }
    public static RegenerationEffect CreateInstance(bool isPermanent, int regeneration)
    {
        var effect = ScriptableObject.CreateInstance<RegenerationEffect>();
        effect.Init(isPermanent, regeneration);
        return effect;
    }
}
