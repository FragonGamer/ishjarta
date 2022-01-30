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
        if(effect is FrostEffect)
        {
            Frost = (FrostEffect)effect;
        } 
        else if(effect is PoisiningEffect) 
        { 
            Poisining = (PoisiningEffect)effect;
        }
        else if( effect is IncinerationEffect)
        {
            Incineration = (IncinerationEffect)effect;
        }
        else if(effect is RegenerationEffect)
        {
            Regeneration = (RegenerationEffect)effect;
        }
        else if (effect is SpeedEffect)
        {
            Speed = (SpeedEffect)effect;
        }
        else if (effect is StrengthEffect)
        {
            Strength = (StrengthEffect)effect;
        }
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
}
