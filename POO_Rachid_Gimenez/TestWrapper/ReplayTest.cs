using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POO_Rachid_Gimenez;

namespace TestWrapper
{
    [TestClass]
    public class ReplayTest
    {
        GameBuilderReplay gameBuilder;
        [TestMethod]
        public void TestAttackDefense()
        {
            gameBuilder = new GameBuilderReplay();
            gameBuilder.Load("C:\\Users\\agimenez\\Documents\\SaveFile.txt");
            Game game = gameBuilder.Build();
        }
    }
}
