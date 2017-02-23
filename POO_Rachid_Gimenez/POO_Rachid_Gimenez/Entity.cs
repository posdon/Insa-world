using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     * ReadMe check !
     */

    public class Entity
    {
        //Constructeur par défaut :
        //Tout = -1 && Race = RaceImpl;
        public Entity()
        {
            Team = -1;
            Race = new RaceImpl();
            Id = -1;
            MovePoint = -1;
            LifePoint = -1;
            Pos = -1;
        }

        //Constructeur normal : Pos = -1
        public Entity(int id, string raceName, int team)
        {
            RaceFactory rFactory = new RaceFactory();
            RaceImpl race = rFactory.GetRace(raceName);
            Team = team;
            Race = race;
            Id = id;
            MovePoint = race.GetMovePointMax();
            LifePoint = race.GetLifePoint();
            Pos = -1;
        }

        public int Team
        {
            get;
            set;
        }

        public double MovePoint
        {
            get;
            set;
        }

        public int LifePoint
        {
            get;
            set;
        }

        public Race Race
        {
            get;
            set;
        }

        public int Pos
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public bool IsDead()
        {
            return LifePoint <= 0;
        }

        public void ResetTurn()
        {
            MovePoint = Race.GetMovePointMax();
        }

        public int Confrontation(Entity ennemy)
        {
            int atkValue = (int)(this.Race.GetAtkPoint() * this.LifePoint / this.Race.GetLifePoint());
            int defValue = (int)(ennemy.Race.GetDefPoint() * ennemy.LifePoint / ennemy.Race.GetLifePoint());
            Random rand = new Random();
            int atkDamage = rand.Next(0, atkValue + 1);
            Console.WriteLine("AtkRand = " + atkDamage);
            int defDamage = rand.Next(0, defValue + 1);
            Console.WriteLine("DefRand = " + defDamage);
            return atkDamage - defDamage;
        }


        // Fonctions à action

        //Retourne si l'unité meurt
        public bool Damage(int dmg)
        {
            LifePoint -= dmg;
            return IsDead();
        }

        //Retourne si l'unité a bien récupéré
        public bool Regenerate()
        {
            if (LifePoint < Race.GetLifePoint())
            {
                LifePoint++;
                return true;
            }
            return false;
        }

        //Décompte le coût de déplacement et set la nouvelle position
        public void Move(int newPos, Map map)
        {
            MovePoint -= Race.GetMovePointCost(map.GetTile(Pos));
            Pos = newPos;
        }

        public void Attack(Map map)
        {
            MovePoint -= Race.GetMovePointCost(map.GetTile(Pos));
        }

        // Redéfini Equals : Comparaison selon id
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Entity p = (Entity)obj;
            return Id == p.Id;
        }

        //Redéfini HashCode selon id
        public override int GetHashCode()
        {
            return Id;
        }


        //Forme :
        // <ENTITY>
        // Id='???' Team='???' Life='???' Move='???' Pos='???'
        // </ENTITY>
        public string[] Save(int isEnd)
        {
            string[] result = new string[3];
            result[0]= "<ENTITY>";
            if (LifePoint < 0)
                LifePoint = 0;
            result[1]= (isEnd==-1)?"Id='"+Id+"' Team='"+Team+"' Life='"+LifePoint+"' Move='"+MovePoint+"' Pos='"+Pos+"'":"Id='"+Id+"' Team='"+Team+"' Life='0' Move='0' Pos='0'";
            result[2]= "</ENTITY>";
            return result;
        }
    }
}
