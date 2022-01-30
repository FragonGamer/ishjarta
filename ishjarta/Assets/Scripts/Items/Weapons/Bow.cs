using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bow : RangedWeapon
{
    private void Awake()
    {
        Range = 5f;
        AttackRate = 1f;
    }
}
