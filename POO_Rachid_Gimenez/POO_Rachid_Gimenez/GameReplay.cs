using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_Rachid_Gimenez
{
    public class GameReplay : Game
    {
        public GameReplay()
            : base()
        {
            NbAction = 0;
        }

        public int NbAction
        {
            get;
            set;
        }

        public void NextAction()
        {
            if (Action.Count > NbAction)
            {
                Action[NbAction].Execute();
                NbAction++;
            }
        }
    }
}
