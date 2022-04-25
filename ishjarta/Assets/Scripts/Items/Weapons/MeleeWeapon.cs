using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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
    private bool isMeleeWeaponInitialized = false;
    public void Init(MeleeWeaponData meleeWeaponData)
    {
        if (!isMeleeWeaponInitialized)
        {
            isMeleeWeaponInitialized = true;
            base.Init(meleeWeaponData);

            WeaponType = (MeleeWeaponType)meleeWeaponData.weaponType;
        }
    }
    #endregion SaveSystem
}


