using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     */
    public class Cerberus : RaceImpl
    {
        public Cerberus():base(10,6,4,3)
        {
            AddVictoryPoint(TileFactory.INSTANCE.TilePlain, 0);
            AddVictoryPoint(TileFactory.INSTANCE.TileDesert, 1);
            AddVictoryPoint(TileFactory.INSTANCE.TileSwamp, 2);
            AddVictoryPoint(TileFactory.INSTANCE.TileVolcano, 3);
            AddMovePointCost(TileFactory.INSTANCE.TileDesert, 1);
            AddMovePointCost(TileFactory.INSTANCE.TilePlain, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileSwamp, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileVolcano, 1);
        }
    }
}
