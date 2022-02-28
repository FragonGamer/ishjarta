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

    public float SpeedDelay { get; protected set; }

    public override float Effect()
    {
        TickPerSecondEffect();
        return SpeedDelay;
    }

    private void Init(float duration, float speedDelay)
    {
        Duration = duration;
        DurationRemaining  = Duration;
        SpeedDelay = speedDelay;
    }

    private void Init(bool isPermanent, float speedDelay)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        SpeedDelay = speedDelay;
    }

    public static FrostEffect CreateInstance(float duration, float speedDelay)
    {
        var effect = ScriptableObject.CreateInstance<FrostEffect>();
        effect.Init(duration, speedDelay);
        return effect;
    }

    public static FrostEffect CreateInstance(bool isPermanent, float speedDelay)
    {
        var effect = ScriptableObject.CreateInstance<FrostEffect>();
        effect.Init(isPermanent, speedDelay);
        return effect;
    }
}
