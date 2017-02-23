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

    public class GameBuilderUnsaved : GameBuilder
    {
        public Game Game
        {
            get;
            set;
        }

        public GameBuilderUnsaved()
        {
            Game = new Game();
        }

        public Game Build()
        {
            Game.StartGame();
            return Game;
        }

        public void AddPlayer(Player[] p)
        {
            int nbUnit = Game.Map.Strategie.GetUnitPerPlayer();
            for (int i = 0; i < p.ToArray().Length; i++)
            {
                int[] id = new int[nbUnit];
                for (int j = 0; j < id.Length; j++)
                {
                    id[j] = Game.ListPlayer.Count *nbUnit + j;
                }
                p[i].CreateEntity(id, Game.ListPlayer.Count);
                Game.AddPlayer(p[i]);
            }
        }

        public void AddStrategy(String s)
        {
            Game.Map.SetStrategy(s);
            Game.NbJoueur = Game.Map.Strategie.GetNbPlayer();
            Game.TurnMax = Game.Map.Strategie.GetNbTurnMax();
            Game.Map.GenerateRandom();
        }

        public void AddAction(Action a)
        {
               Game.Action.Insert(Game.Action.Count, a);
        }

        public void ApplyAction()
        {
            
        }
    }
}
