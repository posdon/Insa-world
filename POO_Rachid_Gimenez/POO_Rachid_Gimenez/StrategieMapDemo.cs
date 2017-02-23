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
    public class StrategieMapDemo : StrategieMapImpl
    {
        public StrategieMapDemo()
            : base(2, 6, 4, 5)
        {

        }

        public override String Save()
        {
            return "Strategy_Name='demo'\n";
        }
    }
}
