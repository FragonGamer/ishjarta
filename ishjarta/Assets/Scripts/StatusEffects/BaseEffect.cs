using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    [field: SerializeField] public float Duration { get; protected set; } = 0f;
    [field: SerializeField] public float DurationRemaining { get; protected set; } = 0f;

    [field: SerializeField] public float LastSecond { get; protected set; } = 0f;
    public bool IsActive => IsPermanent || DurationRemaining > 0f;

    public bool HasOneTimeEffect { get; protected set; }
    public bool HasEffectOverTime { get; protected set; }
    public bool IsPermanent { get; protected set; }

    protected void Awake()
    {
        DurationRemaining = Duration;
        LastSecond = Duration;
    }

    protected bool TickEffect()
    {
        if (IsActive)
            DurationRemaining -= Time.deltaTime;

        if (LastSecond - DurationRemaining >= 1)
        {
            LastSecond--;
            Debug.Log(LastSecond);
            if (IsPermanent && LastSecond == 0)
            {
                DurationRemaining = Duration;
                LastSecond = Duration;
            }
            return true;
        }
        else
            return false;
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
}
