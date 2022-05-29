using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class MeleeWeapon : Weapon
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

    #endregion SaveSystem
}


