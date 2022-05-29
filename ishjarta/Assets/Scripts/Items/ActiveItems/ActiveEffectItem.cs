using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class ActiveEffectItem : ActiveItem
{


    public override void Activate(GameObject parent)
    {
        Player player = PlayerManager.instance.player.GetComponent<Player>();

        player.AddEffectRange(OwnerEffects);
    }
}
