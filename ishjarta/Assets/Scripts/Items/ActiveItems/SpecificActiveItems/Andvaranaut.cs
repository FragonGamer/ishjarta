using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "active items/Andvaranaut")]
public class Andvaranaut : ActiveItem
{
    private int uses = 0;
    private int chance = 45;
    public override void Activate(GameObject parent)
    {
        var inv = parent.GetComponent<Inventory>();
        var player = parent.GetComponent<Player>();
        var random_val = Random.value;
        var percent = chance / 100f - uses/100f;
        if (percent < 0.1f)
            percent = 0.1f;
        if(random_val >= 0f && random_val <= percent){
            
            UsableItem coin = Inventory.instance.GetUsableItem(UsableItem.UsableItemtype.coin);
            coin.Amount = inv.GetCoins().Amount;
            inv.AddItem(coin);
        }
        else{
            player.ReceiveDamage(999999999);
        }


    }
}
