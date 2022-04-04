using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public MeleeWeaponData meleeWeapon;
    public RangedWeaponData rangedWeapon;
    //public WeaponData currentWeapon;
    public bool IsCurrentWeaponMelee;
    public List<PassivItemData> passiveItems;
    public ActiveItemData activeItem;
    public UsableItemData coins;
    public UsableItemData bombs;
    public UsableItemData keys;
    public UsableItemData armor;

    public InventoryData(Inventory inventory)
    {
        if(inventory.GetMeleeWeapon() != null)
            meleeWeapon = new MeleeWeaponData(inventory.GetMeleeWeapon());

        if (inventory.GetRangedWeapon() != null)
            rangedWeapon = new RangedWeaponData(inventory.GetRangedWeapon());

        if (inventory.CurrentWeapon != null)
            IsCurrentWeaponMelee = inventory.CurrentWeapon is MeleeWeapon;

        passiveItems = inventory.GetPassiveItems().Select(x => new PassivItemData(x)).ToList();

        if (inventory.GetActiveItem() != null)
            activeItem = new ActiveItemData(inventory.GetActiveItem());

        coins = new UsableItemData(inventory.GetCoins());
        bombs = new UsableItemData(inventory.GetBombs());
        keys = new UsableItemData(inventory.GetKeys());
        armor = new UsableItemData(inventory.GetArmor());
    }
}
