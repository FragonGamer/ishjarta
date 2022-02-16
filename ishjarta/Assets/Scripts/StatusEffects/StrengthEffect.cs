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
        TickPerSecondEffect();
        return StrengthModifier;
    }

    private void Init(float duration, float strengthModifier)
    {
        Duration = duration;
        DurationRemaining = Duration;
        StrengthModifier = strengthModifier;
    }
    private void Init(bool isPermanent, float strengthModifier)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        StrengthModifier = strengthModifier;
    }

    public static StrengthEffect CreateInstance(float duration, float strengthModifier)
    {
        var effect = ScriptableObject.CreateInstance<StrengthEffect>();
        effect.Init(duration, strengthModifier);
        return effect;
    }

    public static StrengthEffect CreateInstance(bool isPermanent, float strengthModifier)
    {
        var effect = ScriptableObject.CreateInstance<StrengthEffect>();
        effect.Init(isPermanent, strengthModifier);
        return effect;
    }
}
