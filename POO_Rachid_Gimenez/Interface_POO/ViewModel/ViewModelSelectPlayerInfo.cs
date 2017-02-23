using System;
using System.Collections.Generic;
using POO_Rachid_Gimenez;
using System.Windows.Input;

namespace Interface_POO
{
    class ViewModelSelectPlayerInfo : ViewModelBase
    {
        #region fields
        private ViewModelMainWindow refMain;
        private GameBuilder gameBuilder;
        private ICommand backCommand;
        private ICommand playCommand;
        private Player player1;
        private Player player2;
        private List<String> cmbContent;
        #endregion

        public ViewModelSelectPlayerInfo(ViewModelMainWindow mainWindow, GameBuilder gb)
        {
            this.refMain = mainWindow;
            this.gameBuilder = gb;
            player1 = new Player("Joueur 1","centaur");
            player2 = new Player("Joueur 2", "cyclops");
            cmbContent = new List<String>();
            cmbContent.Add("centaur");
            cmbContent.Add("cerberus");
            cmbContent.Add("cyclops");
        }

        #region property
        public List<String> CmbContent1
        {
            get
            {
                List<String> cmbContent1 = new List<String>();
                foreach (String s in cmbContent)
                {
                    if (!s.Equals(RacePlayer2))
                    {
                        cmbContent1.Add(s);
                    }
                }
                return cmbContent1;
            }
            set
            {
            }
        }

        public List<String> CmbContent2
        {
            get
            {
                List<String> cmbContent2 = new List<String>();
                foreach (String s in cmbContent)
                {
                    if (!s.Equals(RacePlayer1))
                    {
                        cmbContent2.Add(s);
                    }
                }
                return cmbContent2;
            }
            set
            {
            }
        }

        public String NamePlayer1
        {
            get
            {
                return player1.Name;
            }
            set
            {
                player1.Name = value;
                OnPropertyChanged("NamePlayer1");
            }
        }

        public String NamePlayer2
        {
            get
            {
                return player2.Name;
            }
            set
            {
                player2.Name = value;
                OnPropertyChanged("NamePlayer2");
            }
        }

        public String RacePlayer1
        {
            get
            {
                return player1.RaceString;
            }
            set
            {
                player1.RaceString = value;
                OnPropertyChanged("RacePlayer1");
                OnPropertyChanged("CmbContent2");
                OnPropertyChanged("DisplayedImage1");
            }
        }

        public String RacePlayer2
        {
            get
            {
                return player2.RaceString;
            }
            set
            {
                
                player2.RaceString = value;
                OnPropertyChanged("RacePlayer2");
                OnPropertyChanged("CmbContent1");
                OnPropertyChanged("DisplayedImage2");
            }
        }

        public string DisplayedImage1
        {
            get { return "/Images/" + char.ToUpper(RacePlayer1[0]) + RacePlayer1.Substring(1) + ".png"; }
        }
        public string DisplayedImage2
        {
            get { return "/Images/" + char.ToUpper(RacePlayer2[0]) + RacePlayer2.Substring(1) + ".png"; }
        }

        public ICommand BackCommand
        {
            get
            {
                if (this.backCommand == null)
                    this.backCommand = new RelayCommand(() => this.Back());

                return this.backCommand;
            }
        }

        public void Back()
        {
            this.refMain.ViewNewGameCommand();
        }

        public ICommand PlayCommand
        {
            get
            {
                if (this.playCommand == null)
                    this.playCommand = new RelayCommand(() => this.Play());

                return this.playCommand;
            }
        }

        public void Play()
        {
            Player[] pTab = { this.player1, this.player2 };
            this.gameBuilder.AddPlayer(pTab);
            this.refMain.ViewGameCommand(this.gameBuilder.Build());
        }

        #endregion
    }
}
