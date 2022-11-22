using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for MeleeWeapon
/// </summary>
[System.Serializable]
public class MeleeWeaponData : WeaponData
{
    public MeleeWeaponData(MeleeWeapon meleeWeapon) : base(meleeWeapon)
    {
        weaponType = (int)meleeWeapon.WeaponType;
    }

    public MeleeWeaponData(MeleeWeapon meleeWeapon, Vector2 position) : this(meleeWeapon)
    {
        this.position = position;
    }
}
