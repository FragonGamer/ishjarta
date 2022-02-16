using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    [field: SerializeField] public float Duration { get; protected set; } = 0f;
    [field: SerializeField] public float DurationRemaining { get; protected set; } = 0f;

    //[field: SerializeField] public float LastSecond { get; protected set; } = 0f;
    public bool IsActive => IsPermanent || DurationRemaining > 0f;

    public bool HasOneTimeEffect { get; protected set; }
    public bool HasEffectOverTime { get; protected set; }
    public bool IsPermanent { get; protected set; }

    protected void Awake()
    {
        DurationRemaining = Duration;
        //LastSecond = Duration;
    }

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

    // Virtual Method to override for the FrostEffect, SpeedEffect and StrengthEffect
    public virtual float Effect()
    {
        return 0.0f;
    }

    // Virtual Method to override for the PoisoningEffect and RegenerationEffect
    public virtual int Effect(int i)
    {
        return i;
    }

    // Virtual Method to override for the IncinerationEffect
    public virtual float Effect(float f, ref int h)
    {
        h = (int)f;
        return f;
    }

    public static BaseEffect ReturnCopy(BaseEffect resource)
    {
        if (resource == null)
            return null;

        if (resource is FrostEffect fe)
        {
            return FrostEffect.CreateInstance(fe.Duration, fe.SpeedDelay);
        }
        else if (resource is PoisoningEffect pe)
        {
            return PoisoningEffect.CreateInstance(pe.Duration, pe.PoisonDamage);
        }
        else if (resource is IncinerationEffect ie)
        {
            return IncinerationEffect.CreateInstance(ie.Duration, ie.ResistanceReduction, ie.IncinerationDamage);
        }
        else if (resource is RegenerationEffect re)
        {
            return RegenerationEffect.CreateInstance(re.Duration, re.Regeneration);
        }
        else if (resource is SpeedEffect spe)
        {
            return SpeedEffect.CreateInstance(spe.Duration, spe.SpeedModifier);
        }
        else if (resource is StrengthEffect ste)
        {
            return StrengthEffect.CreateInstance(ste.Duration, ste.StrengthModifier);
        }

        return null;
    }
}
