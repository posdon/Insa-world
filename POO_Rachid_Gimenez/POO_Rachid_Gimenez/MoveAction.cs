using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_Rachid_Gimenez
{
    public class MoveAction : Action
    {
        /*
         * Classe Check !
         * ReadMe check !
         */

        //Constructeur qui prend l'entité à bouger, la map sur laquelle la déplacer et la nouvelle position.
        public MoveAction(Entity currE, Map map, int newPos)
        {
            Entity = currE;
            NewPos = newPos;
            Map=map;
        }

        public Map Map
        {
            get;
            set;
        }

        public int NewPos
        {
            get;
            set;
        }

        public Entity Entity
        {
            get;
            set;
        }

        //Déplace l'unité jusqu'à la nouvelle position.
        public bool Execute()
        {
            int pos = Entity.Pos;
            //Enlève, bouge et ajoute
            Map.Remove(pos, Entity);
            Entity.Pos = pos;
            Entity.Move(NewPos, Map);
            return Map.Add(NewPos, Entity);
        }

        //Forme :
        // <MOVE_ACTION>
        // MOVE_ID='???' NEW_POS='???'
        // </MOVE_ACTION>
        public string[] Save()
        {
            string[] result = new string[3];
            result[0]= "<MOVE_ACTION>";
            result[1]= "MOVE_ID='" + Entity.Id + "' NEW_POS='" + NewPos + "'";
            result[2]= "</MOVE_ACTION>";
            return result;
        }
    }
}
