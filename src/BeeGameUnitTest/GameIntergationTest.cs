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

namespace BeeGameUnitTest
{
    [TestClass]
    public class GameIntergationTest
    {
        private static int queenLifeScope = 5;
        private static int queenDeduction = 2;
        private static int queenCount = 1;

        private static int workerLifeScope = 3;
        private static int workerDeduction = 2;
        private static int workerCount = 2;

        private static int droneLifeScope = 2;
        private static int droneDeduction = 1;
        private static int droneCount = 3;

        private static void ClearSection()
        {
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            BeeConfigSection tempSection = (BeeConfigSection)config.Sections["BeeConfig"];
            if (tempSection != null)
            {
                config.Sections.Remove("BeeConfig");
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("BeeConfig");
            }
        }

        private static BeeConfigSection CreateSection()
        {
            BeeConfigSection section = null;

            try
            {
                // create section dinamicly and add it to config file
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                BeeConfigSection tempSection = (BeeConfigSection)config.Sections["BeeConfig"];

                if (tempSection == null)
                {
                    tempSection = new BeeConfigSection();

                    config.Sections.Add("BeeConfig", tempSection);

                    BeeConfigElement element = new BeeConfigElement("queen", queenDeduction, queenCount, queenLifeScope);
                    tempSection.Bees.Add(element);
                    element = new BeeConfigElement("worker", workerDeduction, workerCount, workerLifeScope);
                    tempSection.Bees.Add(element);

                    element = new BeeConfigElement("drone", droneDeduction, droneCount, droneLifeScope);
                    tempSection.Bees.Add(element);

                    // Save the application configuration file.
                    tempSection.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("BeeConfig");

                    section = tempSection;
                }
            }
            catch (Exception e)
            {
            }

            return section;
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            CreateSection();
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            ClearSection();
        }

        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void StartGame()
        {
            IRepository repository = new Repository();
            IBeeCatcher catcher = new BeeCatcherPredefined(1,1,2);
            IConfiguration config = new GameLogic.Impl.Configuration();
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
            IConfiguration config = new GameLogic.Impl.Configuration();
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
            IConfiguration config = new GameLogic.Impl.Configuration();
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
        public void GameFailed()
        {

        }
    }
}
