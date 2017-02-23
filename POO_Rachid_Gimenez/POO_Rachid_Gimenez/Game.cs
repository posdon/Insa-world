using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace POO_Rachid_Gimenez
{
    public class Game
    {
        /*
         * Note : Les fonctions avec 0 références seront les fonctions appelées par l'utilisateur
         * 
         * TODO :   EndGame(int winner)
         *
        */

        public Game()
        {
            ListPlayer = new List<Player>();
            Action = new List<Action>();
            EntityWaitingList = new List<Entity>();
            CurrEntity = null;
            Map = new Map();
            CurrPlayerNumber = 0;
            CurrTurnNumber = 0;
            TurnMax = 0;
        }

        public int NbJoueur
        {
            get;
            set;
        }

        public int TurnMax
        {
            get;
            set;
        }

        public int CurrTurnNumber
        {
            get;
            set;
        }

        public Map Map
        {
            get;
            set;
        }

        public int CurrPlayerNumber
        {
            get;
            set;
        }

        public List<Entity> EntityWaitingList
        {
            get;
            set;
        }

        public Entity CurrEntity
        {
            get;
            set;
        }

        public List<POO_Rachid_Gimenez.Player> ListPlayer
        {
            get;
            set;
        }

        public List<POO_Rachid_Gimenez.Action> Action
        {
            get;
            set;
        }

        public bool CanGo(int pos)
        {
            if (CurrEntity != null)
            {
                double dist = Map.GetDistance(CurrEntity, pos);
                Console.WriteLine("Distance :" + dist);
                int init = CurrEntity.Pos;
                int nbSizeMap = this.Map.Strategie.GetSizeMap();
                if (CurrEntity.MovePoint >= dist)
                {
                    if (init % nbSizeMap == 0)
                    {
                        return init + 1 == pos || init - nbSizeMap == pos || init + nbSizeMap == pos;
                    }
                    if (init % nbSizeMap == nbSizeMap - 1)
                    {
                        return init - 1 == pos || init - nbSizeMap == pos || init + nbSizeMap == pos;
                    }
                    if (init / nbSizeMap < 1)
                    {
                        return init - 1 == pos || init + 1 == pos || init + nbSizeMap == pos;
                    }
                    if (init / nbSizeMap > nbSizeMap - 1)
                    {
                        return init - 1 == pos || init + 1 == pos || init - nbSizeMap == pos;
                    }
                    return init - 1 == pos || init + 1 == pos || init - nbSizeMap == pos || init + nbSizeMap == pos;
                }
            }
            return false;
        }

        public int GetActionNumber()
        {
            return Action.Count;
        }

        //If no more player : back to the first one and start a new turn
        public void NextPlayer()
        {
            if (CurrPlayerNumber < NbJoueur - 1)
            {
                CurrPlayerNumber++;
            }
            else
            {
                CurrPlayerNumber = 0;
                CurrTurnNumber++;
            }
            VerifEndGame();
        }

        public void StartGame()
        {
            CurrPlayerNumber = 0;
            CurrTurnNumber = 0;
            for (int i = 0; i < ListPlayer.Count; i++)
            {
                foreach (Entity e in ListPlayer[i].EntityList)
                {
                    e.Pos = Map.SpawnList[i];
                    Map.Add(Map.SpawnList[i], e);
                }
            }
            StartTurn();
        }

        public void StartTurn()
        {
            GetEntityListFromCurrPlayer();
            SetCurrentEntity();
        }

        //Clear EntityWaitingList and Import all Entity From Current Player
        public void GetEntityListFromCurrPlayer()
        {
            List<Entity> tampon = ListPlayer[CurrPlayerNumber].EntityList;
            EntityWaitingList.Clear();
            foreach(Entity e in tampon){
                if (e.Pos != -1)
                {
                    EntityWaitingList.Add(e);
                }
            }
        }

        //Get the first Entity in EntityWaitingList, set it as currentEntity and remove it from EntityWaitingList. If there is no more entity waiting, currEntity=null
        public bool SetCurrentEntity()
        {
            if (EntityWaitingList.Count > 0)
            {
                CurrEntity = EntityWaitingList[0];
                EntityWaitingList.Remove(CurrEntity);
                return true;
            }
            else
            {
                CurrEntity = null;
                return false;
            }
        }

        //Fonction Boolean car dans l'avenir nous pourrons nous déplacer de plusieurs cases et il faudra savoir si nous sommes arriver à la case de fin ou non...
        //Autant mettre le bool dès maintenant pour coder les fonctions l'appelant avec les conditions et vérifications.
        public bool Move(int newPos)
        {
            if (CanGo(newPos)) {
                if (Map.CanGo(CurrEntity, newPos))
                {
                    MoveAction mAction = new MoveAction(CurrEntity, Map, newPos);
                    mAction.Execute();
                    Action.Insert(Action.Count, mAction);
                    return true;
                }
                else
                {
                    //int bestPos = /* Conseillée position */0;
                    //while(!Map.CanGo(CurrEntity,bestPos)){
                    //    bestPos = /*Conseillé position again */ 0;
                    //}
                    //MoveAction mAction = new MoveAction(CurrEntity, Map, bestPos);
                    //mAction.Execute();
                    // Action.Add(mAction);


                    FightAction fAction = new FightAction(CurrEntity, Map.GetBestDefenser(newPos), Map);
                    if (fAction.Execute())
                    {
                        Action.Insert(Action.Count, fAction);
                        MoveAction mAction = new MoveAction(CurrEntity, Map, newPos);
                        mAction.Execute();
                        Action.Insert(Action.Count, mAction);
                        VerifEndGame();
                        return true;
                    }
                    else
                    {
                        Action.Insert(Action.Count, fAction);
                        VerifEndGame();
                        return true;
                    }
                }
            }
            return false;
        }

        //Skip the turn of one unit
        public bool Skip()
        {
            if (CurrEntity != null)
            {
                if (CurrEntity.MovePoint == CurrEntity.Race.GetMovePointMax())
                {
                    HealAction hAction = new HealAction(CurrEntity);
                    if (hAction.Execute()) 
                    {
                        Action.Insert(Action.Count, hAction);
                    }
                }
                CurrEntity.ResetTurn();
            }
            return SetCurrentEntity();
        }

        //Return the number of the winner, -1 if no winner
        public int VerifEndGame()
        {
            //Case : No more turn
            if (CurrTurnNumber == TurnMax)
            {
                int winner = -1;
                int score = 0;
                foreach (Player p in ListPlayer)
                {
                    Map.SetVictoryPoint(p);
                    if (p.VictoryPoint > score)
                    {
                        score = p.VictoryPoint;
                        winner = ListPlayer.IndexOf(p);
                    }
                }
                if (winner != -1)
                {
                    EndGame(winner);
                    return winner;
                }
            }
            //Case : Player has no more unit
            int playerAlive = -1;
            bool verif = true;
            for(int i=0;i<ListPlayer.Count;i++)
            {
                foreach (Entity e in ListPlayer[i].EntityList)
                {
                    if (e.Pos != -1)
                    {
                        if (playerAlive == -1)
                        {
                            playerAlive = i;
                        }
                        else if(playerAlive != i)
                        {
                            verif = false;
                        }
                    }
                }
            }
            if (verif == true && playerAlive != -1)
            {
                EndGame(playerAlive);
                return playerAlive;
            }
            return -1;
        }

        //End of the game, Player[i] is winner
        public void EndGame(int i)
        {
            //Propose de Save
            //throw new NotImplementedException();
        }

        //Skip the turn of all entities waiting, start the next player turn. Might start a new turn.
        public void EndMyTurn()
        {
            while (CurrEntity != null)
            {
                Skip();
            }
            Map.SetVictoryPoint(ListPlayer[CurrPlayerNumber]);
            NextPlayer();
            StartTurn();
        }

        public void Save(String nameFile, int isEnd)
        {
            if (nameFile != null)
            {
                

                // Write the string array to a new file named "WriteLines.txt".
                using (StreamWriter outputFile = new StreamWriter(nameFile))
                {

                    //GAME_STATE = RUNNING
                    if ((isEnd!=-1))
                    {
                        outputFile.WriteLine("GAME_STATE='END'");
                    }
                    else
                    {
                        outputFile.WriteLine("GAME_STATE='RUNNING'");
                    }

                    //Initialisation part

                    string[] linesMap = Map.Save();
                    foreach (string line in linesMap)
                    {
                        outputFile.WriteLine(line);
                    }
                    foreach (Player p in ListPlayer)
                    {
                        string[] lines = p.Save(isEnd);
                        foreach (string line in lines)
                        {
                            outputFile.WriteLine(line);
                        }
                    }
                    for (int i = 0; i < Action.Count; i++)
                    {
                        string[] lines = Action[i].Save();
                        foreach (string line in lines)
                        {
                            outputFile.WriteLine(line);
                        }
                    }

                    if (isEnd!=-1)
                    {
                        outputFile.WriteLine("WINNER='" + isEnd + "'");
                        outputFile.WriteLine("<WAITING_LIST>");
                        outputFile.WriteLine("</WAITING_LIST>");
                    }
                    else
                    {
                        //Current part
                        outputFile.WriteLine("CURR_TURN_NUMBER='" + CurrTurnNumber + "' CURR_PLAYER_NUMBER='" + CurrPlayerNumber + "' CURR_ENTITY_ID='" + CurrEntity.Id + "'");
                        outputFile.WriteLine("<WAITING_LIST>");
                        foreach (Entity e in EntityWaitingList)
                        {
                            outputFile.WriteLine("Id='" + e.Id + "'");
                        }
                        outputFile.WriteLine("</WAITING_LIST>");
                    }
                }
            }
        }

        public bool AddPlayer(Player p)
        {
            if (ListPlayer.Count < NbJoueur)
            {
                foreach (Player i in ListPlayer)
                {
                    if (i.RaceString.Equals(p.RaceString))
                    {
                        return false;
                    }
                }
                foreach (Entity ent in p.EntityList)
                {
                    ent.Pos = Map.SpawnList[ListPlayer.Count];
                }
                ListPlayer.Add(p);
                return true; 
            }
            return false;
        }
    }
}
