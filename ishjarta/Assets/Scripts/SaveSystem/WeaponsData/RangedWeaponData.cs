using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedWeaponData : WeaponData
{
    public RangedWeaponData(RangedWeapon weapon) : base(weapon)
    {
        weaponType = (int)weapon.WeaponType;
    }

    public RangedWeaponData(RangedWeapon weapon, Vector2 position) : this(weapon)
    {
        this.position = position;
    }
}
