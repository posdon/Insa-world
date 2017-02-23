using System;
using System.IO;
using System.Windows.Input;
using POO_Rachid_Gimenez;

namespace Interface_POO
{
    class ViewModelResultGame : ViewModelBase
    {
        private ICommand menuCommand;
        private ICommand saveCommand;
        private ViewModelMainWindow refMain;
        private String winMess;
        private Game endGame;
        private int winnerNb;


        public ViewModelResultGame(ViewModelMainWindow mainWindow, int winner, Game g)
        {
            this.refMain = mainWindow;
            winMess = "Winner : " + g.ListPlayer[winner].Name;
            this.endGame = g;
            this.winnerNb = winner;
        }

        public String WinMessage
        {
            get
            {
                return winMess;
            }
            set
            {
                winMess = value;
                OnPropertyChanged("WinMessage");
            }
        }

        public ICommand MenuCommand
        {
            get
            {
                if (menuCommand == null)
                    menuCommand = new RelayCommand(() => Menu());
                return menuCommand;
            }
            set
            {
                menuCommand = value;
            }
        }

        public void Menu()
        {
            refMain.ViewMainPageCommand();
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
            iLoadSaveService.Extension = "*.pazat";
            iLoadSaveService.Filter = "Replay file (.pazat)|*.pazat";

            iLoadSaveService.SaveCommand.Execute(null);
            endGame.Save(iLoadSaveService.SelectedPath,winnerNb);
        }
    }
}
