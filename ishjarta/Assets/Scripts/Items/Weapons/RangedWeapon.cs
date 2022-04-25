using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangedWeapon : Weapon
{
    [field: SerializeField] public int ProjectileVelocity { get; set; }

    [field: SerializeField] public RangedWeaponType WeaponType { get; set; }

    public enum RangedWeaponType
    {
        bow,
        crossbow
    }

    #region SaveSystem
    private bool isRangedWeaponInitialized = false;
    public void Init(RangedWeaponData rangedWeaponData)
    {
        if (!isRangedWeaponInitialized)
        {
            isRangedWeaponInitialized = true;
            base.Init(rangedWeaponData);

            WeaponType = (RangedWeaponType)rangedWeaponData.weaponType;
        }
    }
    #endregion SaveSystem
}
