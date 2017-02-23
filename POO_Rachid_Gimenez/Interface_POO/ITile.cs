using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using POO_Rachid_Gimenez;

namespace Interface_POO
{
    class ITile : ViewModelBase
    {
        #region fields
        private int id;
        private ViewModelGame viewRef;
        private String displayTileImage;
        private int nbEntityOn;
        private int onClickCommandMode;
        private ICommand mouseOnCommand;
        private Boolean haveEntityOn;
        #endregion
        
       
        public ITile(ViewModelGame view, int num, String path)
        {
            viewRef = view;
            id = num;
            onClickCommandMode = 0;
            displayTileImage = path;
            nbEntityOn = 0;
        }

        #region properties

        #region command
        

        public int OnClickCommandMode { get { return onClickCommandMode; } set { onClickCommandMode = value; OnPropertyChanged("OnClickCommandMode"); OnPropertyChanged("OnClickCommand"); } }
        
        public ICommand OnClickCommand
        {
            get
            {
                if (onClickCommandMode == 1)
                    return new RelayCommand(() => Move());
                return new RelayCommand(() => Nothing());
            }
        }

        public void Move()
        {
            ((ViewModelGamePlay)viewRef).MoveTo(id);
        }

        public void Nothing()
        {
        }
        #endregion

        public int Id { get { return id; } set { id = value; } }

        public Boolean HaveEntityOn
        {
            get
            {
                return haveEntityOn;
            }
            set
            {
                haveEntityOn = value;
                OnPropertyChanged("HaveEntityOn");
            }
        }

        public String BestDefDescrp
        {
            get
            {
                String result = "";
                if (NbEntityOn != 0) {
                    Game g = viewRef.Game;
                    if (g != null)
                    {
                        Entity e = g.Map.GetBestDefenser(id);
                        if (e != null)
                        {
                            result += "Life point : " + e.LifePoint + "\n";
                            result += "Move point : " + e.MovePoint + "\n";
                            result += "Atk point : " + e.Race.GetAtkPoint() * e.LifePoint / e.Race.GetLifePoint() + "\n";
                            result += "Def point : " + e.Race.GetDefPoint() * e.LifePoint / e.Race.GetLifePoint() + "\n";
                            result += "Victory point : " + e.Race.GetVictoryPoint(viewRef.Game.Map.GetTile(id)) + "\n";
                        }
                    }
                }
                return result;
            }
        }

        public String DisplayTileImage
        {
            get
            {
                return displayTileImage;
            }
            set
            {
                displayTileImage = value;
                OnPropertyChanged("DisplayTileImage");
            }
        }

        public int NbEntityOn
        {
            get
            {
                return nbEntityOn;
            }
            set
            {
                nbEntityOn = value;
                haveEntityOn = (nbEntityOn != 0) ? true : false;
                OnPropertyChanged("NbEntityOn");
            }
        }

        public bool NbIsVisible
        {
            get
            {
                return nbEntityOn > 1;
            }
        }

        #endregion
    }
}
