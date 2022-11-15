using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FrostStatus contains the short-lasting and the permanent FrostEffects
/// </summary>
public class FrostStatus
{
    /// <summary>
    /// This field specifies the short-lasting FrostEffect
    /// </summary>
    public FrostEffect Frost { get; set; }
    /// <summary>
    /// This field specifies the permanent FrostEffects
    /// </summary>
    public List<FrostEffect> PermanentFrost { get; set; } = new List<FrostEffect>();
}
