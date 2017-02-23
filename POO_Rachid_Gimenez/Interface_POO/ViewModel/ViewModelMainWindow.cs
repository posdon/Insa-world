using System;
using System.Windows.Input;
using POO_Rachid_Gimenez;

namespace Interface_POO
{
        class ViewModelMainWindow : ViewModelBase
        {
            public ViewModelMainWindow()
            {
                CurrentView = new ViewModelMainPage(this);
            }
 
            public ViewModelBase CurrentView { get; set; }

            public void ViewNewGameCommand()
            {
                CurrentView = new ViewModelNewGame(this);
                OnPropertyChanged("CurrentView");
            }

            public void CloseApplication()
            {
                Environment.Exit(0);
            }

            public void ViewMainPageCommand()
            {
                CurrentView = new ViewModelMainPage(this);
                OnPropertyChanged("CurrentView");
            }

            public void ViewSelectPlayerInfoCommand(GameBuilder gb)
            {
                CurrentView = new ViewModelSelectPlayerInfo(this, gb);
                OnPropertyChanged("CurrentView");
            }

            public void ViewGameCommand(Game game)
            {
                CurrentView = new ViewModelGamePlay(game, this);
                OnPropertyChanged("CurrentView");
            }

            public void ViewLoadCommand(String path)
            {
                GameBuilderSaved gb = new GameBuilderSaved();
                gb.Load(path);
                CurrentView = new ViewModelGamePlay(gb.Build(), this);
                OnPropertyChanged("CurrentView");
            }

            public void ViewResultGame(int i, Game g)
            {
                CurrentView = new ViewModelResultGame(this,i,g);
                OnPropertyChanged("CurrentView");
            }

            public void ViewReplayCommand(String path)
            {
                GameBuilderReplay gb = new GameBuilderReplay(path);
                CurrentView = new ViewModelReplayGame(gb.Build(), this);
                OnPropertyChanged("CurrentView");
            }
        }
}
