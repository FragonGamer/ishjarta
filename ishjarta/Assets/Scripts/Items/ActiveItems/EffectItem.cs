using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.ActiveItems
{
    [CreateAssetMenu]
    public class EffectItem : ActiveItem
    {
        public override void Activate(GameObject parent)
        {
            Player player = PlayerManager.instance.player.GetComponent<Player>();

            player.AddEffectRange(EmitEffects.Select(x => BaseEffect.ReturnCopy(x)));
        }
    }
}
