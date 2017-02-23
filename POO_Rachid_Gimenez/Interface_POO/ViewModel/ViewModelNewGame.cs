using System;
using System.Windows.Input;
using POO_Rachid_Gimenez;

namespace Interface_POO
{
    class ViewModelNewGame : ViewModelBase
    {
        #region fields
        private ViewModelMainWindow refMain;
        private GameBuilder gb;
        private ICommand backCommand;
        private ICommand nextCommand;

        public enum MapType
        {
            Demo,
            Small,
            Standard
        }
        private MapType map = MapType.Demo;
        #endregion

        public ViewModelNewGame(ViewModelMainWindow mainWindow)
        {
            this.refMain = mainWindow;
            gb = new GameBuilderUnsaved();
        }

        #region properties

        public MapType Map
        {
            get
            {
                return this.map;
            }
            set
            {
                this.map = value;
                OnPropertyChanged("Map");
                OnPropertyChanged("IsDemo");
                OnPropertyChanged("IsSmall");
                OnPropertyChanged("IsStandard");

            }
        }

        public bool IsDemo
        {
            get
            {
                return this.map == MapType.Demo;
            }
            set
            {
                map = value ? MapType.Demo : map;
            }
        }

        public bool IsSmall
        {
            get
            {
                return this.map == MapType.Small;
            }
            set
            {
                map = value ? MapType.Small : map;
            }
        }

        public bool IsStandard
        {
            get
            {
                return this.map == MapType.Standard;
            }
            set
            {
                map = value ? MapType.Standard : map;
            }
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
            this.refMain.ViewMainPageCommand();
        }

        public ICommand NextCommand
        {
            get
            {
                if (this.nextCommand == null)
                    this.nextCommand = new RelayCommand(() => this.Next());

                return this.nextCommand;
            }
        }

        public void Next()
        {
            if (IsDemo)
            {
                this.gb.AddStrategy("demo");
                this.refMain.ViewSelectPlayerInfoCommand(gb);
            }
            if (IsSmall)
            {
                this.gb.AddStrategy("small");
                this.refMain.ViewSelectPlayerInfoCommand(gb);
            }
            if (IsStandard)
            {
                this.gb.AddStrategy("standard");
                this.refMain.ViewSelectPlayerInfoCommand(gb);
            }
        }

        #endregion
    }
}
