using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "passiv items/AddArmor")]
public class AddArmor : PassiveItem
{
    [SerializeField] public int Amount;

    public override async void triggerEffect()
    {
        await new Task(() =>
        {
            var p = Utils.LoadAssetsFromAddressablesByLabel<AssetReference>(new string[] { "Item", "UsableItem" });
            var armor = Utils.LoadGameObjectFromAddressablesByReferenceWithName(p, "Armor").GetComponent<UsableItem>();
            Utils.UnloadAssetReferences(p);
            var inv = Inventory.instance;
            armor.Amount = Amount;
            inv.AddUsableItem(armor);


            Inventory.instance.RemovePeriodiclePassiveItem(this);
        });
    }

    public override void removeEffect()
    {
        var p = Utils.LoadAssetsFromAddressablesByLabel<AssetReference>(new string[] { "Item", "UsableItem" });
        var armor = Utils.LoadGameObjectFromAddressablesByReferenceWithName(p, "Armor").GetComponent<UsableItem>();
        Utils.UnloadAssetReferences(p);
        var inv = Inventory.instance;
        armor.Amount = Amount;
        Inventory.instance.DropItem(armor);
    }
}