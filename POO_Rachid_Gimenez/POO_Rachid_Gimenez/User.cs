using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_Rachid_Gimenez
{
    class User
    {
        public Game Game{
            get;
            set;
        }

        public GameBuilder GameBuilder{
            get;
            set;
        }

        public User()
        {
        }

        public void LoadGame()
        {
            GameBuilder = new GameBuilderSaved();
        }

        public void NewGame()
        {
            GameBuilder = new GameBuilderUnsaved();
        }

        public void ReplayGame()
        {
            GameBuilder = new GameBuilderReplay();
        }

    }
}
