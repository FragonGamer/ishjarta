using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IncinerationStatus contains the short-lasting and the permanent IncinerationEffects
/// </summary>
public class IncinerationStatus
{
    /// <summary>
    /// This field specifies the short-lasting IncinerationEffect
    /// </summary>
    public IncinerationEffect Incineration { get; set; }
    /// <summary>
    /// This field specifies the permanent IncinerationEffects
    /// </summary>
    public List<IncinerationEffect> PermanentIncineration { get; set; } = new List<IncinerationEffect>();
}
