using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     */
    public class Cyclops : RaceImpl
    {
        public Cyclops(): base(12, 4, 6, 3)
        {
            AddVictoryPoint(TileFactory.INSTANCE.TilePlain, 2);
            AddVictoryPoint(TileFactory.INSTANCE.TileDesert, 3);
            AddVictoryPoint(TileFactory.INSTANCE.TileSwamp, 0);
            AddVictoryPoint(TileFactory.INSTANCE.TileVolcano, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileDesert, 1);
            AddMovePointCost(TileFactory.INSTANCE.TilePlain, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileSwamp, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileVolcano, 1);
        }
    }
}
