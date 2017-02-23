using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     * ReadMe check !
     */
    public class StrategieMapStandard : StrategieMapImpl
    {
        public StrategieMapStandard()
            : base(2, 14, 8, 30)
        {
        }

        
        public override String Save() {
            return "Strategy_Name='standard'\n";
        }
    }
}
