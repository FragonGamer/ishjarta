using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PoisoningStatus contains the short-lasting and the permanent PoisoningEffects
/// </summary>
public class PoisoningStatus
{
    /// <summary>
    /// This field specifies the short-lasting PoisoningEffect
    /// </summary>
    public PoisoningEffect Poisoning { get; set; }
    /// <summary>
    /// This field specifies the permanent PoisoningEffects
    /// </summary>
    public List<PoisoningEffect> PermanentPoisoning { get; set; } = new List<PoisoningEffect>();

}
