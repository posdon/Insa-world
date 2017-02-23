using System;
using POO_Rachid_Gimenez;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;

namespace Interface_POO
{
    class ViewModelGamePlay : ViewModelGame
    {

        #region fields

        private ICommand enterKeyCommand;
        private ICommand spaceMoveCommand;
        private ICommand leftMoveCommand;
        private ICommand rightMoveCommand;
        private ICommand upMoveCommand;
        private ICommand downMoveCommand;
        private ICommand nextPlayerCommand;
        private ICommand nextEntityCommand;
        private ICommand saveCommand;
        private ICommand surrendCommand;


        #endregion

        public ViewModelGamePlay(Game g, ViewModelMainWindow main):base(g,main)
        {
            this.Game = g;
            ReloadMap();
        }

        public void MoveTo(int pos)
        {
            if (Game.Move(pos))
            {
                ReloadMap();
                EditAction();
                if (Game.CurrEntity.Pos == -1)
                {
                    NextEntity();
                }
                OnPropertyChanged("ItemList");
                OnPropertyChanged("CurrentLifeText");
                OnPropertyChanged("CurrentMoveText");
            }
        }



        public void EditAction()
        {

            POO_Rachid_Gimenez.Action lastAction = Game.Action[Game.Action.Count-1];
            if (lastAction.GetType() == typeof(FightAction))
            {
                int dmg = ((FightAction)lastAction).Damage;
                if (dmg > 0)
                {
                    FightingBox = Game.ListPlayer[Game.CurrPlayerNumber].Name + " attaque : " + dmg + " dégâts.";
                }
                if (dmg < 0)
                {
                    FightingBox = (Game.CurrPlayerNumber == 0) ? Game.ListPlayer[1].Name + " contre : " + dmg + " dégâts." : Game.ListPlayer[0].Name + " contre : " + dmg + " dégâts.";
                }
                if (dmg == 0)
                {
                    FightingBox = "Rien ne se passe.";
                }
            }
        }


        #region properties

        #region command
        public ICommand EnterKeyCommand
        {
            get
            {
                if (enterKeyCommand == null)
                    enterKeyCommand = new RelayCommand(() => EnterKey());
                return enterKeyCommand;
            }
            set
            {
                enterKeyCommand = value;
            }
        }

        public void EnterKey()
        {
            NextPlayer();
        }

        public ICommand LeftMoveCommand
        {
            get
            {
                if (leftMoveCommand == null)
                    leftMoveCommand = new RelayCommand(() => LeftMove());
                return leftMoveCommand;
            }
            set
            {
                leftMoveCommand = value;
            }
        }

        public void LeftMove()
        {
            if (Game.CurrEntity != null)
            {
                int pos = Game.CurrEntity.Pos;
                if (pos >= 0 && pos % Game.Map.Strategie.GetSizeMap() != 0)
                    MoveTo(pos - 1);
            }
        }

        public ICommand UpMoveCommand
        {
            get
            {
                if (upMoveCommand == null)
                    upMoveCommand = new RelayCommand(() => UpMove());
                return upMoveCommand;
            }
            set
            {
                upMoveCommand = value;
            }
        }

        public void UpMove()
        {
            if (Game.CurrEntity != null)
            {
                int pos = Game.CurrEntity.Pos;
                if (pos >= 0 && pos / Game.Map.Strategie.GetSizeMap() > 0)
                    MoveTo(pos - Game.Map.Strategie.GetSizeMap());
            }
        }

        public ICommand DownMoveCommand
        {
            get
            {
                if (downMoveCommand == null)
                    downMoveCommand = new RelayCommand(() => DownMove());
                return downMoveCommand;
            }
            set
            {
                downMoveCommand = value;
            }
        }

        public void DownMove()
        {
            if (Game.CurrEntity != null)
            {
                int pos = Game.CurrEntity.Pos;
                if (pos >= 0 && pos / Game.Map.Strategie.GetSizeMap() < Game.Map.Strategie.GetSizeMap() - 1)
                    MoveTo(pos + Game.Map.Strategie.GetSizeMap());
            }
        }

