using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RegenerationStatus contains the short-lasting and the permanent RegenerationEffects
/// </summary>
public class RegenerationStatus
{
    /// <summary>
    /// This field specifies the short-lasting RegenerationEffect
    /// </summary>
    public RegenerationEffect Regeneration { get; set; }
    /// <summary>
    /// This field specifies the permanent RegenerationEffects
    /// </summary>
    public List<RegenerationEffect> PermanentRegeneration { get; set; } = new List<RegenerationEffect>();

}
