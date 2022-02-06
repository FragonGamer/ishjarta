using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StatusEffects
{
    public interface IEffective
    {
        // Effects which the owner gets
        public List<BaseEffect> OwnerEffects { get; set; }

        // Effects which will be passed on to the enemy
        public List<BaseEffect> EmitEffects { get; set; }
    }
}
