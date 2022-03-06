using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationStatus
{
    public RegenerationEffect Regeneration { get; set; }
    public List<RegenerationEffect> PermanentRegeneration { get; set; } = new List<RegenerationEffect>();

}
