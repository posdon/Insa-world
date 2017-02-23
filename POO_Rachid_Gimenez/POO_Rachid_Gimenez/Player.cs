using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace POO_Rachid_Gimenez
{
    /*
    * Classe check !
    * ReadMe check !
    */
    public class Player
    {
        //Constructeur par défaut : Strings = "", VictoryPoint = 0, List is empty
        public Player()
        {
            Name = "";
            RaceString = "";
            VictoryPoint = 0;
            EntityList = new List<Entity>();
        }

        //Constructeur basique : List is empty, VictoryPoint = 0
        public Player(String NameP, String RaceP)
        {
            Name = NameP;
            RaceString = RaceP;
            VictoryPoint = 0;
            EntityList = new List<Entity>();
        }
        
        public String RaceString
        {
            get;
            set;
        }

        public int VictoryPoint
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public List<Entity> EntityList
        {
            get;
            set;
        }

        public void AddEntity(Entity entity)
        {
            EntityList.Add(entity);        
        }

        public void CreateEntity(int[] id, int team){
            for (int i = 0; i < id.Length; i++)
            {
                EntityList.Add(new Entity(id[i], RaceString, team));
            }
        }

        public int GetNbEntityOn(int pos)
        {
            int result = 0;
            foreach (Entity e in this.EntityList)
            {
                if(e.Pos==pos)
                    result++;
            }
            return result;
        }

        //Forme :
        // <PLAYER>
        // Name='???' Race='???' VictoryPoint='???'
        // <ENTITY_LIST>
        // ...
        // </ENTITY_LIST>
        // </PLAYER>
        public string[] Save(int isEnd)
        {
            int size = 3 * EntityList.Count + 5;
            string[] result = new string[size];
            result[0]= "<PLAYER>";
            result[1] = (isEnd == -1) ? "Name='" + Name + "' Race='" + RaceString + "' VictoryPoint='" + VictoryPoint.ToString() + "'" : "Name='" + Name + "' Race='" + RaceString + "' VictoryPoint='0'";
            result[2]= "<ENTITY_LIST>";
            for(int i = 0; i < EntityList.Count; i++)
            {
                string[] save = EntityList[i].Save(isEnd);
                result[3 + i * 3] = save[0];
                result[4 + i * 3] = save[1];
                result[5 + i * 3] = save[2];
            }
            result[size-2]= "</ENTITY_LIST>";
            result[size-1]= "</PLAYER>";
            return result;
        }
    }
}
