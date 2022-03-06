using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerationEffect : BaseEffect
{
    void start()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = true;
    }

    public float ResistanceReduction { get; protected set; }
    public int IncinerationDamage { get; protected set; }
    public bool HasIncinerated = false;

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

    private void Init(float duration, float resistanceReduction, int incinerationDamage)
    {
        Duration = duration;
        DurationRemaining = Duration;
        ResistanceReduction = resistanceReduction;
        IncinerationDamage = incinerationDamage;
        HasIncinerated = false;
    }

    private void Init(bool isPermanent, float resistanceReduction, int incinerationDamage)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = Duration;
        ResistanceReduction = resistanceReduction;
        IncinerationDamage = incinerationDamage;
        HasIncinerated = false;
    }

    public static IncinerationEffect CreateInstance(float duration, float resistanceReduction, int incinerationDamage)
    {
        var effect = ScriptableObject.CreateInstance<IncinerationEffect>();
        effect.Init(duration, resistanceReduction, incinerationDamage);
        return effect;
    }
    public static IncinerationEffect CreateInstance(bool isPermanent, float resistanceReduction, int incinerationDamage)
    {
        var effect = ScriptableObject.CreateInstance<IncinerationEffect>();
        effect.Init(isPermanent, resistanceReduction, incinerationDamage);
        return effect;
    }
}
