using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StrengthEffect increases the strength of an entity
/// </summary>
[CreateAssetMenu]
public class StrengthEffect : BaseEffect
{
    /// <summary>
    /// Will be executed as the first method
    /// </summary>
    void start()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    /// <summary>
    /// This field specifies the amount of which the strength will be increased
    /// </summary>
    [field: SerializeField] public float StrengthModifier { get; protected set; }

    /// <summary>
    /// Executes the effect of SpeedEffect
    /// </summary>
    /// <returns>Returns StrengthModifier</returns>
    public override float Effect()
    {
        TickPerSecondEffect();
        return StrengthModifier;
    }

    /// <summary>
    /// Inits StrengthEffect
    /// </summary>
    /// <param name="duration">The Duration of StrengthEffect</param>
    /// <param name="strengthModifier">The StrengthModifier of StrengthEffect</param>
    private void Init(float duration, float strengthModifier)
    {
        Duration = duration;
        DurationRemaining = Duration;
        StrengthModifier = strengthModifier;
    }
    /// <summary>
    /// Inits StrengthEffect
    /// </summary>
    /// <param name="isPermanent">Is StrengthEffect permanent</param>
    /// <param name="strengthModifier">The StrengthModifier of StrengthEffect</param>
    private void Init(bool isPermanent, float strengthModifier)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        StrengthModifier = strengthModifier;
    }

    /// <summary>
    /// Creates an instance of StrengthEffect
    /// </summary>
    /// <param name="duration">The Duration of StrengthEffect</param>
    /// <param name="strengthModifier">The StrengthModifier of StrengthEffect</param>
    /// <returns>Returns an instance</returns>
    public static StrengthEffect CreateInstance(float duration, float strengthModifier)
    {
        var effect = ScriptableObject.CreateInstance<StrengthEffect>();
        effect.Init(duration, strengthModifier);
        return effect;
    }

    /// <summary>
    /// Creates an instance of StrengthEffect
    /// </summary>
    /// <param name="isPermanent">Is StrengthEffect permanent</param>
    /// <param name="strengthModifier">The StrengthModifier of StrengthEffect</param>
    /// <returns>Returns an instance</returns>
    public static StrengthEffect CreateInstance(bool isPermanent, float strengthModifier)
    {
        var effect = ScriptableObject.CreateInstance<StrengthEffect>();
        effect.Init(isPermanent, strengthModifier);
        return effect;
    }
}
