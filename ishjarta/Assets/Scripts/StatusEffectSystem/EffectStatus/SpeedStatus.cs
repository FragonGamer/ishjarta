using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpeedStatus contains the short-lasting and the permanent SpeedEffects
/// </summary>
public class SpeedStatus
{
    /// <summary>
    /// This field specifies the short-lasting SpeedEffect
    /// </summary>
    public SpeedEffect Speed { get; set; }
    /// <summary>
    /// This field specifies the permanent SpeedEffects
    /// </summary>
    public List<SpeedEffect> PermanentSpeed { get; set; } = new List<SpeedEffect>();

}
