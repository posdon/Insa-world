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
    public class FightAction : Action
    {

        //Constructeurs nouvelle action
        //Damage par défaut : atkE.Atk+defE.Def+1
        public FightAction(Entity atkE, Entity defE, Map map)
        {
            EntityAtk = atkE;
            EntityDef = defE;
            Map = map;
            Damage = EntityAtk.Race.GetAtkPoint()+EntityDef.Race.GetDefPoint()+1;
        }

        //Constructeurs action sauvegardée
        public FightAction(Entity atkE, Entity defE, Map map,int damage)
        {
            EntityAtk = atkE;
            EntityDef = defE;
            Map = map;
            Damage = damage;
        }

        public Entity EntityAtk
        {
            get;
            set;
        }

        public Entity EntityDef
        {
            get;
            set;
        }

        public Map Map
        {
            get;
            set;
        }

        public int Damage
        {
            get;
            set;
        }
      
        // Retourne si l'attaquant peut aller sur la position du défenseur
        public bool Execute()
        {
            EntityAtk.Attack(Map);
            if (Damage != int.MaxValue)
            {
                // Cas action sauvegardée
                if (Damage > 0)
                {
                    // Cas Defenseur qui perd
                    EntityDef.Damage(Damage);

                    if (EntityDef.IsDead())
                    {
                        int posDef = EntityDef.Pos;
                        Map.Remove(posDef, EntityDef);
                        return Map.CanGo(EntityAtk, posDef);
                    }
                }
                else
                {
                    // Cas Attaquant qui perd
                    EntityAtk.Damage(-Damage);
                    if (EntityAtk.IsDead())
                    {
                        Map.Remove(EntityAtk.Pos, EntityAtk);
                    }
                }
                return false;
            }
            else
            {
                // Cas nouvelle action
                // Calcul des dégats
                Damage = EntityAtk.Confrontation(EntityDef);
                if (Damage > 0)
                {
                    // Cas défenseur qui perd
                    EntityDef.Damage(Damage);
                    if (EntityDef.IsDead())
                    {
                        int posDef = EntityDef.Pos;
                        Map.Remove(EntityDef.Pos, EntityDef);
                        return Map.CanGo(EntityDef, posDef);
                    }
                }
                else
                {
                    // Cas attaquant qui perd
                    EntityAtk.Damage(-Damage);
                    if (EntityAtk.IsDead())
                    {
                        Map.Remove(EntityAtk.Pos, EntityAtk);
                    }
                }
                return false;
            }
        }

        // Forme :
        // <FIGHT_ACTION>
        // ATK_ID='...' DEF_ID='...' Damage='...'
        // </FIGHT_ACTION>
        public string[] Save()
        {
            string[] result = new string[3];
            result[0]= "<FIGHT_ACTION>";
            result[1]= "ATK_ID='" + EntityAtk.Id + "' DEF_ID='" + EntityDef.Id + "' Damage='" + Damage + "'";
            result[2]="</FIGHT_ACTION>";
            return result;
        }
        
    }
}
