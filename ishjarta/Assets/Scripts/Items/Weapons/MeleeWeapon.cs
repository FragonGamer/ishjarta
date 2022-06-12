using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class MeleeWeapon : Weapon , ISaveable
{


    [field: SerializeField] public float Width { get; set; }

    [field: SerializeField] public MeleeWeaponType WeaponType { get; set; }

    public enum MeleeWeaponType
    {
        sword,
        redSword,
        axe
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
        this.WeaponType = (MeleeWeaponType)saveData.ItemType;
    }
    #endregion
}


