using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoisoningEffect")]
public class PoisoningEffect : BaseEffect
{
    new void Awake()
    {
        HasEffectOverTime = true;
        HasOneTimeEffect = false;
    }

    [field: SerializeField] public int PoisonDamage { get; protected set; }

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
    private void Init(bool isPermanent, int poisonDamage)
    {
        IsPermanent = isPermanent;
        Duration = 1;
        DurationRemaining = LastSecond = Duration;
        PoisonDamage = poisonDamage;
    }

    public static PoisoningEffect CreateInstance(float duration, int poisonDamage)
    {
        var effect = ScriptableObject.CreateInstance<PoisoningEffect>();
        effect.Init(duration, poisonDamage);
        return effect;
    }
    public static PoisoningEffect CreateInstance(bool isPermanent, int poisonDamage)
    {
        var effect = ScriptableObject.CreateInstance<PoisoningEffect>();
        effect.Init(isPermanent, poisonDamage);
        return effect;
    }
}
