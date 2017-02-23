using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class GameTurnPointTest
    {
        private GameBuilder gameBuilder;
        [TestMethod]
        public void TestMovePointTurn()
        {
            //construire le gameBuilder
            gameBuilder = new GameBuilderUnsaved();
            //declarer un tableau de Player
            Player[] tabPlayer = new Player[2];
            tabPlayer[0] = new Player("Joueur1", "cyclops");
            tabPlayer[1] = new Player("Joueur2", "centaur");
            //Ajouter la strategie au gameBuilder
            gameBuilder.AddStrategy("demo");
            //Ajouter les joueurs au gameBuilder
            gameBuilder.AddPlayer(tabPlayer);
            //declarer et construire le game
            Game game = gameBuilder.Build();
            //Debut des tours
            game.StartTurn();
            //recuperer les points de mouvement de l'netité courante du premier player
            double pt1 = game.CurrEntity.MovePoint;
            //faire un movement a la case 2
            //Et si il commence pas en 0 ?
            //game.Move(2);
            if (game.CurrEntity.Pos == 0)
            {
                game.Move(2);
            }
            if (game.CurrEntity.Pos == 35)
            {
                game.Move(33);
            }
            if (game.CurrEntity.Pos == 5)
            {
                game.Move(3);
            }
            if (game.CurrEntity.Pos == 30)
            {
                game.Move(32);
            }
            //recuperer les points de mouvement apres le move
            double pt2 = game.CurrEntity.MovePoint;
            //les points de mouvements doivent baisser
            Assert.IsTrue(pt1 > pt2);
            //passer la main à l'autre joueur
            game.NextPlayer();
            game.StartTurn();
            //recuperer la position de l'entité courante
            int pos1 = game.CurrEntity.Pos;
            //deplacer l'entité courante
            game.CurrEntity.Move(10, game.Map);
            //recuperer la nouvelle position
            int pos2 = game.CurrEntity.Pos;
            //tester si la position a changé
            Assert.IsTrue(pos1 != pos2);
            //faire un move qui teste si on peut 
            game.Move(10);
            //recuperer la nouvelle position
            int pos3 = game.CurrEntity.Pos;
            //tester si la position n'a pas changé car on a que 3 points de mouvement
            Assert.IsTrue(pos3 == pos2);
        }
    }
}
