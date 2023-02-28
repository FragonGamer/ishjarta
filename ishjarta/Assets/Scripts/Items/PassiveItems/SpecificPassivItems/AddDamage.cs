using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/AddDamage")]

public class AddDamage : PassiveItem
{
    [SerializeField] float amount;
    public override void removeEffect()
    {
        
 var player = PlayerManager.instance.player.GetComponent<Player>();
        player.RemoveDamageModifierer(amount);

    }

    public override void triggerEffect()
    {
        var player = PlayerManager.instance.player.GetComponent<Player>();
        player.AddDamageModifierer(amount);

    }
}
