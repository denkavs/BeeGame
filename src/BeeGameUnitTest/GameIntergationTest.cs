using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using GameLogic.Infrastructure;
using GameLogic.Interfaces;
using GameLogic.Model;
using GameLogic.Impl;
using BeeGameUnitTest.Infrastructure;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace BeeGameUnitTest
{
    [TestClass]
    public class GameIntergationTest
    {
        class PredefinedConfiguration : IConfiguration
        {
            public List<BeeConfig> GetInitData()
            {
                List<BeeConfig> result = new List<BeeConfig>();

                result.Add(new BeeConfig() { Type = BeeType.Queen, Count = queenCount, Deduction = queenDeduction, LifeSpan = queenLifeScope });
                result.Add(new BeeConfig() { Type = BeeType.Worker, Count = workerCount, Deduction = workerDeduction, LifeSpan = workerLifeScope });
                result.Add(new BeeConfig() { Type = BeeType.Drone, Count = droneCount, Deduction = droneDeduction, LifeSpan = droneLifeScope });

                return result; 
            }
        }

        private static int queenLifeScope = 5;
        private static int queenDeduction = 2;
        private static int queenCount = 1;

        private static int workerLifeScope = 3;
        private static int workerDeduction = 2;
        private static int workerCount = 2;

        private static int droneLifeScope = 2;
        private static int droneDeduction = 1;
        private static int droneCount = 3;

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void StartGame()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(1,1,2);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(0);

            Assert.AreEqual( GameState.Started, gi.State);
            Assert.AreEqual(1, gi.GameId);
            Assert.AreEqual(6, gi.Bees.Count);
            Assert.IsNull(gi.SelectedBee);
        }

        [TestMethod]
        public void GameInProgress_HitQueen_1Time()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(0, 1, 2);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(0);
            int gameId = gi.GameId;

            gi = service.ProcessHit(gi.GameId);

            Assert.AreEqual(GameState.InProgress, gi.State);
            Assert.AreEqual(gameId, gi.GameId);
            Assert.AreEqual(6, gi.Bees.Count);
            Assert.AreEqual(BeeType.Queen, gi.SelectedBee.Type);
            Assert.AreEqual(3, gi.SelectedBee.LifeSpan);
            Assert.AreEqual(0, gi.Bees.Select(b=>b).Where(b=> (b.Type == BeeType.Worker && b.LifeSpan != workerLifeScope) ).Count<Bee>() );
            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Drone && b.LifeSpan != droneLifeScope)).Count<Bee>());
        }

        [TestMethod]
        public void GameInProgress_HitQueen_2Time()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(0, 0, 2);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(0);
            int gameId = gi.GameId;

            gi = service.ProcessHit(gi.GameId);
            gi = service.ProcessHit(gi.GameId);

            Assert.AreEqual(GameState.InProgress, gi.State);
            Assert.AreEqual(gameId, gi.GameId);
            Assert.AreEqual(6, gi.Bees.Count);
            Assert.AreEqual(BeeType.Queen, gi.SelectedBee.Type);
            Assert.AreEqual(1, gi.SelectedBee.LifeSpan);

            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Worker && b.LifeSpan != workerLifeScope)).Count<Bee>());
            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Drone && b.LifeSpan != droneLifeScope)).Count<Bee>());
        }

        [TestMethod]
        public void GameInProgress_KillQueen_Hit3Time_GameFinished()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(0, 0, 0);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(0);
            int gameId = gi.GameId;

            gi = service.ProcessHit(gameId);
            gi = service.ProcessHit(gameId);
            gi = service.ProcessHit(gameId);

            Assert.AreEqual(GameState.Finished, gi.State);
            Assert.AreEqual(gameId, gi.GameId);
            Assert.AreEqual(6, gi.Bees.Count);
            Assert.AreEqual(BeeType.Queen, gi.SelectedBee.Type);
            Assert.AreEqual(0, gi.SelectedBee.LifeSpan);

            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Worker && b.LifeSpan > 0)).Count<Bee>());
            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Drone && b.LifeSpan > 0)).Count<Bee>());
        }

        [TestMethod]
        public void GameInProgress_GameFinished_HitGameAgain_UnknownGame()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(0, 0, 0);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(1);

            Assert.AreEqual( GameState.Unknown, gi.State);
        }

        [TestMethod]
        public void GameInProgress_HitAllWorker_OneDrone()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(1, 2, 3);
            IConfiguration config = new PredefinedConfiguration();
            IGameFactory factory = new GameFactory();
            IGamePlayEngine engine = new GamePlayEngine(config);

            IService service = new Service(repository, catcher, config, factory, engine);

            GameInfo gi = service.ProcessHit(0);
            int gameId = gi.GameId;

            gi = service.ProcessHit(gameId);
            gi = service.ProcessHit(gameId);
            gi = service.ProcessHit(gameId);

            Assert.AreEqual(GameState.InProgress, gi.State);
            Assert.AreEqual(gameId, gi.GameId);
            Assert.AreEqual(6, gi.Bees.Count);
            Assert.AreEqual(BeeType.Drone, gi.SelectedBee.Type);
            Assert.AreEqual(1, gi.SelectedBee.LifeSpan);

            Assert.AreEqual(0, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Worker && b.LifeSpan == workerLifeScope)).Count<Bee>());
            Assert.AreEqual(2, gi.Bees.Select(b => b).Where(b => (b.Type == BeeType.Drone && b.LifeSpan == droneLifeScope)).Count<Bee>());
        }
    }
}
