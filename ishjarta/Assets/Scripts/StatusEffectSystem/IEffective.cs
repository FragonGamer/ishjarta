using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StatusEffectSystem
{
    /// <summary>
    /// The interface that enables items to have an effect
    /// </summary>
    public interface IEffective
    {
        /// <summary>
        /// This field specifies the effects which the owner gets
        /// </summary>
        public List<BaseEffect> OwnerEffects { get; set; }

        /// <summary>
        /// This field specifies the effects which will be passed on to the enemy
        /// </summary>
        public List<BaseEffect> EmitEffects { get; set; }
    }
}
