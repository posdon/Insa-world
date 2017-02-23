using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class SizeMapTest
    {
        [TestMethod]
        public void DemoMapTest()
        {
            //tester l'initialisation de la map
            Map map = new Map("demo");
            Assert.AreEqual(map.Strategie.GetType(),typeof(StrategieMapDemo));
            Assert.AreEqual(map.Strategie.GetNbPlayer(), 2);
            Assert.AreEqual(map.Strategie.GetSizeMap(), 6);
            Assert.AreEqual(map.Strategie.GetNbTurnMax(), 5);
            Assert.AreEqual(map.Strategie.GetUnitPerPlayer(), 4);
            //tester si on a exactement 4 Tile dans la map 
            //Assert.AreEqual(map.TileList.Count, 4);
            Assert.AreEqual(map.TileList.Count, 36);
            //tester si les 4 Tile de la map sont differents les uns des autres
            /*for (int i = 0; i < map.TileList.Count; i++)
            {
                for (int j = 0; j < map.TileList.Count; j++)
                {
                    if (i != j) Assert.AreNotEqual(map.TileList[i].GetType(), map.TileList[j].GetType());
                }
            }*/
            Assert.IsTrue(map.TileList.Contains(TileFactory.INSTANCE.TileDesert));
            Assert.IsTrue(map.TileList.Contains(TileFactory.INSTANCE.TilePlain));
            Assert.IsTrue(map.TileList.Contains(TileFactory.INSTANCE.TileSwamp));
            Assert.IsTrue(map.TileList.Contains(TileFactory.INSTANCE.TileVolcano));
            Assert.AreEqual(map.Strategie.Save(),"Strategy_Name='demo'\n");
        }
        [TestMethod]
        public void SmallMapTest()
        {
            //tester l'initialisation de la map
            Map map = new Map("small");
            Assert.AreEqual(map.Strategie.GetType(), typeof(StrategieMapSmall));
            Assert.AreEqual(map.Strategie.GetNbPlayer(),2);
            Assert.AreEqual(map.Strategie.GetSizeMap(), 10);
            Assert.AreEqual(map.Strategie.GetNbTurnMax(), 20);
            Assert.AreEqual(map.Strategie.GetUnitPerPlayer(), 6);
            Assert.AreEqual(map.TileList.Count, 100);
            //tester si les 4 Tile de la map sont differents les uns des autres
            
            Assert.IsTrue(map.Strategie.Save().Equals("Strategy_Name='small'\n"));
            
        }
        [TestMethod]
        public void StandardMapTest()
        {
            //tester l'initialisation de la map
            Map map = new Map("standard");
            Assert.AreEqual(map.Strategie.GetType(), typeof(StrategieMapStandard));
            Assert.AreEqual(map.Strategie.GetNbPlayer(), 2);
            Assert.AreEqual(map.Strategie.GetSizeMap(), 14);
            Assert.AreEqual(map.Strategie.GetNbTurnMax(), 30);
            Assert.AreEqual(map.Strategie.GetUnitPerPlayer(), 8);
            //tester si on a exactement 4 Tile dans la map
            Assert.AreEqual(map.TileList.Count, 14*14);
            //tester si les 4 Tile de la map sont differents les uns des autres
            
            Assert.IsTrue(map.Strategie.Save().Equals("Strategy_Name='standard'\n"));

        }
    }
}