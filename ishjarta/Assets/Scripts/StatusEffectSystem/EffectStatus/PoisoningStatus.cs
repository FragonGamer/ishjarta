using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningStatus
{
    public PoisoningEffect Poisoning { get; set; }
    public List<PoisoningEffect> PermanentPoisoning { get; set; } = new List<PoisoningEffect>();

}
