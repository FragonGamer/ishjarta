using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeaponData : WeaponData
{
    public float width;
    public MeleeWeaponData(MeleeWeapon meleeWeapon) : base(meleeWeapon)
    {
        width = meleeWeapon.Width;
        weaponType = (int)meleeWeapon.WeaponType;
    }

    public MeleeWeaponData(MeleeWeapon meleeWeapon, Vector2 position) : this(meleeWeapon)
    {
        this.position = position;
    }
}
