using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "passiv items/OnOddOrEvenMultiplyDamage")]
public class OnOddOrEvenMultiplyDamage : PassiveItem
{
    [SerializeField]
    public bool onOdd;
    [SerializeField]
    public bool isOdd;
    private bool hasInit = false;
    public float multiplier;
    public int lastNumberOfVisitedRooms = 0;
    private bool hasInc = false;
    public Player playerObject;

    public override void triggerEffect()
    {
        Debug.Log("1");
        var player = PlayerManager.instance.player.GetComponent<Player>();
        playerObject = player;

        Debug.Log("2");

        if (player.visitedRooms != lastNumberOfVisitedRooms)
        {
            Debug.Log("3");

            isOdd = player.visitedRooms % 2 == 0 ? false : true;
            if (onOdd && isOdd)
                player.AddDamageModifierer(multiplier);
            else if (onOdd && !isOdd)
                player.RemoveDamageModifierer(multiplier);
            else if (!onOdd && !isOdd)
                player.AddDamageModifierer(multiplier);
            else if (!onOdd && isOdd)
                player.RemoveDamageModifierer(multiplier);
        }
        Debug.Log("4");

        lastNumberOfVisitedRooms = player.visitedRooms;
    }

    public override void removeEffect()
    {
        if (onOdd && lastNumberOfVisitedRooms % 2 != 0)
        {
            playerObject.RemoveDamageModifierer(multiplier);
        }
        else if (!onOdd && lastNumberOfVisitedRooms % 2 == 0)
        {
            playerObject.RemoveDamageModifierer(multiplier);
        }
        Inventory.instance.RemovePeriodiclePassiveItem(this);
    }
}
