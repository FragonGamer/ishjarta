using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusEffectHandler : ScriptableObject
{
    public FrostStatus FrostStat { get; private set; } = new FrostStatus();
    public PoisoningStatus PoisoningStat { get; private set; } = new PoisoningStatus();
    public IncinerationStatus IncinerationStat { get; private set; } = new IncinerationStatus();

    public RegenerationStatus RegenerationStat { get; private set; } = new RegenerationStatus();
    public SpeedStatus SpeedStat { get; private set; } = new SpeedStatus();
    public StrengthStatus StrengthStat { get; private set; } = new StrengthStatus();

    public void AddEffect(BaseEffect effect)
    {
        if (effect != null)
            effect = BaseEffect.ReturnCopy(effect);
        else
            return;

        if (effect is FrostEffect fe)
        {
            if(fe.IsPermanent)
            {
                FrostStat.PermanentFrost.Add(fe);
            }
            else
            {
                var frostEffect = FrostStat.Frost;
                if(frostEffect != null)
                {
                    float speedDelay = fe.SpeedDelay >= frostEffect.SpeedDelay ? fe.SpeedDelay : frostEffect.SpeedDelay;
                    float duration = fe.Duration >= frostEffect.Duration ? fe.Duration : frostEffect.Duration;

                    fe = FrostEffect.CreateInstance(duration, speedDelay);
                }

                FrostStat.Frost = fe;
            }

        }
        else if (effect is PoisoningEffect pe)
        {
            if(pe.IsPermanent)
            {
                PoisoningStat.PermanentPoisoning.Add(pe);
            }
            else
            {
                var poisiningEffect = PoisoningStat.Poisoning;
                if(poisiningEffect != null)
                {
                    int poisonDamage = pe.PoisonDamage >= poisiningEffect.PoisonDamage ? pe.PoisonDamage : poisiningEffect.PoisonDamage;
                    float duration = pe.Duration >= poisiningEffect.Duration ? pe.Duration : poisiningEffect.Duration;

                    pe = PoisoningEffect.CreateInstance(duration, poisonDamage);
                }
                PoisoningStat.Poisoning = pe;
            }
        }
        else if (effect is IncinerationEffect ie)
        {
            if(ie.IsPermanent)
            {
                IncinerationStat.PermanentIncineration.Add(ie);
            }
            else
            {
                var incinerationEffect = IncinerationStat.Incineration;
                if(incinerationEffect != null)
                {
                    float resistanceReduction = ie.ResistanceReduction >= incinerationEffect.ResistanceReduction
                                        ? ie.ResistanceReduction : incinerationEffect.ResistanceReduction;
                    float duration = ie.Duration >= incinerationEffect.Duration ? ie.Duration : incinerationEffect.Duration;

                    ie = IncinerationEffect.CreateInstance(duration, resistanceReduction, ie.IncinerationDamage);
                }
                IncinerationStat.Incineration = ie;
            }
        }
        else if (effect is RegenerationEffect re)
        {
            if(re.IsPermanent)
            {
                RegenerationStat.PermanentRegeneration.Add(re);
            }
            else
            {
                var regenerationEffect = RegenerationStat.Regeneration;
                if(regenerationEffect != null)
                {
                    int regeneration = re.Regeneration >= regenerationEffect.Regeneration
                        ? re.Regeneration : regenerationEffect.Regeneration;
                    float duration = re.Duration >= regenerationEffect.Duration 
                        ? re.Duration : regenerationEffect.Duration;

                    re = RegenerationEffect.CreateInstance(duration, regeneration);
                }
                RegenerationStat.Regeneration = re;
            }
        }
        else if (effect is SpeedEffect spe)
        {
            if(spe.IsPermanent)
            {
                Debug.Log("MOIN");
                SpeedStat.PermanentSpeed.Add(spe);
            }
            else
            {
                var speedEffect = SpeedStat.Speed;
                if(speedEffect != null)
                {
                    float speedModifier = spe.SpeedModifier >= speedEffect.SpeedModifier
                        ? spe.SpeedModifier : speedEffect.SpeedModifier;
                    float duration = spe.Duration >= speedEffect.Duration ? spe.Duration : speedEffect.Duration;

                    spe = SpeedEffect.CreateInstance(duration, speedModifier);
                }
                SpeedStat.Speed = spe;
            }
        }
        else if (effect is StrengthEffect ste)
        {
            if(ste.IsPermanent)
            {
                StrengthStat.PermanentStrength.Add(ste);
            }
            else
            {
                var strengthEffect = StrengthStat.Strength;
                if(strengthEffect != null)
                {
                    float strengthModifier = ste.StrengthModifier >= strengthEffect.StrengthModifier
                    ? ste.StrengthModifier : strengthEffect.StrengthModifier;
                    float duration = ste.Duration >= strengthEffect.Duration ? ste.Duration : strengthEffect.Duration;

                    ste = StrengthEffect.CreateInstance(duration, strengthModifier);
                }
                StrengthStat.Strength = ste;
            }
        }
        //if (effect is FrostEffect fe)
        //{
        //    Frost.Add(fe);
        //}
        //else if (effect is PoisoningEffect pe)
        //{
        //    Poisining.Add(pe);
        //}
        //else if (effect is IncinerationEffect ie)
        //{
        //    Incineration.Add(ie);
        //}
        //else if (effect is RegenerationEffect re)
        //{
        //    Regeneration.Add(re);
        //}
        //else if (effect is SpeedEffect spe)
        //{
        //    Speed.Add(spe);
        //}
        //else if (effect is StrengthEffect ste)
        //{
        //    Strength.Add(ste);
        //}

        //if(effect is FrostEffect fe)
        //{
        //    if(Frost != null)
        //    {
        //        if (!Frost.IsPermanent && !fe.IsPermanent)
        //        {
        //            float speedDelay = fe.SpeedDelay >= Frost.SpeedDelay ? fe.SpeedDelay : Frost.SpeedDelay;
        //            float duration = fe.Duration >= Frost.Duration ? fe.Duration : Frost.Duration;

        //            fe = FrostEffect.CreateInstance(duration, speedDelay);
        //        }
        //        else if(Frost.IsPermanent && !fe.IsPermanent)
        //        {
        //            fe = Frost;
        //        }
        //        else if(Frost.IsPermanent && fe.IsPermanent)
        //        {
        //            fe = FrostEffect.CreateInstance(true, Frost.SpeedDelay * fe.SpeedDelay);
        //        }
        //    }

        //    Frost = fe;
        //} 
        //else if(effect is PoisoningEffect pe) 
        //{
        //    if (Poisining != null)
        //    {
        //        if(!Poisining.IsPermanent && !pe.IsPermanent)
        //        {
        //            int poisonDamage = pe.PoisonDamage >= Poisining.PoisonDamage ? pe.PoisonDamage : Poisining.PoisonDamage;
        //            float duration = pe.Duration >= Poisining.Duration ? pe.Duration : Poisining.Duration;

        //            pe = PoisoningEffect.CreateInstance(duration, poisonDamage);
        //        }
        //        else if(Poisining.IsPermanent && !pe.IsPermanent)
        //        {
        //            pe = Poisining;
        //        }
        //        else if(Poisining.IsPermanent && pe.IsPermanent)
        //        {
        //            pe = PoisoningEffect.CreateInstance(true, Poisining.PoisonDamage + pe.PoisonDamage);
        //        }
        //    }

        //    Poisining = pe;
        //}
        //else if(effect is IncinerationEffect ie)
        //{
        //    if (Incineration != null)
        //    {
        //        if(!Incineration.IsPermanent && !ie.IsPermanent)
        //        {
        //            float resistanceReduction = ie.ResistanceReduction >= Incineration.ResistanceReduction
        //            ? ie.ResistanceReduction : Incineration.ResistanceReduction;
        //            float duration = ie.Duration >= Incineration.Duration ? ie.Duration : Incineration.Duration;

        //            ie = IncinerationEffect.CreateInstance(duration, resistanceReduction, ie.IncinerationDamage);
        //        }
        //        else if(Incineration.IsPermanent && !ie.IsPermanent)
        //        {
        //            ie = Incineration;
        //        }
        //        else if(Incineration.IsPermanent && ie.IsPermanent)
        //        {
        //            ie = IncinerationEffect.CreateInstance(true, Incineration.ResistanceReduction + ie.ResistanceReduction,
        //                ie.IncinerationDamage);
        //        }
        //    }

        //    Incineration = ie;
        //}
        //else if(effect is RegenerationEffect re)
        //{
        //    if (Regeneration != null)
        //    {
        //        if(!Regeneration.IsPermanent && !re.IsPermanent)
        //        {
        //            int regeneration = re.Regeneration >= Regeneration.Regeneration
        //            ? re.Regeneration : Regeneration.Regeneration;
        //            float duration = re.Duration >= Regeneration.Duration ? re.Duration : Regeneration.Duration;

        //            re = RegenerationEffect.CreateInstance(duration, regeneration);
        //        }
        //        else if(Regeneration.IsPermanent && !re.IsPermanent)
        //        {
        //            re = Regeneration;
        //        }
        //        else if(Regeneration.IsPermanent && re.IsPermanent)
        //        {
        //            re = RegenerationEffect.CreateInstance(true, Regeneration.Regeneration + re.Regeneration);
        //        }
        //    }

        //    Regeneration = re;
        //}
        //else if (effect is SpeedEffect spe)
        //{
        //    if (Speed != null)
        //    {
        //        if(!Speed.IsPermanent && !spe.IsPermanent)
        //        {
        //            float speedModifier = spe.SpeedModifier >= Speed.SpeedModifier
        //            ? spe.SpeedModifier : Speed.SpeedModifier;
        //            float duration = spe.Duration >= Speed.Duration ? spe.Duration : Speed.Duration;

        //            spe = SpeedEffect.CreateInstance(duration, speedModifier);
        //        }
        //        else if(Speed.IsPermanent && !spe.IsPermanent)
        //        {
        //            spe = Speed;
        //        }
        //        else if(Speed.IsPermanent && spe.IsPermanent)
        //        {
        //            spe = SpeedEffect.CreateInstance(true, Speed.SpeedModifier * spe.SpeedModifier);
        //        }
        //    }
        //    Speed = spe;
        //}
        //else if (effect is StrengthEffect ste)
        //{
        //    if (Strength != null)
        //    {
        //        if(!Strength.IsPermanent && !ste.IsPermanent)
        //        {
        //            float strengthModifier = ste.StrengthModifier >= Strength.StrengthModifier
        //            ? ste.StrengthModifier : Strength.StrengthModifier;
        //            float duration = ste.Duration >= Strength.Duration ? ste.Duration : Strength.Duration;

        //            ste = StrengthEffect.CreateInstance(duration, strengthModifier);
        //        }
        //        else if(Strength.IsPermanent && !ste.IsPermanent)
        //        {
        //            ste = Strength;
        //        }
        //        else if(Strength.IsPermanent && ste.IsPermanent)
        //        {
        //            ste = StrengthEffect.CreateInstance(true, Strength.StrengthModifier * ste.StrengthModifier);
        //        }
        //    }

        //    Strength = ste;
        //}
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
        FrostStat.Frost = null;
    }
    public void RemovePoisining()
    {
        PoisoningStat.Poisoning = null;
    }

    public void RemoveIncineration()
    {
        IncinerationStat.Incineration = null;
    }
    public void RemoveRegeneration()
    {
        RegenerationStat.Regeneration = null;
    }
    public void RemoveSpeed()
    {
        SpeedStat.Speed = null;
    }
    public void RemoveStrengh()
    {
        StrengthStat.Strength = null;
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
        //if (effect != null)
        //{
        //    if (effect is FrostEffect fe)
        //    {
        //        if(fe.IsPermanent)
        //        {
        //            float speedDelay = Frost.SpeedDelay / fe.SpeedDelay;
        //            if(speedDelay == 1)
        //            {
        //                Frost = null;
        //            }
        //            else
        //            {
        //                Frost = FrostEffect.CreateInstance(true, speedDelay);
        //            }
        //        }
        //        else
        //        {
        //            Frost = null;
        //        }
        //    }
        //    else if (effect is PoisoningEffect pe)
        //    {
        //        if(pe.IsPermanent)
        //        {
        //            int poisonDamage = Math.Abs(Poisining.PoisonDamage - pe.PoisonDamage);
        //            if(poisonDamage == 0)
        //            {
        //                Poisining = null;
        //            }
        //            else
        //            {
        //                Poisining = PoisoningEffect.CreateInstance(true, poisonDamage);
        //            }
        //        }
        //        else
        //        {
        //            Poisining = null;
        //        }
        //    }
        //    else if (effect is IncinerationEffect ie)
        //    {
        //        if(ie.IsPermanent)
        //        {
        //            float resistanceReduction = Math.Abs(Incineration.ResistanceReduction - ie.ResistanceReduction);
        //            if (resistanceReduction == 0)
        //            {
        //                Incineration = null;
        //            }
        //            else
        //            {
        //                Incineration = IncinerationEffect.CreateInstance(true, resistanceReduction, 0);
        //            }
        //        }
        //        else
        //        {
        //            Incineration = null;
        //        }
        //    }
        //    else if (effect is RegenerationEffect re)
        //    {
        //        if(re.IsPermanent)
        //        {
        //            int regeneration = Math.Abs(Regeneration.Regeneration - re.Regeneration);
        //            if (regeneration == 0)
        //            {
        //                Regeneration = null;
        //            }
        //            else
        //            {
        //                Regeneration = RegenerationEffect.CreateInstance(true, regeneration);
        //            }
        //        }
        //        else
        //        {
        //            Regeneration = null;
        //        }
        //    }
        //    else if (effect is SpeedEffect spe)
        //    {
        //        if (spe.IsPermanent)
        //        {
        //            float speedModifier = Speed.SpeedModifier / spe.SpeedModifier;
        //            if (speedModifier == 1)
        //            {
        //                Speed = null;
        //            }
        //            else
        //            {
        //                Speed = SpeedEffect.CreateInstance(true, speedModifier);
        //            }
        //        }
        //        else
        //        {
        //            Speed = null;
        //        }
        //    }
        //    else if (effect is StrengthEffect ste)
        //    {
        //        if(ste.IsPermanent)
        //        {
        //            float strengthModifier = Strength.StrengthModifier / ste.StrengthModifier;
        //            if(strengthModifier == 1)
        //            {
        //                Strength = null;
        //            }
        //            else
        //            {
        //                Strength = StrengthEffect.CreateInstance(true, strengthModifier);
        //            }
        //        }
        //        else
        //        {
        //            Strength = null;
        //        }
        //    }
        //}
        if (effect != null)
        {
            if (effect is FrostEffect fe)
            {
                if (fe.IsPermanent)
                {
                    FrostStat.PermanentFrost
                        .Remove(FrostStat.PermanentFrost.Where(x => x.SpeedDelay == fe.SpeedDelay)
                        .FirstOrDefault());
                }
                else
                    RemoveFrost();
            }
            else if (effect is PoisoningEffect pe)
            {
                if (pe.IsPermanent)
                {
                    PoisoningStat.PermanentPoisoning
                        .Remove(PoisoningStat.PermanentPoisoning.Where(x => x.PoisonDamage == pe.PoisonDamage)
                        .FirstOrDefault());
                }
                else
                    RemovePoisining();
            }
            else if (effect is IncinerationEffect ie)
            {
                if (ie.IsPermanent)
                {
                    IncinerationStat.PermanentIncineration
                        .Remove(IncinerationStat.PermanentIncineration.Where(x => x.ResistanceReduction == ie.ResistanceReduction 
                        && x.IncinerationDamage == ie.IncinerationDamage)
                        .FirstOrDefault());
                }
                else
                    RemoveIncineration();
            }
            else if (effect is RegenerationEffect re)
            {
                if (re.IsPermanent)
                {
                    RegenerationStat.PermanentRegeneration
                        .Remove(RegenerationStat.PermanentRegeneration.Where(x => x.Regeneration == re.Regeneration)
                    .FirstOrDefault());
                }
                else
                    RemoveRegeneration();
            }
            else if (effect is SpeedEffect spe)
            {
                if (spe.IsPermanent)
                {
                    SpeedStat.PermanentSpeed
                        .Remove(SpeedStat.PermanentSpeed.Where(x => x.SpeedModifier == spe.SpeedModifier)
                        .FirstOrDefault());
                }
                else
                    RemoveSpeed();
            }
            else if (effect is StrengthEffect ste)
            {
                if (ste.IsPermanent)
                {
                    StrengthStat.PermanentStrength
                        .Remove(StrengthStat.PermanentStrength.Where(x => x.StrengthModifier == ste.StrengthModifier)
                        .FirstOrDefault());
                }
                else
                    RemoveStrengh();
            }
        }
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
