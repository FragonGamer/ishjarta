using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostStatus
{
    public FrostEffect Frost { get; set; }
    public List<FrostEffect> PermanentFrost { get; set; } = new List<FrostEffect>();
}
