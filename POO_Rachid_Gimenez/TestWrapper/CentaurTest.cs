using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class CentaurTest
    {
        [TestMethod]
        public void TestCentaurAttackPoint()
        {
            Race r = new Centaur();
            Assert.AreEqual(r.GetAtkPoint(), 8);
        }

        [TestMethod]
        public void TestCentaurLifePoint()
        {
            Race r = new Centaur();
            Assert.AreEqual(r.GetLifePoint(), 10);
        }

        [TestMethod]
        public void TestCentaurDefencePoint()
        {
            Race r = new Centaur();
            Assert.AreEqual(r.GetDefPoint(), 2);
        }

        [TestMethod]
        public void TestCentaurVictoryPoint()
        {
            Race r = new Centaur();
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileVolcano), 0);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileSwamp), 1);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileDesert), 2);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TilePlain), 3);
        }

        [TestMethod]
        public void TestCentaurMovePoint()
        {
            Race r = new Centaur();
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileDesert), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TilePlain), 0.5);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileVolcano), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileSwamp), 1);
        }
    }
}
