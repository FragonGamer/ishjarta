using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/AddMaxHealth")]
public class PlusMaxHealth : PassiveItem
{
    public int Amount;
    public override void triggerEffect()
    {
        var player = PlayerManager.instance.player.GetComponent<Player>();
        player.AddToMaxHealth(Amount);
        player.AddToCurrentHealth(Amount);
        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }

    public override void removeEffect()
    {
        var player = PlayerManager.instance.player.GetComponent<Player>();
        player.RemoveFromMaxHealth(Amount);
        if (player.CurrentHealth - Amount > 0)
        {
            player.RemoveFromMaxHealth(Amount);
        }
    }
}
