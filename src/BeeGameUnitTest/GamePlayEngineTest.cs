using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Interfaces;
using GameLogic.Model;
using System.Collections.Generic;
using Moq;
using GameLogic.Impl;

namespace BeeGameUnitTest
{
    [TestClass]
    public class GamePlayEngineTest
    {
        private List<Bee> bees;
        private List<BeeConfig> configLst;

        [TestInitialize]
        public void Initialize()
        {
            this.bees = new List<Bee> {
                new Bee(1, 10, BeeType.Queen) ,new Bee(1, 5, BeeType.Worker) ,new Bee(2, 5, BeeType.Worker)
                ,new Bee(3, 3, BeeType.Drone) ,new Bee(4, 3, BeeType.Drone) ,new Bee(5, 3, BeeType.Drone)
            };

            this.configLst = new List<BeeConfig> {
                new BeeConfig() { Count = 1, Deduction = 5, LifeSpan = 10, Type = BeeType.Queen },
                new BeeConfig() { Count = 2, Deduction = 2, LifeSpan = 5, Type = BeeType.Worker },
                new BeeConfig() { Count = 3, Deduction = 2, LifeSpan = 3, Type = BeeType.Drone }
            };
        }

        [TestMethod]
        public void Play_KillQueenBee()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            this.configLst[0].Deduction = 10;

            config.Setup(c=> c.GetInitData()).Returns(this.configLst);

            IGamePlayEngine engine = new GamePlayEngine(config.Object);
            GameContext context = new GameContext();
            context.Bees = this.bees;
            context.SelectedBee = this.bees[0];
            context.GameResult = new GameResult();

            engine.Play(context);

            Assert.AreEqual(GameState.Finished, context.GameResult.GameState);
            Assert.IsTrue(context.Bees.TrueForAll(b => !b.IsAlive()));
        }


        [TestMethod]
        public void Play_HitQueenBee()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();

            config.Setup(c => c.GetInitData()).Returns(this.configLst);

            IGamePlayEngine engine = new GamePlayEngine(config.Object);
            GameContext context = new GameContext();
            context.Bees = this.bees;
            context.SelectedBee = this.bees[0];
            context.GameResult = new GameResult();

            engine.Play(context);

            Assert.AreEqual(GameState.InProgress, context.GameResult.GameState);
            Assert.AreEqual(5, context.SelectedBee.LifeSpan);
            Assert.AreEqual(BeeType.Queen, context.SelectedBee.Type);
        }

        [TestMethod]
        public void Play_HitWorkerBee()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(c => c.GetInitData()).Returns(this.configLst);

            IGamePlayEngine engine = new GamePlayEngine(config.Object);
            GameContext context = new GameContext();
            context.Bees = this.bees;
            context.SelectedBee = this.bees[1];
            context.GameResult = new GameResult();

            engine.Play(context);

            Assert.AreEqual(GameState.InProgress, context.GameResult.GameState);
            Assert.AreEqual(3, context.SelectedBee.LifeSpan);
        }

        [TestMethod]
        public void Play_HitDroneBee()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(c => c.GetInitData()).Returns(this.configLst);

            IGamePlayEngine engine = new GamePlayEngine(config.Object);
            GameContext context = new GameContext();
            context.Bees = this.bees;
            context.SelectedBee = this.bees[3];
            context.GameResult = new GameResult();

            engine.Play(context);

            Assert.AreEqual(GameState.InProgress, context.GameResult.GameState);
            Assert.AreEqual(1, context.SelectedBee.LifeSpan);
            Assert.AreEqual(BeeType.Drone, context.SelectedBee.Type);
        }
    }
}
