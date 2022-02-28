using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : ScriptableObject
{
    public FrostEffect Frost { get; private set; }
    public PoisoningEffect Poisining { get; private set; }
    public IncinerationEffect Incineration { get; private set; }

    public RegenerationEffect Regeneration { get; private set; }
    public SpeedEffect Speed { get; private set; }
    public StrengthEffect Strength { get; private set; }

    public void AddEffect(BaseEffect effect)
    {
        if(effect is FrostEffect fe)
        {
            if(Frost != null)
            {
                if (!Frost.IsPermanent && !fe.IsPermanent)
                {
                    float speedDelay = fe.SpeedDelay >= Frost.SpeedDelay ? fe.SpeedDelay : Frost.SpeedDelay;
                    float duration = fe.Duration >= Frost.Duration ? fe.Duration : Frost.Duration;

                    fe = FrostEffect.CreateInstance(duration, speedDelay);
                }
                else if(Frost.IsPermanent && !fe.IsPermanent)
                {
                    fe = Frost;
                }
                else if(Frost.IsPermanent && fe.IsPermanent)
                {
                    fe = FrostEffect.CreateInstance(true, Frost.SpeedDelay * fe.SpeedDelay);
                }
            }

            Frost = fe;
        } 
        else if(effect is PoisoningEffect pe) 
        {
            if (Poisining != null)
            {
                if(!Poisining.IsPermanent && !pe.IsPermanent)
                {
                    int poisonDamage = pe.PoisonDamage >= Poisining.PoisonDamage ? pe.PoisonDamage : Poisining.PoisonDamage;
                    float duration = pe.Duration >= Poisining.Duration ? pe.Duration : Poisining.Duration;

                    pe = PoisoningEffect.CreateInstance(duration, poisonDamage);
                }
                else if(Poisining.IsPermanent && !pe.IsPermanent)
                {
                    pe = Poisining;
                }
                else if(Poisining.IsPermanent && pe.IsPermanent)
                {
                    pe = PoisoningEffect.CreateInstance(true, Poisining.PoisonDamage + pe.PoisonDamage);
                }
            }

            Poisining = pe;
        }
        else if(effect is IncinerationEffect ie)
        {
            if (Incineration != null)
            {
                if(!Incineration.IsPermanent && !ie.IsPermanent)
                {
                    float resistanceReduction = ie.ResistanceReduction >= Incineration.ResistanceReduction
                    ? ie.ResistanceReduction : Incineration.ResistanceReduction;
                    float duration = ie.Duration >= Incineration.Duration ? ie.Duration : Incineration.Duration;

                    ie = IncinerationEffect.CreateInstance(duration, resistanceReduction, ie.IncinerationDamage);
                }
                else if(Incineration.IsPermanent && !ie.IsPermanent)
                {
                    ie = Incineration;
                }
                else if(Incineration.IsPermanent && ie.IsPermanent)
                {
                    ie = IncinerationEffect.CreateInstance(true, Incineration.ResistanceReduction + ie.ResistanceReduction,
                        ie.IncinerationDamage);
                }
            }

            Incineration = ie;
        }
        else if(effect is RegenerationEffect re)
        {
            if (Regeneration != null)
            {
                if(!Regeneration.IsPermanent && !re.IsPermanent)
                {
                    int regeneration = re.Regeneration >= Regeneration.Regeneration
                    ? re.Regeneration : Regeneration.Regeneration;
                    float duration = re.Duration >= Regeneration.Duration ? re.Duration : Regeneration.Duration;

                    re = RegenerationEffect.CreateInstance(duration, regeneration);
                }
                else if(Regeneration.IsPermanent && !re.IsPermanent)
                {
                    re = Regeneration;
                }
                else if(Regeneration.IsPermanent && re.IsPermanent)
                {
                    re = RegenerationEffect.CreateInstance(true, Regeneration.Regeneration + re.Regeneration);
                }
            }

            Regeneration = re;
        }
        else if (effect is SpeedEffect spe)
        {
            if (Speed != null)
            {
                if(!Speed.IsPermanent && !spe.IsPermanent)
                {
                    float speedModifier = spe.SpeedModifier >= Speed.SpeedModifier
                    ? spe.SpeedModifier : Speed.SpeedModifier;
                    float duration = spe.Duration >= Speed.Duration ? spe.Duration : Speed.Duration;

                    spe = SpeedEffect.CreateInstance(duration, speedModifier);
                }
                else if(Speed.IsPermanent && !spe.IsPermanent)
                {
                    spe = Speed;
                }
                else if(Speed.IsPermanent && spe.IsPermanent)
                {
                    spe = SpeedEffect.CreateInstance(true, Speed.SpeedModifier * spe.SpeedModifier);
                }
            }
            Speed = spe;
        }
        else if (effect is StrengthEffect ste)
        {
            if (Strength != null)
            {
                if(!Strength.IsPermanent && !ste.IsPermanent)
                {
                    float strengthModifier = ste.StrengthModifier >= Strength.StrengthModifier
                    ? ste.StrengthModifier : Strength.StrengthModifier;
                    float duration = ste.Duration >= Strength.Duration ? ste.Duration : Strength.Duration;

                    ste = StrengthEffect.CreateInstance(duration, strengthModifier);
                }
                else if(Strength.IsPermanent && !ste.IsPermanent)
                {
                    ste = Strength;
                }
                else if(Strength.IsPermanent && ste.IsPermanent)
                {
                    ste = StrengthEffect.CreateInstance(true, Strength.StrengthModifier * ste.StrengthModifier);
                }
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

    //private bool IsEffectAlreadyPermanent(BaseEffect effect)
    //{
    //    bool result = false;
    //    if (effect is FrostEffect && Frost != null)
    //    {
    //        result = Frost.IsPermanent;
    //    }
    //    else if (effect is PoisoningEffect && Poisining != null)
    //    {
    //        result = Poisining.IsPermanent;
    //    }
    //    else if (effect is IncinerationEffect && Incineration != null)
    //    {
    //        result = Incineration.IsPermanent;
    //    }
    //    else if (effect is RegenerationEffect && Regeneration != null)
    //    {
    //        result = Regeneration.IsPermanent;
    //    }
    //    else if (effect is SpeedEffect && Speed != null)
    //    {
    //        result = Speed.IsPermanent;
    //    }
    //    else if (effect is StrengthEffect && Strength != null)
    //    {
    //        result = Strength.IsPermanent;
    //    }
    //    return result;
    //}

    public void RemoveEffect(BaseEffect effect)
    {
        if (effect != null)
        {
            if (effect is FrostEffect fe)
            {
                if(fe.IsPermanent)
                {
                    float speedDelay = Frost.SpeedDelay / fe.SpeedDelay;
                    if(speedDelay == 1)
                    {
                        Frost = null;
                    }
                    else
                    {
                        Frost = FrostEffect.CreateInstance(true, speedDelay);
                    }
                }
                else
                {
                    Frost = null;
                }
            }
            else if (effect is PoisoningEffect pe)
            {
                if(pe.IsPermanent)
                {
                    int poisonDamage = Math.Abs(Poisining.PoisonDamage - pe.PoisonDamage);
                    if(poisonDamage == 0)
                    {
                        Poisining = null;
                    }
                    else
                    {
                        Poisining = PoisoningEffect.CreateInstance(true, poisonDamage);
                    }
                }
                else
                {
                    Poisining = null;
                }
            }
            else if (effect is IncinerationEffect ie)
            {
                if(ie.IsPermanent)
                {
                    float resistanceReduction = Math.Abs(Incineration.ResistanceReduction - ie.ResistanceReduction);
                    if (resistanceReduction == 0)
                    {
                        Incineration = null;
                    }
                    else
                    {
                        Incineration = IncinerationEffect.CreateInstance(true, resistanceReduction, 0);
                    }
                }
                else
                {
                    Incineration = null;
                }
            }
            else if (effect is RegenerationEffect re)
            {
                if(re.IsPermanent)
                {
                    int regeneration = Math.Abs(Regeneration.Regeneration - re.Regeneration);
                    if (regeneration == 0)
                    {
                        Regeneration = null;
                    }
                    else
                    {
                        Regeneration = RegenerationEffect.CreateInstance(true, regeneration);
                    }
                }
                else
                {
                    Regeneration = null;
                }
            }
            else if (effect is SpeedEffect spe)
            {
                if (spe.IsPermanent)
                {
                    float speedModifier = Speed.SpeedModifier / spe.SpeedModifier;
                    if (speedModifier == 1)
                    {
                        Speed = null;
                    }
                    else
                    {
                        Speed = SpeedEffect.CreateInstance(true, speedModifier);
                    }
                }
                else
                {
                    Speed = null;
                }
            }
            else if (effect is StrengthEffect ste)
            {
                if(ste.IsPermanent)
                {
                    float strengthModifier = Strength.StrengthModifier / ste.StrengthModifier;
                    if(strengthModifier == 1)
                    {
                        Strength = null;
                    }
                    else
                    {
                        Strength = StrengthEffect.CreateInstance(true, strengthModifier);
                    }
                }
                else
                {
                    Strength = null;
                }
            }
        }
        //if (effect != null)
        //{
        //    if (effect == Frost)
        //    {
        //        Frost = null;
        //    }
        //    else if (effect == Poisining)
        //    {
        //        Poisining = null;
        //    }
        //    else if (effect == Incineration)
        //    {
        //        Incineration = null;
        //    }
        //    else if (effect == Regeneration)
        //    {
        //        Regeneration = null;
        //    }
        //    else if (effect == Speed)
        //    {
        //        Speed = null;
        //    }
        //    else if (effect == Strength)
        //    {
        //        Strength = null;
        //    }
        //}
    }

    public void RemoveEffectRange(BaseEffect[] effects)
    {
        if (effects != null)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                RemoveEffect(effects[i]);
            }
        }
    }
}
