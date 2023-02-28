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

        var p = Utils.LoadIRessourceLocations<ScriptableObject>(new string[] { "ScriptableObject", "UsableItem" });
        var coins = Utils.LoadItemByName<UsableItem>(p, "Coin");
        var inv = Inventory.instance;
        coins.Amount = Amount;

        inv.AddUsableItem(coins);


    }

    public override void removeEffect()
    {
        var p = Utils.LoadIRessourceLocations<GameObject>(new string[] { "ScriptableObject", "UsableItem" });
        var coins = Utils.LoadItemByName<UsableItem>(p, "Coin");
        var inv = Inventory.instance;
        coins.Amount = Amount;
        Inventory.instance.DropItem(coins);
    }
}
