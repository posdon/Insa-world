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
    public class StrategieMapSmall : StrategieMapImpl
    {
        public StrategieMapSmall()
            : base(2, 10, 6, 20)
        {

        }

        public override String Save()
        {
            return "Strategy_Name='small'\n";
        }

    }
}
