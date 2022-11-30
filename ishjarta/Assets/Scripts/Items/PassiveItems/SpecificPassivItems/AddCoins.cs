using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "passiv items/AddCoins")]
public class AddCoins : PassiveItem
{
    public int Amount;
    public override void triggerEffect()
    {

        var p = Utils.LoadAssetsFromAddressablesByLabel<AssetReference>(new string[] { "Item", "UsableItem" });
        var coins = Utils.LoadGameObjectFromAddressablesByReferenceWithName(p, "Coin").GetComponent<UsableItem>();
        Utils.UnloadAssetReferences(p);
        var inv = Inventory.instance;
        coins.Amount = Amount;

        inv.AddUsableItem(coins);


        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }

    public override void removeEffect()
    {
        var p = Utils.LoadAssetsFromAddressablesByLabel<AssetReference>(new string[] { "Item", "UsableItem" });
        var coins = Utils.LoadGameObjectFromAddressablesByReferenceWithName(p, "Coin").GetComponent<UsableItem>();
        Utils.UnloadAssetReferences(p);
        var inv = Inventory.instance;
        coins.Amount = Amount;
        Inventory.instance.DropItem(coins);
    }
}
