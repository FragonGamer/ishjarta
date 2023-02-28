using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/Gungnir")]

public class Gungnir : PassiveItem
{
     [SerializeField]
    public bool onOdd;
    [SerializeField]
    public bool isOdd;
    private bool hasInit = false;
    public float multiplier;
    public int lastNumberOfVisitedRooms = 0;
    public Player playerObject;

    public override void triggerEffect()
    {
        var player = PlayerManager.instance.player.GetComponent<Player>();
        playerObject = player;


        if (player.visitedRooms != lastNumberOfVisitedRooms)
        {

            isOdd = player.visitedRooms % 2 == 0 ? false : true;
            if (onOdd && isOdd)
               { player.AddDamageModifierer(multiplier); hasInit = true;}
            else if (onOdd && !isOdd && hasInit)
               { player.RemoveDamageModifierer(multiplier);}
            else if (!onOdd && !isOdd )
                {player.AddDamageModifierer(multiplier);hasInit = true;}
            else if (!onOdd && isOdd && hasInit)
                {player.RemoveDamageModifierer(multiplier);}
        }

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
    }
}
