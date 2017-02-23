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
    public class RaceFactory
    {
        public RaceFactory()
        {
        }

        public RaceImpl GetRace(String nameRace)
        {
            if(nameRace.Equals("cerberus")){
                return CreerCerberus();
            }
            if (nameRace.Equals("centaur"))
            {
                return new Centaur();
            }
            if (nameRace.Equals("cyclops"))
            {
                return CreerCyclops();
            }
            return new RaceImpl();
        }

        public Centaur CreerCentaur()
        {
            return new Centaur();
        }

        public Cyclops CreerCyclops()
        {
            return new Cyclops();
        }

        public Cerberus CreerCerberus()
        {
            return new Cerberus();
        }
    }
}
