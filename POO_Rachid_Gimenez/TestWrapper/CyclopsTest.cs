using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class CyclopsTest
    {
        [TestMethod]
        public void TestCyclopsAttackPoint()
        {
            Race r = new Cyclops();
            Assert.AreEqual(r.GetAtkPoint(), 4);
        }

        [TestMethod]
        public void TestCyclopsLifePoint()
        {
            Race r = new Cyclops();
            Assert.AreEqual(r.GetLifePoint(), 12);
        }

        [TestMethod]
        public void TestCyclopsDefencePoint()
        {
            Race r = new Cyclops();
            Assert.AreEqual(r.GetDefPoint(), 6);
        }

        [TestMethod]
        public void TestCyclopsVictoryPoint()
        {
            Race r = new Cyclops();
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileVolcano), 1);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileSwamp), 0);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileDesert), 3);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TilePlain), 2);
        }

        [TestMethod]
        public void TestCyclopsMovePoint()
        {
            Race r = new Cyclops();
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileDesert), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TilePlain), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileVolcano), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileSwamp), 1);
        }
    }
}
