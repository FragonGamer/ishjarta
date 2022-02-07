using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerationEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = true;
    }

    public float ResistanceReduction { get; protected set; }
    public int IncinerationDamage { get; protected set; }
    public bool HasIncinerated = false;

    public override float Effect(float resistance, ref int health)
    {
        TickEffect();
        if(!HasIncinerated)
        {
            health -= IncinerationDamage;
            HasIncinerated = true;
        }
        return resistance - ResistanceReduction;
    }

    private void Init(float duration, float resistanceReduction, int incinerationDamage)
    {
        Duration = duration;
        DurationRemaining = LastSecond = Duration;
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
}
