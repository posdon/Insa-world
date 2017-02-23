using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;


namespace POO_Rachid_Gimenez
{
    /*
     * Check classe
     * Check Readme
     */
    public class Map
    {
        private int nbPlayer;
        private int nbUnitPerPlayer;
        private int nbSizeMap;

        public Map()
        {
            SetStrategy("");
            TileList = new List<Tile>();
            GridEntity = new List<Entity>();
            SpawnList = new List<int>();
        }

        public Map(String strategyName)
        {
            SetStrategy(strategyName);
            TileList = new List<Tile>();
            GridEntity = new List<Entity>();
            SpawnList = new List<int>();
            GenerateRandom();
        }

        public List<Tile> TileList
        {
            get;
            set;
        }

        public StrategieMap Strategie
        {
            get;
            set;
        }

        public List<Entity> GridEntity
        {
            get;
            set;
        }

        public List<int> SpawnList
        {
            get;
            set;
        }

        
        // Setters

        // Set strategy from the associated name
        public void SetStrategy(String mapName)
        {
            StrategieMapImpl m = new StrategieMapImpl();
            Strategie = m.GetStrategy(mapName);
            SetStrategyValue();
        }

        // Set the value as size, number of unit etc... in function of the strategy
        public void SetStrategyValue()
        {
            nbPlayer = Strategie.GetNbPlayer();
            nbSizeMap = Strategie.GetSizeMap();
            nbUnitPerPlayer = Strategie.GetUnitPerPlayer();
        }

        // Set all the grid and spawn randomly
        public void GenerateRandom()
        {
            Dll dll = new Dll();
            TileType[] tiles = new TileType[nbSizeMap * nbSizeMap];
            tiles = dll.CreateGrid(nbSizeMap * nbSizeMap);
            SetGrid(tiles);
            SetSpawn(dll.CreateSpawn(nbSizeMap * nbSizeMap, nbPlayer));
        }

        // Convert int[] into List<int>
        public void SetSpawn(int[] spawnTab)
        {
            for (int i = 0; i < spawnTab.Length; i++)
            {
                SpawnList.Add(spawnTab[i]);
            }
        }

