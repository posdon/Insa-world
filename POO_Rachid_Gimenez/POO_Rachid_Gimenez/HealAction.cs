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
    public class HealAction : Action
    {
        public HealAction(Entity currE)
        {
            Entity = currE;
        }

        public Entity Entity
        {
            get;
            set;
        }

        // Régénère l'entité, retourne si l'unité c'est régénérée
        public bool Execute()
        {
            return Entity.Regenerate();
        }

        //Forme :
        // <HEAL_ACTION>
        // HEAL_ID='???'
        // </HEAL_ACTION>
        public string[] Save()
        {
            string[] result = new string[3];
            result[0]="<HEAL_ACTION>";
            result[1]= "HEAL_ID='" + Entity.Id + "'";
            result[2]= "</HEAL_ACTION>";
            return result;
        }
        
    }
}
