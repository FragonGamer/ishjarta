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
        TickEffect();
        return SpeedDelay;
    }

    private void Init(float duration, float speedDelay)
    {
        Duration = duration;
        DurationRemaining = LastSecond = Duration;
        SpeedDelay = speedDelay;
    }

    public static FrostEffect CreateInstance(float duration, float speedDelay)
    {
        var effect = ScriptableObject.CreateInstance<FrostEffect>();
        effect.Init(duration, speedDelay);
        return effect;
    }
}
