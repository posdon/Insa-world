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
    public class Centaur : RaceImpl
    {
        public Centaur():base(10,8,2,3)
        {
            AddMovePointCost(TileFactory.INSTANCE.TilePlain, 0.5);
            AddMovePointCost(TileFactory.INSTANCE.TileDesert, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileSwamp, 1);
            AddMovePointCost(TileFactory.INSTANCE.TileVolcano, 1);
            AddVictoryPoint(TileFactory.INSTANCE.TilePlain, 3);
            AddVictoryPoint(TileFactory.INSTANCE.TileDesert, 2);
            AddVictoryPoint(TileFactory.INSTANCE.TileSwamp, 1);
            AddVictoryPoint(TileFactory.INSTANCE.TileVolcano, 0);

        }
    }
}
