using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FrostEffect slows an entity down
/// </summary>
public class FrostEffect : BaseEffect
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
    /// This field specifies the amount of which the speed will be delayed
    /// </summary>
    public float SpeedDelay { get; protected set; }

    /// <summary>
    /// Executes the effect of FrostEffect
    /// </summary>
    /// <returns>Returns SpeedDelay</returns>
    public override float Effect()
    {
        TickPerSecondEffect();
        return SpeedDelay;
    }

    /// <summary>
    /// Inits FrostEffect
    /// </summary>
    /// <param name="duration">The Duration of FrostEffect</param>
    /// <param name="speedDelay">The SpeedDelay of FrostEffect</param>
    private void Init(float duration, float speedDelay)
    {
        Duration = duration;
        DurationRemaining  = Duration;
        SpeedDelay = speedDelay;
    }

    /// <summary>
    /// Inits FrostEffect
    /// </summary>
    /// <param name="isPermanent">Is FrostEffect permanent</param>
    /// <param name="speedDelay">The SpeedDelay of FrostEffect</param>
    private void Init(bool isPermanent, float speedDelay)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        SpeedDelay = speedDelay;
    }

    /// <summary>
    /// Creates an instance of FrostEffect
    /// </summary>
    /// <param name="duration">The Duration of FrostEffect</param>
    /// <param name="speedDelay">The SpeedDelay of FrostEffect</param>
    /// <returns>Returns an instance</returns>
    public static FrostEffect CreateInstance(float duration, float speedDelay)
    {
        var effect = ScriptableObject.CreateInstance<FrostEffect>();
        effect.Init(duration, speedDelay);
        return effect;
    }

    /// <summary>
    /// Creates an instance of FrostEffect
    /// </summary>
    /// <param name="isPermanent">Is FrostEffect permanent</param>
    /// <param name="speedDelay">The SpeedDelay of FrostEffect</param>
    /// <returns>Returns an instance</returns>
    public static FrostEffect CreateInstance(bool isPermanent, float speedDelay)
    {
        var effect = ScriptableObject.CreateInstance<FrostEffect>();
        effect.Init(isPermanent, speedDelay);
        return effect;
    }
}
