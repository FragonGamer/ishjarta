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

    public float StrengthModifier { get; protected set; }

    public override float Effect()
    {
        TickEffect();
        return StrengthModifier;
    }

    private void Init(float duration, float strengthModifier)
    {
        Duration = duration;
        DurationRemaining = Duration;
        StrengthModifier = strengthModifier;
    }

    public static StrengthEffect CreateInstance(float duration, float strengthModifier)
    {
        var effect = ScriptableObject.CreateInstance<StrengthEffect>();
        effect.Init(duration, strengthModifier);
        return effect;
    }
}
