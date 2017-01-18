using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Interfaces;
using GameLogic.Model;
using Moq;
using System.Collections.Generic;

namespace BeeGameUnitTest
{
    [TestClass]
    public class IServiceTest
    {
        private static Mock<IRepository> repository;
        private static Mock<IConfiguration> config;
        private static Mock<IGamePlayEngine> engine;
        private static Mock<IGameFactory> factory;
        private static List<Bee> bees;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            IServiceTest.bees = new List<Bee> {
                new Bee(1, 10, BeeType.Queen) ,new Bee(1, 5, BeeType.Worker) ,new Bee(2, 5, BeeType.Worker)
                ,new Bee(3, 3, BeeType.Drone) ,new Bee(4, 3, BeeType.Drone) ,new Bee(5, 3, BeeType.Drone)
            };
            IServiceTest.repository = new Mock<IRepository>();
            IServiceTest.repository.Setup(a => a.Restore(It.IsAny<int>())).Returns(IServiceTest.bees);

            IServiceTest.config = new Mock<IConfiguration>();
            IServiceTest.config.Setup(a => a.GetInitData()).Returns(new List<BeeConfig> {
                new BeeConfig() { Count = 1, Deduction = 5, LifeSpan = 10, Type = BeeType.Queen },
                new BeeConfig() { Count = 2, Deduction = 2, LifeSpan = 5, Type = BeeType.Worker },
                new BeeConfig() { Count = 3, Deduction = 2, LifeSpan = 3, Type = BeeType.Drone }
            });

            IServiceTest.engine = new Mock<IGamePlayEngine>();
            IServiceTest.engine.Setup(e => e.Play(It.IsAny<GameContext>()));

            IServiceTest.factory = new Mock<IGameFactory>();
            IServiceTest.factory.Setup<List<Bee>>(f => f.CreateBees(It.IsAny<List<BeeConfig>>())).Returns(IServiceTest.bees);
        }

        [TestInitialize()]
        public void Initialize()
        {
            //this.service = new Service(this.repository.Object, catcher, config.Object, this.factory.Object, this.engine.Object);
        }
        [TestCleanup()]
        public void Cleanup()
        {
            //this.service = null;
        }

        [TestMethod]
        public void StartGame()
        {
            IService service = new Service(IServiceTest.repository.Object, null, IServiceTest.config.Object, IServiceTest.factory.Object, IServiceTest.engine.Object);

            GameInfo gi = service.ProcessHit(0);

            Assert.IsTrue(gi.State == GameState.Started);
            Assert.IsTrue(gi.GameId == 1);
            Assert.AreEqual(gi.Bees[0].Type, IServiceTest.bees[0].Type);
            Assert.AreEqual(gi.Bees.Count, IServiceTest.bees.Count);
        }
    }
}
