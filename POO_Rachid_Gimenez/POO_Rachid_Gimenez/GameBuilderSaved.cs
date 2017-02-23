using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     * ReadMe check !
     */

    public class GameBuilderSaved : GameBuilder
    {
        public Game Game
        {
            get;
            set;
        }

        public GameBuilderSaved()
        {
            Game = new Game();
        }

        public GameBuilderSaved(String s)
        {
            Game = new Game();
            Load(s);
        }

        // "s" est le chemin du fichier de sauvegarde chargé
        public void Load(String s)
        {
            if (s != null && s != "")
            {
                List<Entity> allEntity = new List<Entity>();
                int playerCount = 0;
                string line = "";
                Player[] tabPlayer = new Player[1];
                using (StreamReader sr = new StreamReader(s))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Equals("GAME_STATE='END'\n"))
                        {
                            throw new ArgumentException();
                        }

                        String patternStrategyName = @"^Strategy_Name='(\w+)'$";
                        String patternGrid = @"(\d+)";
                        String patternSpawn = @"Spawn='(\d+) (\d+)'";
                        String patternPlayer = @"Name='(.+)' Race='(\w+)' VictoryPoint='(\w)'$";
                        String patternEntity = @"Id='(\w+)' Team='(\w+)' Life='(\w+)' Move='(\w+)' Pos='(\w+)'";
                        String patternCurrTurn = @"CURR_TURN_NUMBER='(\d+)' CURR_PLAYER_NUMBER='(\d+)' CURR_ENTITY_ID='(\d+)'";
                        String patternWaitingList = @"Id='(\d+)'";

                        Match m = (new Regex(patternStrategyName, RegexOptions.IgnoreCase)).Match(line);
                        // type : "Strategy_Name='(w)'
                        if (m.Success)
                        {
                            AddStrategy(m.Groups[1].Value);
                            tabPlayer = new Player[Game.NbJoueur];
                        }


                        // type : 0 0 2 0 1 5 0 ...
                        if (line.Equals("<GRID>"))
                        {
                            line = sr.ReadLine();
                            int count = 0;
                            int[] tab = new int[Game.Map.Strategie.GetSizeMap() * Game.Map.Strategie.GetSizeMap()];

                            m = (new Regex(patternGrid, RegexOptions.IgnoreCase)).Match(line);

                            while (m.Success)
                            {
                                tab[count] = Int32.Parse(m.Groups[1].Value);
                                count++;
                                m = m.NextMatch();
                            }
                            line = sr.ReadLine();

                            Game.Map.SetGrid(tab);
                            line = sr.ReadLine();
                        }

                        //Type : Spawn='0 64'
                        m = (new Regex(patternSpawn, RegexOptions.IgnoreCase)).Match(line);
                        if (m.Success)
                        {
                            int nbPlayer = Game.Map.Strategie.GetNbPlayer();
                            int[] tab = new int[nbPlayer];
                            for (int x = 1; x <= nbPlayer; x++)
                            {
                                tab[x - 1] = Int32.Parse(m.Groups[x].Value);
                            }
                            AddSpawn(tab);
                        }

                        //Creation player
                        if (line.Equals("<PLAYER>"))
                        {
                            line = sr.ReadLine();
                            Player p = new Player();
                            m = (new Regex(patternPlayer, RegexOptions.IgnoreCase)).Match(line);
                            if (m.Success)
                            {
                                p = new Player(m.Groups[1].Value, m.Groups[2].Value);
                                p.VictoryPoint = Int32.Parse(m.Groups[3].Value);
                            }
                            line = sr.ReadLine();
                            while (!line.Equals("</PLAYER>"))
                            {
                                m = (new Regex(patternEntity, RegexOptions.IgnoreCase)).Match(line);
                                if (m.Success)
                                {
                                    Entity e = new Entity(Int32.Parse(m.Groups[1].Value), p.RaceString, Int32.Parse(m.Groups[2].Value));
                                    Console.WriteLine("Entity created : " + e.Id);
                                    e.LifePoint = Int32.Parse(m.Groups[3].Value);
                                    e.MovePoint = Int32.Parse(m.Groups[4].Value);
                                    e.Pos = Int32.Parse(m.Groups[5].Value);
                                    p.AddEntity(e);
                                    allEntity.Add(e);
                                    Game.Map.Add(e.Pos, e);
                                }
                                line = sr.ReadLine();
                            }
                            tabPlayer[playerCount] = p;
                            playerCount++;
                        }

                        //Type : Action List
                        if (line.Equals("<HEAL_ACTION>"))
                        {
                            line = sr.ReadLine();
                            m = (new Regex(@"HEAL_ID='(\d+)'")).Match(line);
                            if (m.Success)
                            {
                                foreach (Entity e in allEntity)
                                {
                                    if (Int32.Parse(m.Groups[1].Value) == e.Id)
                                    {
                                        HealAction hAction = new HealAction(e);
                                        AddAction(hAction);
                                    }
                                }
                            }
                            line = sr.ReadLine();
                        }

                        if (line.Equals("<MOVE_ACTION>"))
                        {
                            line = sr.ReadLine();
                            m = (new Regex(@"MOVE_ID='(\d+)' NEW_POS='(\d+)'")).Match(line);
                            if (m.Success)
                            {
                                foreach (Entity e in allEntity)
                                {
                                    if (e.Id == Int32.Parse(m.Groups[1].Value))
                                    {
                                        MoveAction mAction = new MoveAction(e, Game.Map, Int32.Parse(m.Groups[2].Value));
                                        AddAction(mAction);
                                    }
                                }
                            }
                            line = sr.ReadLine();
                        }

                        if (line.Equals("<FIGHT_ACTION>"))
                        {
                            line = sr.ReadLine();
                            m = (new Regex(@"ATK_ID='(\d+)' DEF_ID='(\d+)' Damage='(\d+)'")).Match(line);
                            if (m.Success)
                            {
                                Entity def = new Entity();
                                Entity atk = new Entity();
                                foreach (Entity e in allEntity)
                                {
                                    if (e.Id == Int32.Parse(m.Groups[1].Value))
                                    {
                                        atk = e;
                                    }
                                    if (e.Id == Int32.Parse(m.Groups[2].Value))
                                    {
                                        def = e;
                                    }
                                    FightAction fAction = new FightAction(atk, def, Game.Map, Int32.Parse(m.Groups[3].Value));
                                    AddAction(fAction);
                                }
                            }
                        }

                        //Initialisation du tour courant
                        m = (new Regex(patternCurrTurn, RegexOptions.IgnoreCase)).Match(line);
                        if (m.Success)
                        {
                            Game.CurrTurnNumber = Int32.Parse(m.Groups[1].Value);
                            Game.CurrPlayerNumber = Int32.Parse(m.Groups[2].Value);
                            foreach (Entity e in allEntity)
                            {
                                if (e.Id == Int32.Parse(m.Groups[3].Value))
                                {
                                    Game.CurrEntity = e;
                                }
                            }
                        }
                        //Type : Waiting List, ajout des entités
                        if (line.Equals("<WAITING_LIST>"))
                        {
                            AddPlayer(tabPlayer);
                            line = sr.ReadLine();
                            int x = 0;
                            while (!line.Equals("</WAITING_LIST>"))
                            {
                                m = (new Regex(patternWaitingList, RegexOptions.IgnoreCase)).Match(line);
                                if (m.Success)
                                {
                                    foreach (Entity e in allEntity)
                                    {
                                        if (e.Id == Int32.Parse(m.Groups[1].Value))
                                        {
                                            Game.EntityWaitingList.Insert(x, e);
                                            x++;
                                        }
                                    }
                                }
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                ApplyAction();
            }
        }

        public Game Build()
        {
            return Game;
        }

        public void AddSpawn(int[] tab)
        {
            Game.Map.SetSpawn(tab);
        }

        public void AddPlayer(Player[] p)
        {
            for (int i = 0; i < p.ToArray().Length ; i++)
            {
                
                foreach (Entity e in p[i].EntityList)
                {
                    Console.WriteLine(e.Id + " présent !");
                }Game.AddPlayer(p[i]);
            }
        }

        public void AddStrategy(String s)
        {
            Game.Map.SetStrategy(s);
            Game.NbJoueur=Game.Map.Strategie.GetNbPlayer();
            Game.TurnMax = Game.Map.Strategie.GetNbTurnMax();

        }

        public void AddAction(Action a)
        {
            Game.Action.Insert(Game.Action.Count, a);
        }

        public void ApplyAction()
        {
            for (int i = 0; i < Game.Action.Count; i++)
            {
                Game.Action[i].Execute();
            }
        }
    }
}
