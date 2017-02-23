using System;
using System.Collections.Generic;
using System.Windows.Input;
using POO_Rachid_Gimenez;
using System.Collections.ObjectModel;
using System.Threading;

namespace Interface_POO
{
    class ViewModelReplayGame : ViewModelGame
    {
        #region fields
        private ICommand playAndPauseCommand;
        private ICommand nextCommand;
        private Boolean isPlaying;
        private ReplayAuxThread objThread;
        private Thread auxThread;
        #endregion

        public ViewModelReplayGame(Game g, ViewModelMainWindow mainWindow):base(g,mainWindow)
        {
            objThread = new ReplayAuxThread(this);
            auxThread = new Thread(objThread.DoWork);
            IsPlaying = false;
            ReloadMap();
            OnPropertyChanged("PlayOrPause");
        }

        #region properties
        public String PlayOrPause
        {
            get
            {
                return (IsPlaying) ? "Play" : "Pause";
            }
        }

        public Boolean IsPlaying
        {
            get
            {
                return isPlaying;
            }
            set
            {
                isPlaying = value;
                OnPropertyChanged("PlayOrPause");
            }
        }

        #region command
        public ICommand PlayAndPauseCommand
        {
            get
            {
                if(playAndPauseCommand==null)
                    playAndPauseCommand = new RelayCommand(() => Play());
                return playAndPauseCommand;
            }
            set
            {
                playAndPauseCommand = value;
                OnPropertyChanged("PlayAndPauseCommand");
                OnPropertyChanged("PlayOrPause");
            }
        }

        public void Play()
        {
            IsPlaying = true;
            PlayAndPauseCommand = new RelayCommand(() => Pause());
            objThread.RequestGo();
            if (!auxThread.IsAlive)
            {
                auxThread = new Thread(objThread.DoWork);
                auxThread.Start();
            }
            while (!auxThread.IsAlive) ;
            Thread.Sleep(1);
        }

        public void Pause()
        {
            IsPlaying = false;
            PlayAndPauseCommand = new RelayCommand(() => Play());
            objThread.RequestStop();
        }
       
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                    nextCommand = new RelayCommand(() => Next());
                return nextCommand;
            }
            set
            {
                nextCommand = value;
            }
        }

        public void Next()
        {
            GameReplay gReplay = ((GameReplay)game);
            if (gReplay.Action[gReplay.NbAction].GetType() == typeof(FightAction))
            {
                FightAction actionf = (FightAction)gReplay.Action[gReplay.NbAction];
                FightingBox = "Dégâts reçus : " + actionf.Damage;
                OnPropertyChanged("FightingBox");
            }
            gReplay.NextAction();
            ReloadMap();
        }
        #endregion
        #endregion
    }
}
