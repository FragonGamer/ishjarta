using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IncinerationEffect damages an entity and reduces its resistance over a certain time
/// </summary>
public class IncinerationEffect : BaseEffect
{
    /// <summary>
    /// Will be executed as the first method
    /// </summary>
    void start()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = true;
    }

    /// <summary>
    /// This field specifies the amount of which the resistance will be reduced
    /// </summary>
    public float ResistanceReduction { get; protected set; }

    /// <summary>
    /// This field specifies the amount of damage that will be given
    /// </summary>
    public int IncinerationDamage { get; protected set; }

    /// <summary>
    /// This field specifies if IncinerationDamage has already been given
    /// </summary>
    public bool HasIncinerated = false;

    /// <summary>
    /// Executes the effect of IncinerationEffect
    /// </summary>
    /// <param name="resistance">The resistance of the entity</param>
    /// <param name="health">The health of the entity</param>
    /// <returns>Returns ResistanceReduction</returns>
    public override float Effect(float resistance, ref int health)
    {
        TickPerSecondEffect();
        if(!HasIncinerated)
        {
            health -= IncinerationDamage;
            HasIncinerated = true;
        }
        //return resistance - ResistanceReduction;
        return ResistanceReduction;
    }

    /// <summary>
    /// Inits IncinerationEffect
    /// </summary>
    /// <param name="duration">The Duration of IncinerationEffect</param>
    /// <param name="resistanceReduction">The ResistanceReduction of IncinerationEffect</param>
    /// <param name="incinerationDamage">The IncinerationDamage of IncinerationEffect</param>
    private void Init(float duration, float resistanceReduction, int incinerationDamage)
    {
        Duration = duration;
        DurationRemaining = Duration;
        ResistanceReduction = resistanceReduction;
        IncinerationDamage = incinerationDamage;
        HasIncinerated = false;
    }

    /// <summary>
    /// Inits IncinerationEffect
    /// </summary>
    /// <param name="isPermanent">Is IncinerationEffect permanent</param>
    /// <param name="resistanceReduction">The ResistanceReduction of IncinerationEffect</param>
    /// <param name="incinerationDamage">The IncinerationDamage of IncinerationEffect</param>
    private void Init(bool isPermanent, float resistanceReduction, int incinerationDamage)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        ResistanceReduction = resistanceReduction;
        IncinerationDamage = incinerationDamage;
        HasIncinerated = false;
    }

    /// <summary>
    /// Creates an instance of IncinerationEffect
    /// </summary>
    /// <param name="duration">The Duration of IncinerationEffect</param>
    /// <param name="resistanceReduction">The ResistanceReduction of IncinerationEffect</param>
    /// <param name="incinerationDamage">The IncinerationDamage of IncinerationEffect</param>
    /// <returns>Returns an instance</returns>
    public static IncinerationEffect CreateInstance(float duration, float resistanceReduction, int incinerationDamage)
    {
        var effect = ScriptableObject.CreateInstance<IncinerationEffect>();
        effect.Init(duration, resistanceReduction, incinerationDamage);
        return effect;
    }
    /// <summary>
    /// Creates an instance of IncinerationEffect
    /// </summary>
    /// <param name="isPermanent">Is IncinerationEffect permanent</param>
    /// <param name="resistanceReduction">The ResistanceReduction of IncinerationEffect</param>
    /// <param name="incinerationDamage">The IncinerationDamage of IncinerationEffect</param>
    /// <returns>Returns an instance</returns>
    public static IncinerationEffect CreateInstance(bool isPermanent, float resistanceReduction, int incinerationDamage)
    {
        var effect = ScriptableObject.CreateInstance<IncinerationEffect>();
        effect.Init(isPermanent, resistanceReduction, incinerationDamage);
        return effect;
    }
}
