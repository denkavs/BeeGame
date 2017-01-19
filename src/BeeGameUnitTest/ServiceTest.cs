using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Interfaces;
using GameLogic.Model;
using Moq;
using System.Collections.Generic;

namespace BeeGameUnitTest
{
    [TestClass]
    public class ServiceTest
    {
        private static List<Bee> bees;
        private static int gameId = 3;
        private static List<BeeConfig> configLst;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize()]
        public void Initialize()
        {
            ServiceTest.bees = new List<Bee> {
                new Bee(1, 10, BeeType.Queen) ,new Bee(1, 5, BeeType.Worker) ,new Bee(2, 5, BeeType.Worker)
                ,new Bee(3, 3, BeeType.Drone) ,new Bee(4, 3, BeeType.Drone) ,new Bee(5, 3, BeeType.Drone)
            };

            ServiceTest.configLst = new List<BeeConfig> {
                new BeeConfig() { Count = 1, Deduction = 5, LifeSpan = 10, Type = BeeType.Queen },
                new BeeConfig() { Count = 2, Deduction = 2, LifeSpan = 5, Type = BeeType.Worker },
                new BeeConfig() { Count = 3, Deduction = 2, LifeSpan = 3, Type = BeeType.Drone }
            };
        }
        [TestCleanup()]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void ProcessHit_ReturnNewGame()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(c => c.GetInitData()).Returns(ServiceTest.configLst);

            Mock<IGameFactory> factory = new Mock<IGameFactory>();
            factory.Setup(f => f.CreateBees(It.IsAny<List<BeeConfig>>())).Returns(ServiceTest.bees);

            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(r => r.Save(It.Is<List<Bee>>((b) => Object.ReferenceEquals(b, ServiceTest.bees)))).Returns(ServiceTest.gameId);

            IService service = new Service(repository.Object, null, config.Object, factory.Object, null);

            GameInfo gi = service.ProcessHit(0);

            Assert.IsTrue(gi.State == GameState.Started);
            Assert.IsTrue(gi.GameId == 3);
            Assert.AreEqual(gi.Bees[0].Type, ServiceTest.bees[0].Type);
            Assert.AreEqual(gi.Bees.Count, ServiceTest.bees.Count);
        }

        [TestMethod]
        public void ProcessHit_GameInProgress()
        {
            // arrange
            int gameiD = 1;
            Bee hittedBee = ServiceTest.bees[1];
            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(r => r.Restore(It.Is<int>((b) => b == gameiD))).Returns(ServiceTest.bees); // game exist

            Mock<IBeeCatcher> catcher = new Mock<IBeeCatcher>();
            catcher.Setup(c => c.Hit(It.Is<List<Bee>>(b => Object.ReferenceEquals(b, ServiceTest.bees)))).Returns(hittedBee); // select hitted bee

            Mock<IGameFactory> factory = new Mock<IGameFactory>();
            GameContext context = new GameContext()
            {
                SelectedBee = hittedBee,
                Bees = ServiceTest.bees,
                GameResult = new GameResult() { GameState = GameState.InProgress, Message = "Do next hit" }
            };
            factory.Setup(f => f.CreateContext(It.Is<List<Bee>>(b => Object.ReferenceEquals(b, ServiceTest.bees)), It.Is<Bee>(bOne => Object.ReferenceEquals(bOne, hittedBee)))).Returns(context); // create game context

            Mock<IGamePlayEngine> engine = new Mock<IGamePlayEngine>();
            engine.Setup(e => e.Play(It.Is<GameContext>(gc => Object.ReferenceEquals(gc, context))));
            context.SelectedBee.RemoveLifeSpan(3);
            IService service = new Service(repository.Object, catcher.Object, null, factory.Object, engine.Object);

            // perform
            GameInfo gi = service.ProcessHit(gameiD);

            // check
            Assert.IsTrue(gi.State == GameState.InProgress);
            Assert.IsTrue(gi.GameId == gameiD);
            Assert.AreEqual(2, gi.SelectedBee.LifeSpan);
            Assert.AreEqual(gi.Bees.Count, ServiceTest.bees.Count);
        }

        [TestMethod]
        public void ProcessHit_GameNotExist()
        {
            //arrange
            int gameiD = 1;

            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(r => r.Restore(It.Is<int>((b) => b == gameiD))).Returns(()=> { return null; }); // game not exist

            IService service = new Service(repository.Object, null, null, null, null);

            // perform
            GameInfo gi = service.ProcessHit(gameiD);

            // check
            Assert.IsTrue(gi.State == GameState.Unknown);
            Assert.AreEqual(null, gi.SelectedBee);
            Assert.AreEqual(null, gi.Bees);
        }

        [TestMethod]
        public void ProcessHit_Failed_GameCreate()
        {
            //arrange
            int gameiD = 1;

            Mock<IRepository> repository = new Mock<IRepository>();
            repository.Setup(r => r.Restore(It.Is<int>((b) => b == gameiD))).Returns(() => { return null; }); // game not exist

            IService service = new Service(repository.Object, null, null, null, null);

            // perform
            GameInfo gi = service.ProcessHit(gameiD);

            // check
            Assert.IsTrue(gi.State == GameState.Unknown);
            Assert.AreEqual(null, gi.SelectedBee);
            Assert.AreEqual(null, gi.Bees);
        }
    }
}
