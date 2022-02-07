using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpeedEffect")]
public class SpeedEffect : BaseEffect
{
    new void Awake()
    {
        DurationRemaining = Duration;
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    [SerializeField] public float SpeedModifier { get; protected set; }

    public override float Effect()
    {
        TickEffect();
        return SpeedModifier;
    }

    private void Init(float duration, float speedModifier)
    {
        Duration = duration;
        DurationRemaining = LastSecond = Duration;
        SpeedModifier = speedModifier;
    }
    private void Init(bool isPermanent, float speedModifier)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = LastSecond = Duration;
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
