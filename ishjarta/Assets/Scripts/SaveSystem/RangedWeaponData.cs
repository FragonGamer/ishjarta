using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedWeaponData : WeaponData
{
    public int projectileVelocity;

    public RangedWeaponData(RangedWeapon weapon) : base(weapon)
    {
        projectileVelocity = weapon.ProjectileVelocity;
        weaponType = (int)weapon.WeaponType;
    }

    public RangedWeaponData(RangedWeapon weapon, Vector2 position) : this(weapon)
    {
        this.position = position;
    }
}
