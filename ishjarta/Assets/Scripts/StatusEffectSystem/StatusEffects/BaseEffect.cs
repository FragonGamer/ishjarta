using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base from which all other effects inherit
/// </summary>
public abstract class BaseEffect : ScriptableObject
{
    /// <summary>
    /// This field specifies how long the effect lasts
    /// </summary>
    [field: SerializeField] public float Duration { get; protected set; } = 0f;

    /// <summary>
    /// This field specifies how long the effect remains
    /// </summary>
    public float DurationRemaining { get; protected set; }

    //[field: SerializeField] public float LastSecond { get; protected set; } = 0f;

    /// <summary>
    /// This field specifies if the effect is active
    /// </summary>
    public bool IsActive => IsPermanent || DurationRemaining > 0f;

    /// <summary>
    /// This field specifies if the effect is an one-time effect
    /// </summary>
    public bool HasOneTimeEffect { get; protected set; }

    /// <summary>
    /// This field specifies if the effect has an influence over time
    /// </summary>
    public bool HasEffectOverTime { get; protected set; }

    /// <summary>
    /// This field specifies if the effect is permanent
    /// </summary>
    [field: SerializeField] public bool IsPermanent { get; protected set; } = false;

    protected void Awake()
    {
        DurationRemaining = Duration;
        //LastSecond = Duration;
    }

    /// <summary>
    /// Should be executed every second and reduces DurationRemaining for 1 second
    /// </summary>
    /// <returns></returns>
    protected bool TickPerSecondEffect()
    {
        if (IsActive && !IsPermanent)
            DurationRemaining -= 1;
        Debug.Log(DurationRemaining);

        //if (LastSecond - DurationRemaining >= 1)
        //{
        //    LastSecond--;
        //    Debug.Log(LastSecond);
        //    if (IsPermanent && LastSecond == 0)
        //    {
        //        DurationRemaining = Duration;
        //        LastSecond = Duration;
        //    }
        //    return true;
        //}
        //else
        //    return false;
        return true;
    }

    /// <summary>
    /// Virtual Method to override for FrostEffect, SpeedEffect and StrengthEffect
    /// </summary>
    /// <returns></returns>
    public virtual float Effect()
    {
        return 0.0f;
    }

    /// <summary>
    /// Virtual Method to override for PoisoningEffect and RegenerationEffect
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public virtual int Effect(int i)
    {
        return i;
    }

    /// <summary>
    /// Virtual Method to override for the IncinerationEffect
    /// </summary>
    /// <param name="f"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public virtual float Effect(float f, ref int h)
    {
        h = (int)f;
        return f;
    }

    /// <summary>
    /// Creates a copy of the given effect
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public static BaseEffect ReturnCopy(BaseEffect resource)
    {
        if (resource == null)
            return null;

        if (resource is FrostEffect fe)
        {
            if(fe.IsPermanent)
                return FrostEffect.CreateInstance(true, fe.SpeedDelay);
            else
                return FrostEffect.CreateInstance(fe.Duration, fe.SpeedDelay);
        }
        else if (resource is PoisoningEffect pe)
        {
            if(pe.IsPermanent)
                return PoisoningEffect.CreateInstance(true, pe.PoisonDamage);
            else
                return PoisoningEffect.CreateInstance(pe.Duration, pe.PoisonDamage);
        }
        else if (resource is IncinerationEffect ie)
        {
            if(ie.IsPermanent)
                return IncinerationEffect.CreateInstance(true, ie.ResistanceReduction, ie.IncinerationDamage);
            else
                return IncinerationEffect.CreateInstance(ie.Duration, ie.ResistanceReduction, ie.IncinerationDamage);
        }
        else if (resource is RegenerationEffect re)
        {
            if(re.IsPermanent)
                return RegenerationEffect.CreateInstance(true, re.Regeneration);
            else
                return RegenerationEffect.CreateInstance(re.Duration, re.Regeneration);
        }
        else if (resource is SpeedEffect spe)
        {
            if(spe.IsPermanent)
                return SpeedEffect.CreateInstance(true, spe.SpeedModifier);
            else
                return SpeedEffect.CreateInstance(spe.Duration, spe.SpeedModifier);
        }
        else if (resource is StrengthEffect ste)
        {
            if(ste.IsPermanent)
                return StrengthEffect.CreateInstance(true, ste.StrengthModifier);
            else
                return StrengthEffect.CreateInstance(ste.Duration, ste.StrengthModifier);
        }

        return null;
    }
}
