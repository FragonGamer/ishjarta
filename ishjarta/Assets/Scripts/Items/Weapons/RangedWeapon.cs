using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class RangedWeapon : Weapon
{

    [field: SerializeField] public int ProjectileVelocity { get; set; }

    [field: SerializeField] public RangedWeaponType WeaponType { get; set; }

    public enum RangedWeaponType
    {
        bow,
        crossbow
    }


}
