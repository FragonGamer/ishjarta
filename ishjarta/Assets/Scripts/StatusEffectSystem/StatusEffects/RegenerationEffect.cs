using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RegenerationEffect heals an entity over a certain time
/// </summary>
public class RegenerationEffect : BaseEffect
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
    /// This field specifies the amount of regeneration
    /// </summary>
    public int Regeneration { get; protected set; }

    /// <summary>
    /// Executes the effect of RegenerationEffect
    /// </summary>
    /// <param name="health">The health of the entity</param>
    /// <returns>Returns the regenerated health of the entity</returns>
    public override int Effect(int health)
    {
        TickPerSecondEffect();

        return health + Regeneration;
    }

    /// <summary>
    /// Inits RegenerationEffect
    /// </summary>
    /// <param name="duration">The Duration of RegenerationEffect</param>
    /// <param name="regeneration">The Regeneration of RegenerationEffect</param>
    private void Init(float duration, int regeneration)
    {
        Duration = duration;
        DurationRemaining = Duration;
        Regeneration = regeneration;
    }
    /// <summary>
    /// Inits RegenerationEffect
    /// </summary>
    /// <param name="isPermanent">Is RegenerationEffect permanent</param>
    /// <param name="regeneration">The Regeneration of RegenerationEffect</param>
    private void Init(bool isPermanent, int regeneration)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        Regeneration = regeneration;
    }

    /// <summary>
    /// Creates an instance of RegenerationEffect
    /// </summary>
    /// <param name="duration">The Duration of RegenerationEffect</param>
    /// <param name="regeneration">The Regeneration of RegenerationEffect</param>
    /// <returns>Returns an instance</returns>
    public static RegenerationEffect CreateInstance(float duration, int regeneration)
    {
        var effect = ScriptableObject.CreateInstance<RegenerationEffect>();
        effect.Init(duration, regeneration);
        return effect;
    }
    /// <summary>
    /// Creates an instance of RegenerationEffect
    /// </summary>
    /// <param name="isPermanent">Is RegenerationEffect permanent</param>
    /// <param name="regeneration">The Regeneration of RegenerationEffect</param>
    /// <returns>Returns an instance</returns>
    public static RegenerationEffect CreateInstance(bool isPermanent, int regeneration)
    {
        var effect = ScriptableObject.CreateInstance<RegenerationEffect>();
        effect.Init(isPermanent, regeneration);
        return effect;
    }
}