        public ICommand RightMoveCommand
        {
            get
            {
                if (rightMoveCommand == null)
                    rightMoveCommand = new RelayCommand(() => RightMove());
                return rightMoveCommand;
            }
            set
            {
                rightMoveCommand = value;
            }
        }

        public void RightMove()
        {
            if (Game.CurrEntity != null)
            {
                int pos = Game.CurrEntity.Pos;
                if (pos >= 0 && pos % Game.Map.Strategie.GetSizeMap() != Game.Map.Strategie.GetSizeMap() - 1)
                    MoveTo(pos + 1);
            }
        }

        public ICommand SpaceKeyCommand
        {
            get
            {
                if (spaceMoveCommand == null)
                    spaceMoveCommand = new RelayCommand(() => SpaceKey());
                return spaceMoveCommand;
            }
            set
            {
                spaceMoveCommand = value;
            }
        }

        public void SpaceKey()
        {
            NextEntity();
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new RelayCommand(() => Save());
                return saveCommand;
            }
            set
            {
                saveCommand = value;
            }
        }

        public void Save()
        {
            ViewModelFileDialog iLoadSaveService = new ViewModelFileDialog();
            iLoadSaveService.Extension = "*.blouin";
            iLoadSaveService.Filter = "Save file (.blouin)|*.blouin";

            iLoadSaveService.SaveCommand.Execute(null);

            Game.Save(iLoadSaveService.SelectedPath,-1);
        }

        public ICommand NextPlayerCommand
        {
            get
            {
                if (nextPlayerCommand == null)
                    nextPlayerCommand = new RelayCommand(() => NextPlayer());
                return nextPlayerCommand;
            }
            set
            {
                nextPlayerCommand = value;
            }
        }

        public void NextPlayer()
        {
            Game.EndMyTurn();
            ReloadMap();
            OnPropertyChanged("CurrentLifeText");
            OnPropertyChanged("CurrentMoveText");
            OnPropertyChanged("CurrentPlayerName");
            OnPropertyChanged("WaitingPlayerName");
            OnPropertyChanged("CurrentPlayerPoint");
            OnPropertyChanged("WaitingPlayerPoint");
            OnPropertyChanged("CurrentPlayerRaceImage");
            OnPropertyChanged("CurrentTurn");
        }

        public ICommand SurrendCommand
        {
            get
            {
                if (surrendCommand == null)
                    surrendCommand = new RelayCommand(() => Surrend());
                return surrendCommand;
            }
            set
            {
                surrendCommand = value;
            }
        }

        public void Surrend()
        {
            if(Game.CurrPlayerNumber == 0 )
            {
                MainRef.ViewResultGame(1,Game);
            }
            else
            { 
                MainRef.ViewResultGame(0,Game);
            }
        }

        public ICommand NextEntityCommand
        {
            get
            {
                if (nextEntityCommand == null)
                    nextEntityCommand = new RelayCommand(() => NextEntity());
                return nextEntityCommand;
            }
            set
            {
                nextEntityCommand = value;
            }
        }

        public void NextEntity()
        {
            Game.Skip();
            ReloadMap();
            OnPropertyChanged("CurrentLifeText");
            OnPropertyChanged("CurrentMoveText");
        }

        #endregion

        #region visualInfo
        public String CurrentLifeText
        {
            get { return Game.CurrEntity == null ? "No more" : "Life pts : " + Game.CurrEntity.LifePoint; }
        }

        public String CurrentMoveText
        {
            get { return Game.CurrEntity == null ? "entity :(" : "Move pts : " + Game.CurrEntity.MovePoint; }
        }

        public String CurrentTurn
        {
            get
            {
                return Game.CurrTurnNumber.ToString();
            }
        }

        #endregion
        
        #endregion
    }
}
