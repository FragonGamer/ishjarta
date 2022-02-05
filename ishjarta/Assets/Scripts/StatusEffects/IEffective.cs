using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StatusEffects
{
    public interface IEffective
    {
        public List<BaseEffect> Effects { get; set; }
    }
}
