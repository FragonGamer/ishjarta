using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisiningEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    public int PoisonDamage { get; protected set; }

    public override int Effect(int health)
    {
        if(TickEffect())
            return health - PoisonDamage;
        return health;
    }

    private void Init(float duration, int poisonDamage)
    {
        Duration = duration;
        DurationRemaining = LastSecond = Duration;
        PoisonDamage = poisonDamage;
    }

    public static PoisiningEffect CreateInstance(float duration, int poisonDamage)
    {
        var effect = ScriptableObject.CreateInstance<PoisiningEffect>();
        effect.Init(duration, poisonDamage);
        return effect;
    }
}
