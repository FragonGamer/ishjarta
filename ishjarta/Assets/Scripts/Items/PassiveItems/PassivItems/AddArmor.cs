using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "passiv items/AddArmor")]
public class AddArmor : PassiveItem
{
    [SerializeField] public int Amount;

    public override async void triggerEffect()
    {
        var p = Utils.loadAssetPack("usableitem");
        var asdf = p.LoadAllAssets();
        var armor = p.LoadAsset("Armor") as UsableItem;
        var inv = Inventory.instance;
        armor.Amount = Amount;
        p.Unload(true);

        inv.AddUsableItem(armor);


        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }

    public override void removeEffect()
    {
        var p = Utils.loadAssetPack("usableitem");
        var asdf = p.LoadAllAssets();
        var armor = p.LoadAsset("Armor") as UsableItem;
        var inv = Inventory.instance;
        armor.Amount = Amount;
        p.Unload(true);
        Inventory.instance.DropItem(armor);
    }
}