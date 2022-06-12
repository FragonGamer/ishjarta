using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class RangedWeapon : Weapon , ISaveable
{

    [field: SerializeField] public int ProjectileVelocity { get; set; }

    [field: SerializeField] public RangedWeaponType WeaponType { get; set; }

    public enum RangedWeaponType
    {
        bow,
        crossbow
    }

    #region SaveSystem
    public override object SaveState()
    {
        return new SaveData()
        {
            ItemName = this.ItemName,
            ItemType = (int)this.WeaponType
        };
    }

    public override void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.ItemName = saveData.ItemName;
        this.WeaponType = (RangedWeaponType)saveData.ItemType;
    }
    #endregion
}
