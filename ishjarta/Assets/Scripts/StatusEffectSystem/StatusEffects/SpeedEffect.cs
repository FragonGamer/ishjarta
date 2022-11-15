using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpeedEffect speeds an entity up
/// </summary>
[CreateAssetMenu(menuName = "SpeedEffect")]
public class SpeedEffect : BaseEffect
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
    /// This field specifies the amount of which the speed will be increased
    /// </summary>
    [field: SerializeField] public float SpeedModifier { get; protected set; }

    /// <summary>
    /// Executes the effect of SpeedEffect
    /// </summary>
    /// <returns>Returns SpeedModifier</returns>
    public override float Effect()
    {
        TickPerSecondEffect();
        return SpeedModifier;
    }

    /// <summary>
    /// Inits SpeedEffect
    /// </summary>
    /// <param name="duration">The Duration of SpeedEffect</param>
    /// <param name="speedModifier">The SpeedModifier of SpeedEffect</param>
    private void Init(float duration, float speedModifier)
    {
        Duration = duration;
        DurationRemaining = Duration;
        SpeedModifier = speedModifier;
    }
    /// <summary>
    /// Inits SpeedEffect
    /// </summary>
    /// <param name="isPermanent">Is SpeedEffect permanent</param>
    /// <param name="speedModifier">The SpeedModifier of SpeedEffect</param>
    private void Init(bool isPermanent, float speedModifier)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        SpeedModifier = speedModifier;
    }

    /// <summary>
    /// Creates an Instance of SpeedEffect
    /// </summary>
    /// <param name="duration">The Duration of SpeedEffect</param>
    /// <param name="speedModifier">The SpeedModifier of SpeedEffect</param>
    /// <returns>Returns an instance</returns>
    public static SpeedEffect CreateInstance(float duration, float speedModifier)
    {
        var effect = ScriptableObject.CreateInstance<SpeedEffect>();
        effect.Init(duration, speedModifier);
        return effect;
    }
    /// <summary>
    /// Creates an instance of SpeedEffect
    /// </summary>
    /// <param name="isPermanent">Is SpeedEffect permanent</param>
    /// <param name="speedModifier">The SpeedModifier of SpeedEffect</param>
    /// <returns>Returns an instance</returns>
    public static SpeedEffect CreateInstance(bool isPermanent, float speedModifier)
    {
        var effect = ScriptableObject.CreateInstance<SpeedEffect>();
        effect.Init(isPermanent, speedModifier);
        return effect;
    }
}
