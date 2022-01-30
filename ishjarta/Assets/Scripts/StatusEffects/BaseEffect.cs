using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    [SerializeField] protected float Duration = 0f;
    public float DurationRemaining = 0f;

    public bool IsActive => DurationRemaining > 0f;

    public bool HasOneTimeEffect;
    public bool HasEffectOverTime;

    protected void Awake()
    {
        DurationRemaining = Duration;
    }

    protected void TickEffect()
    {
        if (!IsActive)
            DurationRemaining -= Time.deltaTime;
    }
}
