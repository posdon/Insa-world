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
    public class RaceImpl : Race
    {
        //Attributes
        public int AtkPoint
        {
            get;
            set;
        }

        public int DefPoint
        {
            get;
            set;
        }

        public int LifePoint
        {
            get;
            set;
        }

        public double MovePoint
        {
            get;
            set;
        }

        public Dictionary<Tile, double> MovePointCost
        {
            get;
            set;
        }

        public Dictionary<Tile, int> VictoryPoint
        {
            get;
            set;
        }


        //Default Constructor
        public RaceImpl()
        {
            LifePoint = 0; AtkPoint = 0; DefPoint = 0; MovePoint = 0;
            VictoryPoint = new Dictionary<Tile, int>();
            MovePointCost = new Dictionary<Tile, double>();
        }

        //Specific Constructor
        public RaceImpl(int lPoint, int aPoint, int dPoint, double mPoint)
        {
            LifePoint = lPoint; AtkPoint = aPoint; DefPoint = dPoint; MovePoint = mPoint;
            VictoryPoint = new Dictionary<Tile, int>();
            MovePointCost = new Dictionary<Tile, double>();
        }

        //Specifics methodes
        public int GetLifePoint()
        {
            return LifePoint;
        }

        public int GetAtkPoint()
        {
            return AtkPoint;
        }

        public int GetDefPoint()
        {
            return DefPoint;
        }

        public double GetMovePointMax()
        {
            return MovePoint;
        }

        //If tileTo isn't in movePoint : return -1
        public double GetMovePointCost(Tile tileTo)
        {
            if (tileTo != null)
            {
                if (MovePointCost.ContainsKey(tileTo))
                {
                    return MovePointCost[tileTo];
                }
            }
            return 0;
        }

        public void AddMovePointCost(Tile tileTo, double mPoint)
        {
            MovePointCost.Add(tileTo, mPoint);
        }

        public int GetVictoryPoint(Tile tileOn)
        {
            if (VictoryPoint.ContainsKey(tileOn))
            {
                return VictoryPoint[tileOn];
            }
            return -1;
        }

        public void AddVictoryPoint(Tile tileOn, int vPoint)
        {
            VictoryPoint.Add(tileOn, vPoint);
        }

        public Dictionary<Tile,Double> GetMoveCost()
        {
            return MovePointCost;
        }
    }
}
