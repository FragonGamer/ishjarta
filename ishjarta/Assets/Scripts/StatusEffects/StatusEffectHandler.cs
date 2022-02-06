using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : ScriptableObject
{
    public FrostEffect Frost { get; private set; }
    public PoisiningEffect Poisining { get; private set; }
    public IncinerationEffect Incineration { get; private set; }

    public RegenerationEffect Regeneration { get; private set; }
    public SpeedEffect Speed { get; private set; }
    public StrengthEffect Strength { get; private set; }

    public void AddEffect(BaseEffect effect)
    {
        if (IsEffectAlreadyPermanent(effect))
            return;

        if(effect is FrostEffect fe)
        {
            if(Frost != null)
            {
                float speedDelay = fe.SpeedDelay >= Frost.SpeedDelay ? fe.SpeedDelay : Frost.SpeedDelay;
                float duration = fe.Duration >= Frost.Duration ? fe.Duration : Frost.Duration;

                fe = FrostEffect.CreateInstance(duration, speedDelay);
            }

            Frost = fe;
        } 
        else if(effect is PoisiningEffect pe) 
        {
            if (Poisining != null)
            {
                int poisonDamage = pe.PoisonDamage >= Poisining.PoisonDamage ? pe.PoisonDamage : Poisining.PoisonDamage;
                float duration = pe.Duration >= Poisining.Duration ? pe.Duration : Poisining.Duration;

                pe = PoisiningEffect.CreateInstance(duration, poisonDamage);
            }

            Poisining = pe;
        }
        else if(effect is IncinerationEffect ie)
        {
            if (Incineration != null)
            {
                float resistanceReduction = ie.ResistanceReduction >= Incineration.ResistanceReduction 
                    ? ie.ResistanceReduction : Incineration.ResistanceReduction;
                float duration = ie.Duration >= Incineration.Duration ? ie.Duration : Incineration.Duration;

                ie = IncinerationEffect.CreateInstance(duration, resistanceReduction, ie.IncinerationDamage);
            }

            Incineration = ie;
        }
        else if(effect is RegenerationEffect re)
        {
            if (Regeneration != null)
            {
                int regeneration = re.Regeneration >= Regeneration.Regeneration
                    ? re.Regeneration : Regeneration.Regeneration;
                float duration = re.Duration >= Regeneration.Duration ? re.Duration : Regeneration.Duration;

                re = RegenerationEffect.CreateInstance(duration, regeneration);
            }

            Regeneration = re;
        }
        else if (effect is SpeedEffect spe)
        {
            if (Speed != null)
            {
                float speedModifier = spe.SpeedModifier >= Speed.SpeedModifier
                    ? spe.SpeedModifier : Speed.SpeedModifier;
                float duration = spe.Duration >= Speed.Duration ? spe.Duration : Speed.Duration;

                spe = SpeedEffect.CreateInstance(duration, speedModifier);
            }
            Speed = spe;
        }
        else if (effect is StrengthEffect ste)
        {
            if (Strength != null)
            {
                float strengthModifier = ste.StrengthModifier >= Strength.StrengthModifier
                    ? ste.StrengthModifier : Strength.StrengthModifier;
                float duration = ste.Duration >= Strength.Duration ? ste.Duration : Strength.Duration;

                ste = StrengthEffect.CreateInstance(duration, strengthModifier);
            }

            Strength = ste;
        }
    }

    public void AddEffectRange(BaseEffect[] effects)
    {
        for(int i = 0; i < effects.Length; i++)
        {
            AddEffect(effects[i]);
        }
    }

    public void RemoveFrost()
    {
        Frost = null;
    }
    public void RemovePoisining()
    {
        Poisining = null;
    }

    public void RemoveIncineration()
    {
        Incineration = null;
    }
    public void RemoveRegeneration()
    {
        Regeneration = null;
    }
    public void RemoveSpeed()
    {
        Speed = null;
    }
    public void RemoveStrengh()
    {
        Strength = null;
    }

    private bool IsEffectAlreadyPermanent(BaseEffect effect)
    {
        bool result = false;
        if (effect is FrostEffect && Frost != null)
        {
            result = Frost.IsPermanent;
        }
        else if (effect is PoisiningEffect && Poisining != null)
        {
            result = Poisining.IsPermanent;
        }
        else if (effect is IncinerationEffect && Incineration != null)
        {
            result = Incineration.IsPermanent;
        }
        else if (effect is RegenerationEffect && Regeneration != null)
        {
            result = Regeneration.IsPermanent;
        }
        else if (effect is SpeedEffect && Speed != null)
        {
            result = Speed.IsPermanent;
        }
        else if (effect is StrengthEffect && Strength != null)
        {
            result = Strength.IsPermanent;
        }
        return result;
    }

    public void RemoveEffect(BaseEffect effect)
    {
        if (effect == Frost)
        {
            Frost = null;
        }
        else if (effect == Poisining)
        {
            Poisining = null;
        }
        else if (effect == Incineration)
        {
            Incineration = null;
        }
        else if (effect == Regeneration)
        {
            Regeneration = null;
        }
        else if (effect == Speed)
        {
            Speed = null;
        }
        else if (effect == Strength)
        {
            Strength = null;
        }
    }

    public void RemoveEffectRange(BaseEffect[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            RemoveEffect(effects[i]);
        }
    }
}
