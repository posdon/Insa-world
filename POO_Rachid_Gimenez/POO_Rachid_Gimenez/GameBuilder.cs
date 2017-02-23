using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     * ReadMe check !
     */
    public interface GameBuilder
    {
        Game Build();

        void AddPlayer(Player[] p);

        void AddStrategy(String s);

        void AddAction(Action a);

        void ApplyAction();

    }
}
