using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POO_Rachid_Gimenez
{
    /*
     * Classe check !
     */
    public enum TileType{
	    Desert = 0,
	    Plain = 1,
	    Swamp = 2,
	    Volcano = 3
    };

    public class Dll : IDisposable
    {
        bool disposed = false;
        IntPtr nativeDll;

        public TileType[] CreateGrid(int nbTiles)
        {
            var tiles = new TileType[nbTiles];
            Dll_fillMap(nativeDll, tiles, nbTiles);
            return tiles;
        }

        public int[] CreateSpawn(int nbTiles, int nbPlayer)
        {
            int[] spawn = new int[nbPlayer];
            Dll_fillSpawn(nativeDll, spawn, nbTiles, nbPlayer);
            return spawn;
        }

        public int[] GetBestMove(Entity e, int[] validGrid ,List<Tile> grid)
        {
            int[] result = new int[3];
            Race r = e.Race;
            TileType[] tiles = new TileType[grid.Count];
            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].Equals(TileFactory.INSTANCE.TileDesert))
                {
                    tiles[i] = TileType.Desert;
                }
                if (grid[i].Equals(TileFactory.INSTANCE.TilePlain))
                {
                    tiles[i] = TileType.Plain;
                }
                if (grid[i].Equals(TileFactory.INSTANCE.TileSwamp))
                {
                    tiles[i] = TileType.Swamp;
                }
                if (grid[i].Equals(TileFactory.INSTANCE.TileVolcano))
                {
                    tiles[i] = TileType.Volcano;
                }
            }
            int[] moveWin = new int[4];
            for(int i=0 ; i<r.GetMoveCost().Count ; i++)
            {
                moveWin[0] = r.GetVictoryPoint(TileFactory.INSTANCE.TileDesert);
                moveWin[1] = r.GetVictoryPoint(TileFactory.INSTANCE.TilePlain);
                moveWin[2] = r.GetVictoryPoint(TileFactory.INSTANCE.TileSwamp);
                moveWin[3] = r.GetVictoryPoint(TileFactory.INSTANCE.TileVolcano);
            }
            Dll_bestPosition(nativeDll, tiles, moveWin, validGrid, result);
            return result;
        }

        public Dll()
        {
            nativeDll = Dll_new();
        }

        ~Dll()
        {
            Dispose(false);
            Dll_delete(nativeDll);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                Dll_delete(nativeDll);
            }
            disposed = true;
        }
        

        [DllImport("MapDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static void Dll_fillMap(IntPtr dll, TileType[] tiles, int nbTiles);

        [DllImport("MapDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static void Dll_fillSpawn(IntPtr dll, int[] spawn, int nbTiles, int nbPlayer);

        [DllImport("MapDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static double Dll_bestPosition(IntPtr dll, TileType[] map, int[] winGrid, int[] validPosition, int[] bestPos);

        [DllImport("MapDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr Dll_new();

        [DllImport("MapDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr Dll_delete(IntPtr dll);
    }
}
