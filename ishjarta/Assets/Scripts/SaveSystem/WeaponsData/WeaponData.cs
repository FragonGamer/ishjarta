using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the state-storage for Weapon
/// </summary>
[System.Serializable]
public class WeaponData : ItemData
{
    public int weaponType;

    public WeaponData(Weapon weapon) : base(weapon)
    {
    }
}
