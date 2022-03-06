using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerationStatus
{
    public IncinerationEffect Incineration { get; set; }
    public List<IncinerationEffect> PermanentIncineration { get; set; } = new List<IncinerationEffect>();
}
