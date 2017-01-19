using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Impl;
using GameLogic.Interfaces;
using System.Collections.Generic;
using GameLogic.Model;

namespace BeeGameUnitTest
{
    [TestClass]
    public class GameFactoryTest
    {
        private List<BeeConfig> configLst;

        [TestInitialize]
        public void Initialize()
        {
            this.configLst = new List<BeeConfig> {
                new BeeConfig() { Count = 1, Deduction = 5, LifeSpan = 10, Type = BeeType.Queen },
                new BeeConfig() { Count = 2, Deduction = 2, LifeSpan = 5, Type = BeeType.Worker },
                new BeeConfig() { Count = 3, Deduction = 2, LifeSpan = 3, Type = BeeType.Drone }
            };
        }

        [TestMethod]
        public void CreateBees_Return_6()
        {
            IGameFactory factory = new GameFactory();
            List<Bee> bees = factory.CreateBees(this.configLst);
            Assert.AreEqual(6, bees.Count);
        }

        [TestMethod]
        public void CreateBees_Return_0()
        {
            IGameFactory factory = new GameFactory();
            List<Bee> bees = factory.CreateBees(new List<BeeConfig>());
            Assert.AreEqual(0, bees.Count);

            bees = factory.CreateBees(null);
            Assert.AreEqual(0, bees.Count);
        }
    }
}
