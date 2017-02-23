using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class EntityTest
    {
        [TestMethod]
        public void TestEntity()
        {
            //decalarion de deux entités et tests d'initialisation
            Entity centaur = new Entity(1, "centaur", 1);
            Entity cyclops = new Entity(2, "cyclops", 2);
            Entity inconnue = new Entity();
            
            Assert.AreEqual(centaur.Pos,-1);
            Assert.AreEqual(centaur.MovePoint, 3);
            

            Assert.IsTrue(centaur.Id == 1);
            Assert.IsTrue(centaur.Team== 1);
            Assert.AreEqual(centaur.Race.GetType(), (new Centaur()).GetType());
            int lp = centaur.LifePoint;
            Assert.AreEqual(lp, (new Centaur()).GetLifePoint());
            centaur.Regenerate();
            int lp2 = centaur.LifePoint;

            //LifePoint n'est pas censé changer car le centaur a toujours tout ses points
            Assert.IsTrue(lp == lp2);

            //vu que le centaur a toujours des LifePoints il est pas Dead
            Assert.IsFalse(centaur.IsDead());

            //centaur est plus fort de cyclops
            // Faux ! Pour rappel même si centaur est plus fort que cyclops, on fait un gros aléa qui fait que cyclops peut encore gagner !!!
            // Confrontation retourne les dégats reçus...
            //Assert.IsTrue(centaur.Confrontation(cyclops) > 0);
            //On attack le centaur et on lui enleve tout ses LifePoints
            //maintenant il est dead
            Assert.IsTrue(centaur.Damage(centaur.LifePoint));
        }
    }
}
