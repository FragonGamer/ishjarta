using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "passiv items/AddCoins")]
public class AddCoins : PassiveItem
{
    [SerializeField]
    public int Amount = 1;
    public override void triggerEffect()
    {

        var p = Utils.LoadIRessourceLocations<ScriptableObject>(new string[] { "ScriptableObject", "UsableItem" });
        var coins = Utils.LoadItemByName<UsableItem>(p, "Coin");
        var coin = Instantiate(coins);
        var inv = Inventory.instance;
        coin.Amount = Amount;

        inv.AddUsableItem(coin);
        coins.Amount = 1;


    }

    public override void removeEffect()
    {
        var p = Utils.LoadIRessourceLocations<GameObject>(new string[] { "ScriptableObject", "UsableItem" });
        var coins = Utils.LoadItemByName<UsableItem>(p, "Coin");
        var coin = Instantiate(coins);
        var inv = Inventory.instance;
        coin.Amount = Amount;
        Inventory.instance.DropItem(coin);
                coins.Amount = 1;

    }
}
