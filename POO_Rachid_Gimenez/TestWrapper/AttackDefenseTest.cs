using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class AttackDefenseTest
    {
        GameBuilder gameBuilder;
        [TestMethod]
        public void TestAttackDefense()
        {
            gameBuilder = new GameBuilderUnsaved(); ;

            Player[] tabPlayer = new Player[2];
            tabPlayer[0] = new Player("Joueur1", "cyclops");
            tabPlayer[1] = new Player("Joueur2", "centaur");

            gameBuilder.AddStrategy("demo");
            gameBuilder.AddPlayer(tabPlayer);
            Game game = gameBuilder.Build();

            game.StartTurn();
            game.CurrEntity.Pos = 2;

            //les movePoints ne sont sencés changer après un Skip() car ils sont au max
            double Oldpoint = game.CurrEntity.MovePoint;
            Assert.IsTrue(game.Skip());
            Assert.AreEqual(Oldpoint, game.CurrEntity.MovePoint);

            //On passe la main a l'enemy
            game.EndMyTurn();
            game.CurrEntity.Pos = 3;
            game.EndMyTurn();

            //c'est la seul entity dans la position 3 donc c'est la meilluer defense
            Assert.AreEqual(game.CurrEntity,game.Map.GetBestDefenser(2));

            game.Map.GetDistance(game.CurrEntity, 3);

            //Le move va decalancher un combat vu que il ya l'enemy est dans la case suivante
            //l'entité courante gagne et cela retourne true
            Assert.IsTrue(game.Move(3));
            Assert.AreEqual(game.GetActionNumber(),1);

            //On met tout ses LifePoint à 0 pour que l'autre joueur gagne
            // FAUX ! On sort l'unité du plateau en lui mettant pour position -1
            foreach (Entity e in game.ListPlayer[0].EntityList)
            {
                //e.LifePoint = 0;
                game.Map.Remove(e.Pos,e);
            }
            //On passe la main à l'autre joueur
            game.EndMyTurn();
            Assert.AreEqual(game.VerifEndGame(), game.CurrPlayerNumber);
        }
    }
}
