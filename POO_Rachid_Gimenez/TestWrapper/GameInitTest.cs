using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class GameInitTest
    {
        
        [TestMethod]
        public void TestNbPlayer()
        {
            //Nous allons tester l'initialisation d'un game
            GameBuilderUnsaved gameBuilder = new GameBuilderUnsaved(); ;

            Player[] tabPlayer = new Player[2];
            tabPlayer[0] = new Player("Joueur1", "cyclops");
            tabPlayer[1] = new Player("Joueur2", "centaur");
            
            gameBuilder.AddStrategy("demo");
            gameBuilder.AddPlayer(tabPlayer);
            Game game = gameBuilder.Build();

            Assert.AreEqual(game.ListPlayer.Count, 2);
            Assert.AreEqual(game.NbJoueur, 2);
            Assert.AreEqual(game.ListPlayer[0].Name, "Joueur1");
            Assert.AreEqual(game.ListPlayer[0].RaceString, "cyclops");
            Assert.AreEqual(game.ListPlayer[0].EntityList.Count, 4);
            Assert.AreEqual(game.ListPlayer[1].Name, "Joueur2");
            Assert.AreEqual(game.ListPlayer[1].RaceString, "centaur");
            Assert.AreEqual(game.ListPlayer[1].EntityList.Count, 4);
            Assert.AreEqual(game.Map.Strategie.GetType(), typeof(StrategieMapDemo));
            Assert.AreEqual(game.CurrPlayerNumber,0);
            Assert.AreEqual(game.CurrTurnNumber, 0);
        }
    }
}
