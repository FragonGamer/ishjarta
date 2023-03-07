using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "passiv items/Met")]

public class Met : PassiveItem
{
    public override void removeEffect()
    {var player = PlayerManager.instance.player.GetComponent<Player>();
                player.canSeeFullDesc = true;
    }

    public override void triggerEffect()
    {
                var player = PlayerManager.instance.player.GetComponent<Player>();
                player.canSeeFullDesc = true;
        
    }
}
