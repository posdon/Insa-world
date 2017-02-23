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
    public class StrategieMapImpl : StrategieMap
    {
        // Par défaut tout vaut 0
        public StrategieMapImpl(){
            NbPlayer=0;
            SizeMap=0;
            UnitPerPlayer=0;
            NbTurnMax = 0;
        }

        public StrategieMapImpl(int nbP, int sizeM, int upP, int nbTM)
        {
            NbPlayer = nbP;
            SizeMap = sizeM;
            UnitPerPlayer = upP;
            NbTurnMax = nbTM;
        }

        public int NbPlayer
        {
            get;
            set;
        }

        public int SizeMap
        {
            get;
            set;
        }

        public int UnitPerPlayer
        {
            get;
            set;
        }

        public int NbTurnMax
        {
            get;
            set;
        }
       
        public int GetSizeMap()
        {
            return SizeMap;
        }

        public int GetNbPlayer()
        {
            return NbPlayer;
        }

        public int GetUnitPerPlayer()
        {
            return UnitPerPlayer;
        }

        public int GetNbTurnMax()
        {
            return NbTurnMax;
        }

        //mapName = demo | small | standard
        public StrategieMap GetStrategy(string mapName)
        {
            if (mapName.Equals("demo"))
            {
                return new StrategieMapDemo();
            }
            if (mapName.Equals("small"))
            {
                return new StrategieMapSmall();
            }
            if (mapName.Equals("standard"))
            {
                return new StrategieMapStandard();
            }
            return new StrategieMapImpl();
        }

        //Forme :
        // Strategy_Name='???'
        // Par défaut ??? = NO_STRATEGY
        public virtual String Save()
        {
            String result="";
            result += "Strategy_Name='NO_STRATEGY'\n";
            return result;
        }
    }
}
