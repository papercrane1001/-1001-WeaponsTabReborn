using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponsTabReborn
{
    public sealed class VariableReference<VarType>
    {
        public VarType Val;

        public VarType Get()
        {
            return Val;
        }
        public void Set(VarType newval)
        {
            Val = newval;
        }
        public VariableReference(VarType startval)
        {
            Val = startval;
        }
    }
}
