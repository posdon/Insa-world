using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class GameSaveTest
    {
        GameBuilder gameBuilder;
        [TestMethod]
        public void TestGameSave()
        {
            gameBuilder = new GameBuilderUnsaved();

            Player[] tabPlayer = new Player[2];
            tabPlayer[0] = new Player("Joueur1", "cyclops");
            tabPlayer[1] = new Player("Joueur2", "centaur");

            gameBuilder.AddStrategy("demo");
            gameBuilder.AddPlayer(tabPlayer);
            Game game = gameBuilder.Build();


            Assert.AreEqual(game.NbJoueur, 2);
            Assert.AreEqual(game.ListPlayer.Count, 2);
            Assert.AreEqual(game.ListPlayer[0].Name, "Joueur1");
            Assert.AreEqual(game.ListPlayer[0].RaceString, "cyclops");
            Assert.AreEqual(game.ListPlayer[0].EntityList.Count, 4);
            Assert.AreEqual(game.ListPlayer[1].Name, "Joueur2");
            Assert.AreEqual(game.ListPlayer[1].RaceString, "centaur");
            Assert.AreEqual(game.ListPlayer[1].EntityList.Count, 4);

            if (game.CurrEntity.Pos==0)
            {
                game.Move(1);
            }
            else if (game.CurrEntity.Pos == 35)
            {
                game.Move(34);
            }
            else if (game.CurrEntity.Pos == 5)
            {
                game.Move(4);
            }
            else if (game.CurrEntity.Pos == 29)
            {
                game.Move(30);
            }

            //On enregistre une sauvegarde du game dans un fichier SaveFile
            game.Save("SaveFile.txt", -1);

            Assert.IsTrue(game.VerifEndGame()==-1);

            //On charge la sauvegarde
            gameBuilder = new GameBuilderSaved("C:\\Users\\agimenez\\Documents\\SaveFile.txt");

            Game gameLoad = gameBuilder.Build();

            Assert.AreEqual(gameLoad.NbJoueur, 2);
            Assert.AreEqual(gameLoad.ListPlayer.Count, 2);
            Assert.AreEqual(gameLoad.ListPlayer[0].Name, "Joueur1");
            Assert.AreEqual(gameLoad.ListPlayer[0].RaceString, "cyclops");
            
            //Assert.AreEqual(game.ListPlayer[0].EntityList.Count, 4);
            Assert.AreEqual(gameLoad.ListPlayer[1].Name, "Joueur2");
            Assert.AreEqual(gameLoad.ListPlayer[1].RaceString, "centaur");
            Assert.AreEqual(gameLoad.ListPlayer[1].EntityList.Count, 4);
        }
    }
}
