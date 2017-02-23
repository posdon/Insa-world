using System;
using POO_Rachid_Gimenez;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

namespace Interface_POO
{
    class ViewModelGame : ViewModelBase
    {
        #region fields
        private ViewModelMainWindow mainRef;
        protected Game game;
        private String fightingBox;
        private ICommand quitCommand;

        private ObservableCollection<ITile> itemList;
        #endregion
        public String NbTurn
        {
            get { return "Tour : " + (Game.CurrTurnNumber + 1) + "/" + Game.TurnMax; }
        }
        public ViewModelGame(Game g, ViewModelMainWindow main)
        {

            this.mainRef = main;
            game = g;
            fightingBox = "";
            itemList = new ObservableCollection<ITile>();
        }

        public void ReloadMap()
        {
            itemList = new ObservableCollection<ITile>();
            int nbTile = 0;
            foreach (Tile t in Game.Map.TileList)
            {
                ITile tile = new ITile(this, nbTile, "/Images/");
                if (t.GetType() == typeof(Swamp))
                {
                    tile.DisplayTileImage += "swamp";
                }
                if (t.GetType() == typeof(Desert))
                {
                    tile.DisplayTileImage += "desert";
                }
                if (t.GetType() == typeof(Plain))
                {
                    tile.DisplayTileImage += "grass";
                }
                if (t.GetType() == typeof(Volcano))
                {
                    tile.DisplayTileImage += "volcano";
                }
                int p = Game.Map.GetTeamOn(nbTile);
                if (p != -1)
                {
                    String race = Game.ListPlayer[p].RaceString;
                    tile.DisplayTileImage += char.ToUpper(race[0]) + race.Substring(1);
                    tile.NbEntityOn = Game.ListPlayer[p].GetNbEntityOn(nbTile);
                }
                else
                {
                    tile.NbEntityOn = 0;
                }
                tile.DisplayTileImage += "Tile";
                if (Game.CurrEntity != null)
                    if (Game.CurrEntity.Pos == nbTile)
                    {
                        tile.DisplayTileImage += "Curr";
                    }


                tile.DisplayTileImage += ".jpg";

                tile.OnClickCommandMode = 1;

                itemList.Add(tile);
                nbTile++;
            }
            int verif = Game.VerifEndGame();
            if (verif != -1)
            {
                mainRef.ViewResultGame(verif, Game);
            }
            OnPropertyChanged("ItemList");
            OnPropertyChanged("NbTurn");
            OnPropertyChanged("FightingBox");
        }

        #region properties
        public ObservableCollection<ITile> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }

        public Game Game
        {
            get { return game; }
            set { game = value; }
        }

        public ViewModelMainWindow MainRef
        {
            get { return mainRef; }
            set { mainRef = value; }
        }

        #region command
        public ICommand QuitCommand
        {
            get
            {
                if (quitCommand == null)
                    quitCommand = new RelayCommand(() => Quit());
                return quitCommand;
            }
            set
            {
                quitCommand = value;
            }
        }

        public void Quit()
        {
            mainRef.ViewMainPageCommand();
        }
        #endregion

        #region visualInfo
        public int ColumnNumber
        {
            get
            {
                return Game.Map.Strategie.GetSizeMap();
            }
            set
            {

            }
        }

        public String FightingBox
        {
            get { return fightingBox; }
            set
            {
                fightingBox = value;
                OnPropertyChanged("FightingBox");
            }
        }

        public String CurrentPlayerName
        {
            get
            {
                return Game.ListPlayer[0].Name;
            }
        }

        public String CurrentPlayerRaceImage
        {
            get
            {
                String race = Game.ListPlayer[Game.CurrPlayerNumber].RaceString;
                return "/Images/" + char.ToUpper(race[0]) + race.Substring(1) + ".png";
            }
        }

        public String CurrentPlayerPoint
        {
            get
            {
                return "Pts : " + Game.ListPlayer[0].VictoryPoint;
            }
        }

        public String WaitingPlayerPoint
        {
            get { return "Pts : " + Game.ListPlayer[1].VictoryPoint; }
        }

        public String WaitingPlayerName
        {
            get
            {
                return Game.ListPlayer[1].Name;
            }
        }
        #endregion

        #endregion
    }
}
