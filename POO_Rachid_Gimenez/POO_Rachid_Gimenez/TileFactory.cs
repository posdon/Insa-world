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
    public class TileFactory
    {

        public static TileFactory INSTANCE = new TileFactory();

        public Desert TileDesert
        {
            get;
            set;
        }
        public Volcano TileVolcano
        {
            get;
            set;
        }
        public Plain TilePlain
        {
            get;
            set;
        }
        public Swamp TileSwamp
        {
            get;
            set;
        }
        //Constructeur
        private TileFactory()
        {
            TileDesert = new Desert();
            TilePlain = new Plain();
            TileSwamp = new Swamp();
            TileVolcano = new Volcano();
        }
    }
}
