using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bow : RangedWeapon
{
    private void Awake()
    {
        range = 5f;
    }
}
