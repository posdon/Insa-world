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
    public interface StrategieMap
    {
        int GetSizeMap();

        int GetNbPlayer();

        int GetUnitPerPlayer();

        int GetNbTurnMax();

        StrategieMap GetStrategy(string mapName);

        String Save();
    }
}
