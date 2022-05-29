using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
[Serializable]
public abstract class Weapon : Item
{


    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackRate { get; set; }


    [field: SerializeField] public bool IsChargable { get; set; }

}
