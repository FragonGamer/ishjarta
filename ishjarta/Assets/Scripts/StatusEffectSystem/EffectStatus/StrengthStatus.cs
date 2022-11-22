using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StrengthStatus contains the short-lasting and the permanent StrengthEffects
/// </summary>
public class StrengthStatus
{
    /// <summary>
    /// This field specifies the short-lasting StrengthEffect
    /// </summary>
    public StrengthEffect Strength { get; set; }
    /// <summary>
    /// This field specifies the permanent StrengthEffects
    /// </summary>
    public List<StrengthEffect> PermanentStrength { get; set; } = new List<StrengthEffect>();

}