        // Methode linked to dll's map generation
        public void SetGrid(TileType[] tiles)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i] == TileType.Desert)
                {
                    TileList.Add(TileFactory.INSTANCE.TileDesert);
                }
                if (tiles[i] == TileType.Plain)
                {
                    TileList.Add(TileFactory.INSTANCE.TilePlain);
                }
                if (tiles[i] == TileType.Swamp)
                {
                    TileList.Add(TileFactory.INSTANCE.TileSwamp);
                }
                if (tiles[i] == TileType.Volcano)
                {
                    TileList.Add(TileFactory.INSTANCE.TileVolcano);
                }
            }
        }

        // Method linked to saved map generation
        public void SetGrid(int[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] == 0)
                {
                    TileList.Add(TileFactory.INSTANCE.TileDesert);
                }
                if (param[i] == 1)
                {
                    TileList.Add(TileFactory.INSTANCE.TilePlain);
                }
                if (param[i] == 2)
                {
                    TileList.Add(TileFactory.INSTANCE.TileSwamp);
                }
                if (param[i] == 3)
                {
                    TileList.Add(TileFactory.INSTANCE.TileVolcano);
                }
            }
        }

        // Set victory point from the given player
        public void SetVictoryPoint(Player player)
        {
            int result = 0;
            ArrayList listPos = new ArrayList();
            foreach (Entity e in player.EntityList)
            {
                if (!listPos.Contains(e.Pos)&&e.Pos!=-1)
                {
                    result += e.Race.GetVictoryPoint(TileList[e.Pos]);
                    listPos.Add(e.Pos);
                }
            }
            Console.WriteLine(result);
            player.VictoryPoint = result;
        }

        // if pos isn't a tile reference, return null
        public Tile GetTile(int pos)
        {
            if ((TileList.Count > pos) && (pos >= 0))
            {
                return TileList[pos];
            }
            return null;
        }

        //True if currEntity has been removed
        public bool Remove(int pos, Entity currEntity)
        {
            if (GridEntity.Contains(currEntity))
            {
                if(currEntity.Pos==pos)
                {
                    GridEntity.Remove(currEntity);
                    currEntity.Pos = -1;
                    return true;
                }
            }
            return false;
        }

        //Vrai si l'entité a été ajoutée
        // Ne peut ajouter une entité présente sur la grille
        // Ne peut ajouter une entité sur une case occupée par une entité ennemie.
        public bool Add(int pos, Entity entity)
        {
            if (!GridEntity.Contains(entity))
            {
                foreach (Entity e in GridEntity)
                {
                    if (e != entity && e.Pos==entity.Pos )
                    {
                        if (e.Team == entity.Team)
                        {
                            GridEntity.Add(entity);
                            entity.Pos=pos;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }  
                }
                GridEntity.Add(entity);
                entity.Pos=pos;
                return true;
            }
            return false;
        }

        public bool CanGo(Entity e, int pos)
        {
            return GetTeamOn(pos) == e.Team || GetTeamOn(pos) == -1;
        }

        public int GetTeamOn(int pos)
        {
            foreach (Entity e in GridEntity)
            {
                if (e.Pos == pos)
                {
                    return e.Team;
                }
            }
            return -1;
        }

        public double GetDistance(Entity e, int pos)
        {
            int oldPos = e.Pos;
            return (oldPos-1==pos||oldPos+1==pos||oldPos+Strategie.GetSizeMap()==pos||oldPos-Strategie.GetSizeMap()==pos)?e.Race.GetMovePointCost(this.GetTile(oldPos)):Double.MaxValue;

            /*double[,] minValueTab = new double[nbSizeMap,nbSizeMap];
            int[,] precTab = new int[nbSizeMap , nbSizeMap];

            double minValueSwapped = 0;

            for (int i = 0; i < nbSizeMap; i++)
            {
                for(int j = 0; j < nbSizeMap; j++)
                {
                    minValueTab[i,j] = Double.MaxValue;
                    precTab[i,j] = -1;
                }
            }
            minValueTab[pos / nbSizeMap, pos % nbSizeMap] = 0;
            do
            {
                minValueSwapped = Double.MaxValue;
                for (int i = 0; i < nbSizeMap; i++)
                {
                    for (int j = 0; j < nbSizeMap; j++)
                    {
                        if (precTab[i, j] != -1)
                        {
                            double myCost = minValueTab[i, j] + e.Race.GetMovePointCost(TileList[i * nbSizeMap + j]);
                            if (j > 0)
                            {
                                if (minValueTab[i, j - 1] > myCost && (GetTeamOn(i*nbSizeMap+j-1)==0 || GetTeamOn(i*nbSizeMap+j-1)==e.Team) )
                                {
                                    minValueTab[i, j - 1] = myCost;
                                    precTab[i, j - 1] = i * nbSizeMap + j;
                                    if (minValueSwapped > myCost)
                                    {
                                        minValueSwapped = myCost;
                                    }
                                }
                            }
                            if (j < nbSizeMap - 1)
                            {
                                if (minValueTab[i, j + 1] > myCost && (GetTeamOn(i * nbSizeMap + j + 1) == 0 || GetTeamOn(i * nbSizeMap + j + 1) == e.Team))
                                {
                                    minValueTab[i, j + 1] = myCost;
                                    precTab[i, j + 1] = i * nbSizeMap + j;
                                    if (minValueSwapped > myCost)
                                    {
                                        minValueSwapped = myCost;
                                    }
                                }
                            }
                            if (i > 0)
                            {
                                if (minValueTab[i - 1, j] > myCost)
                                {
                                    minValueTab[i - 1, j] = myCost;
                                    precTab[i - 1, j] = i * nbSizeMap + j;
                                    if (minValueSwapped > myCost && (GetTeamOn((i - 1) * nbSizeMap + j) == 0 || GetTeamOn((i - 1) * nbSizeMap + j) == e.Team))
                                    {
                                        minValueSwapped = myCost;
                                    }
                                }
                            }
                            if (i < nbSizeMap - 1)
                            {
                                if (minValueTab[i + 1, j] > myCost)
                                {
                                    minValueTab[i + 1, j] = myCost;
                                    precTab[i + 1, j] = i * nbSizeMap + j;
                                    if (minValueSwapped > myCost && (GetTeamOn((i + 1) * nbSizeMap + j) == 0 || GetTeamOn((i + 1) * nbSizeMap + j) == e.Team))
                                    {
                                        minValueSwapped = myCost;
                                    }
                                }
                            }
                        }
                    }
                }
            } while (minValueSwapped < precTab[pos / nbSizeMap, pos % nbSizeMap]);
            // Si aucun changement effectué : minValueSwapped = MAXVALUE > precTab[x,y]
            // Si minValueSwapped > precTab[x,y], aucun prochain changement ne fera baissé la valeur minimale du chemin.
            int lengthWay = 0;
            int prec = pos;
            while (precTab[prec / nbSizeMap, prec % nbSizeMap] != -1)
            {
                lengthWay++;
                prec = precTab[prec / nbSizeMap, prec % nbSizeMap];
            }
            double[] result = new double[lengthWay + 1];
            result[0] = minValueTab[pos / nbSizeMap, pos % nbSizeMap];
            prec = pos;
            while (precTab[prec / nbSizeMap, prec % nbSizeMap] != -1)
            {
                lengthWay--;
                result[lengthWay - 1] = prec;
                prec = precTab[prec / nbSizeMap, prec % nbSizeMap];
            }
            return result;*/
        }

        //If no entity on tile pos, return null
        public Entity GetBestDefenser(int pos)
        {
            Entity bestOne = null;
            float bestDef = 0;
            foreach (Entity e in GridEntity)
            {
                if (e.Pos == pos)
                {
                    float valDef =(float)e.Race.GetDefPoint() * (float)e.LifePoint / (float)e.Race.GetLifePoint();
                    if ( valDef > bestDef)
                    {
                        bestDef = valDef;
                        bestOne = e;
                    }
                }
            }
            return bestOne;
        }


        // Form :
        // [STRATEGY SAVE]
        // <GRID>
        // 0 0 0 0 ...
        // </GRID>
        // Spawn='... ...'
        public string[] Save()
        {
            //Save strategy
            string[] result = new string[5];
            result[0] = Strategie.Save();

            //Save grid
            result[1] = "<GRID>";
            String line = "";
            for (int i = 0; i < nbSizeMap; i++)
            {
                for (int j = 0; j < nbSizeMap; j++)
                {
                    Console.Write(i * nbSizeMap + j);
                    if (TileList[i * nbSizeMap + j].Equals(TileFactory.INSTANCE.TileDesert))
                    {
                        line += "0 ";
                    }
                    if (TileList[i * nbSizeMap + j].Equals(TileFactory.INSTANCE.TilePlain))
                    {
                        line += "1 ";
                    }
                    if (TileList[i * nbSizeMap + j].Equals(TileFactory.INSTANCE.TileSwamp))
                    {
                        line += "2 ";
                    }
                    if (TileList[i * nbSizeMap + j].Equals(TileFactory.INSTANCE.TileVolcano))
                    {
                        line += "3 ";
                    }
                }
            }
            result [2] = line;
            result [3] = "</GRID>";
            result [4] = "Spawn='"+SpawnList[0] + " "+SpawnList[1]+"'";
            return result;
        }
    }
}
