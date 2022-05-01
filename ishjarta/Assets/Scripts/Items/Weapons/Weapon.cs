using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackRate { get; set; }

    #region SaveSystem
    private bool isWeaponInitialized = false;
    protected void Init(WeaponData weaponData)
    {
        if (!isWeaponInitialized)
        {
            isWeaponInitialized = true;
            base.Init(weaponData);
        }
    }
    #endregion SaveSystem
    [field: SerializeField] public bool IsChargable { get; set; }
}
