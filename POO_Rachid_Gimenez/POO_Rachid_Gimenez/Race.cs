using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     */
    public interface Race
    {
        int GetLifePoint();

        int GetAtkPoint();

        int GetDefPoint();

        double GetMovePointMax();

        double GetMovePointCost(Tile tileTo);

        Dictionary<Tile, Double> GetMoveCost();

        int GetVictoryPoint(Tile tileOn);

        void AddMovePointCost(Tile tileTo, double mPoint);

        void AddVictoryPoint(Tile tileOn, int vPoint);
    }
}
