using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/Gram")]

public class Gram : PassiveItem
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float speedAmount;
    [SerializeField] private int attackspeedAmount;


    public override void removeEffect()
    {
         var player = PlayerManager.instance.player.GetComponent<Player>();
        player.RemoveDamageModifierer(damageAmount);
        player.RemoveAttackRate(attackspeedAmount);
        player.RemoveSpeedModifierer(speedAmount);
    }

    public override void triggerEffect()
    {
        var player = PlayerManager.instance.player.GetComponent<Player>();
        player.AddDamageModifierer(damageAmount);
        player.AddAttackRate(attackspeedAmount);
        player.AddSpeedModifierer(speedAmount);
    }
}
