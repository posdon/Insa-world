using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestPlayer()
        {
            //tester l'initialisation des player
            Player p1 = new Player("Joueur1", "Cyclops");
            Player p2 = new Player("Joueur2", "Centaur");

            Assert.AreEqual(p1.Name,"Joueur1");
            Assert.AreEqual(p2.Name, "Joueur2");
            Assert.AreEqual(p1.RaceString, "Cyclops");
            Assert.AreEqual(p2.RaceString, "Centaur");
            Assert.AreEqual(p1.VictoryPoint, 0);
            Assert.AreEqual(p2.VictoryPoint, 0);
            Entity e = new Entity(1, "Cyclops", 1);
            p1.AddEntity(e);
            Assert.IsTrue(p1.EntityList.Contains(e));

            Player p3 = new Player();
            Assert.AreEqual(p3.Name, "");
            Assert.AreEqual(p3.RaceString, "");
            Assert.AreEqual(p3.VictoryPoint, 0);
        }
    }
}
