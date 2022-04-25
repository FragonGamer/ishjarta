using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ItemData
{
    public int weaponType;

    public WeaponData(Weapon weapon) : base(weapon)
    {
    }
}
