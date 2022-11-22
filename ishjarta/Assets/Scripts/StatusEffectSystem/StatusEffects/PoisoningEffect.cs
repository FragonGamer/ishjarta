using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PoisoningEffect damages an entity over a certain time
/// </summary>
[CreateAssetMenu(menuName = "PoisoningEffect")]
public class PoisoningEffect : BaseEffect
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
    /// This field specifies the amount of damage that will be given
    /// </summary>
    [field: SerializeField] public int PoisonDamage { get; protected set; }

    /// <summary>
    /// Executes the effect of PoisoningEffect
    /// </summary>
    /// <param name="health">The health of the entity</param>
    /// <returns>Returns the damaged health of the entity</returns>
    public override int Effect(int health)
    {
        TickPerSecondEffect();
        
        return health - PoisonDamage;
    }

    /// <summary>
    /// Inits PoisoningEffect
    /// </summary>
    /// <param name="duration">The Duration of PoisoningEffect</param>
    /// <param name="poisonDamage">The PoisonDamage of PoisoningEffect</param>
    private void Init(float duration, int poisonDamage)
    {
        Duration = duration;
        DurationRemaining = Duration;
        PoisonDamage = poisonDamage;
    }
    /// <summary>
    /// Inits PoisoningEffect
    /// </summary>
    /// <param name="isPermanent">Is PoisoningEffect permanent</param>
    /// <param name="poisonDamage">The PoisonDamage of PoisoningEffect</param>
    private void Init(bool isPermanent, int poisonDamage)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        PoisonDamage = poisonDamage;
    }

    /// <summary>
    /// Creates an instance of PoisoningEffect
    /// </summary>
    /// <param name="duration">The Duration of PoisoningEffect</param>
    /// <param name="poisonDamage">The PoisonDamage of PoisoningEffect></param>
    /// <returns>Returns an instance</returns>
    public static PoisoningEffect CreateInstance(float duration, int poisonDamage)
    {
        var effect = ScriptableObject.CreateInstance<PoisoningEffect>();
        effect.Init(duration, poisonDamage);
        return effect;
    }
    /// <summary>
    /// Creates an instance of PoisoningEffect
    /// </summary>
    /// <param name="isPermanent">Is PoisoningEffect permanent</param>
    /// <param name="poisonDamage">The PoisonDamage of PoisoningEffect></param>
    /// <returns>Returns an instance</returns>
    public static PoisoningEffect CreateInstance(bool isPermanent, int poisonDamage)
    {
        var effect = ScriptableObject.CreateInstance<PoisoningEffect>();
        effect.Init(isPermanent, poisonDamage);
        return effect;
    }
}
