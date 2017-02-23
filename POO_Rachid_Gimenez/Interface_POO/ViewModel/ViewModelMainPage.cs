using System;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;

namespace Interface_POO
{
    class ViewModelMainPage : ViewModelBase
    {
        #region fields
        private ViewModelMainWindow refMain;
        private ICommand newGameCommand;
        private ICommand loadGameCommand;
        private ICommand replayGameCommand;
        private ICommand quitCommand;
        #endregion

        public ViewModelMainPage(ViewModelMainWindow mainWindow)
        {
            this.refMain = mainWindow;
        }

        #region properties
        public ICommand NewGameCommand
        {
            get
            {
                if (this.newGameCommand == null)
                    this.newGameCommand = new RelayCommand(() => this.NewGame());

                return this.newGameCommand;
            }
        }

        public void NewGame()
        {
            this.refMain.ViewNewGameCommand();
        }

        public ICommand LoadGameCommand
        {
            get
            {
                if (this.loadGameCommand == null)
                    this.loadGameCommand = new RelayCommand(() => this.LoadGame());

                return this.loadGameCommand;
            }
        }

        public void LoadGame()
        {
            ViewModelFileDialog iLoadSaveService = new ViewModelFileDialog();
            iLoadSaveService.Extension = "*.blouin";
            iLoadSaveService.Filter = "Save file (.blouin)|*.blouin";

            iLoadSaveService.OpenCommand.Execute(null);

            if (iLoadSaveService.SelectedPath != null && iLoadSaveService.SelectedPath!="")
                refMain.ViewLoadCommand(iLoadSaveService.SelectedPath);
            
        }

        public ICommand ReplayGameCommand
        {
            get
            {
                if (this.replayGameCommand == null)
                    this.replayGameCommand = new RelayCommand(() => this.ReplayGame());

                return this.replayGameCommand;
            }
        }

        public void ReplayGame()
        {
            ViewModelFileDialog iLoadSaveService = new ViewModelFileDialog();
            iLoadSaveService.Extension = "*.pazat";
            iLoadSaveService.Filter = "Replay file (.pazat)|*.pazat";

            iLoadSaveService.OpenCommand.Execute(null);

            if (iLoadSaveService.SelectedPath != null && iLoadSaveService.SelectedPath != "")
                refMain.ViewReplayCommand(iLoadSaveService.SelectedPath);
            
        }

        public ICommand QuitCommand
        {
            get
            {
                if (this.quitCommand == null)
                    this.quitCommand = new RelayCommand(() => this.Quit());

                return this.quitCommand;
            }
        }

        public void Quit()
        {
            this.refMain.CloseApplication();
        }
        #endregion
    }
}