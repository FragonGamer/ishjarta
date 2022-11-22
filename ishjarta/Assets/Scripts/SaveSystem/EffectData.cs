using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for Effect
/// </summary>
[System.Serializable]
public class EffectData
{
    public float duration;

    public bool isPermanent = false;

    public EffectType effectType;

    // Modifier for Speed, Strength and Resistance
    public float powerModifier;

    // Healthchange by Poisoning, Incineration or Regeneration
    public int healthChange;

    public EffectData(BaseEffect baseEffect)
    {
        //if (baseEffect == null)
        //    throw new ArgumentNullException("Should not hand EffectData a null over");

        this.duration = baseEffect.Duration;
        this.isPermanent = baseEffect.IsPermanent;

        if (baseEffect is FrostEffect fe)
        {
            effectType = EffectType.FrostEffect;
            this.powerModifier = fe.SpeedDelay;
        }
        else if (baseEffect is PoisoningEffect pe)
        {
            effectType = EffectType.PoisoningEffect;
            this.healthChange = pe.PoisonDamage;
        }
        else if (baseEffect is IncinerationEffect ie)
        {
            effectType= EffectType.IncinerationEffect;
            this.powerModifier = ie.ResistanceReduction;
            this.healthChange = ie.IncinerationDamage;
        }
        else if (baseEffect is RegenerationEffect re)
        {
            effectType = EffectType.RegenerationEffect;
            this.healthChange = re.Regeneration;
        }
        else if (baseEffect is SpeedEffect spe)
        {
            effectType = EffectType.SpeedEffect;
            this.powerModifier = spe.SpeedModifier;
        }
        else if (baseEffect is StrengthEffect ste)
        {
            effectType = EffectType.StrengthEffect;
            this.powerModifier = ste.StrengthModifier;
        }
    }
}

[System.Serializable]
public enum EffectType
{
    FrostEffect,
    PoisoningEffect,
    IncinerationEffect,
    RegenerationEffect,
    SpeedEffect,
    StrengthEffect
}
