using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpeedEffect")]
public class SpeedEffect : BaseEffect
{
    void start()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    [field: SerializeField] public float SpeedModifier { get; protected set; }

    public override float Effect()
    {
        TickPerSecondEffect();
        return SpeedModifier;
    }

    private void Init(float duration, float speedModifier)
    {
        Duration = duration;
        DurationRemaining = Duration;
        SpeedModifier = speedModifier;
    }
    private void Init(bool isPermanent, float speedModifier)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        SpeedModifier = speedModifier;
    }

    public static SpeedEffect CreateInstance(float duration, float speedModifier)
    {
        var effect = ScriptableObject.CreateInstance<SpeedEffect>();
        effect.Init(duration, speedModifier);
        return effect;
    }

    public static SpeedEffect CreateInstance(bool isPermanent, float speedModifier)
    {
        var effect = ScriptableObject.CreateInstance<SpeedEffect>();
        effect.Init(isPermanent, speedModifier);
        return effect;
    }
}
