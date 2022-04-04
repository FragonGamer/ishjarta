using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ItemData
{
    public int damage;

    public float range;

    public float attackRate;

    public int weaponType;

    public WeaponData(Weapon weapon) : base(weapon)
    {
        damage = weapon.Damage;
        range = weapon.Range;
        attackRate = weapon.AttackRate;
    }
}
