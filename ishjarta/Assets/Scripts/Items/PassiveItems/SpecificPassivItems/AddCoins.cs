using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/AddCoins")]
public class AddCoins : PassiveItem
{
    public int Amount;
    public override void triggerEffect()
    {

        var p = Utils.loadAssetPack("usableitem");
        var coins = p.LoadAsset("Coin") as UsableItem;
        var inv = Inventory.instance;
        coins.Amount = Amount;
        p.Unload(true);

        inv.AddUsableItem(coins);


        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }

    public override void removeEffect()
    {
        var p = Utils.loadAssetPack("usableitem");
        var coins = p.LoadAsset("Coin") as UsableItem;
        var inv = Inventory.instance;
        coins.Amount = Amount;
        p.Unload(true);
        Inventory.instance.DropItem(coins);
    }
}
