using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class CeberusTest
    {
        [TestMethod]
        public void TestCerberusAttackPoint()
        {
            Race r = new Cerberus();
            Assert.AreEqual(r.GetAtkPoint(), 6);
        }

        [TestMethod]
        public void TestCerberusLifePoint()
        {
            Race r = new Cerberus();
            Assert.AreEqual(r.GetLifePoint(), 10);
        }

        [TestMethod]
        public void TestCerberusDefencePoint()
        {
            Race r = new Cerberus();
            Assert.AreEqual(r.GetDefPoint(), 4);
        }

        [TestMethod]
        public void TestCerberusVictoryPoint()
        {
            Race r = new Cerberus();
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileVolcano), 3);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileSwamp), 2);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TileDesert), 1);
            Assert.AreEqual(r.GetVictoryPoint(TileFactory.INSTANCE.TilePlain), 0);
        }

        [TestMethod]
        public void TestCerberusMovePoint()
        {
            Race r = new Cerberus();
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileDesert),1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TilePlain), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileVolcano), 1);
            Assert.AreEqual(r.GetMovePointCost(TileFactory.INSTANCE.TileSwamp), 1);
        }
    }
}
