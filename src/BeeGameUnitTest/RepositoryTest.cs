using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Interfaces;
using GameLogic.Impl;
using GameLogic.Model;
using System.Collections.Generic;

namespace BeeGameUnitTest
{
    [TestClass]
    public class RepositoryTest
    {
        private List<Bee> bees;
        private IRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            this.repository = new Repository();
            this.bees = new List<Bee> {
                new Bee(1, 10, BeeType.Queen) ,new Bee(1, 5, BeeType.Worker) ,new Bee(2, 5, BeeType.Worker)
                ,new Bee(3, 3, BeeType.Drone) ,new Bee(4, 3, BeeType.Drone) ,new Bee(5, 3, BeeType.Drone)
            };
        }

        [TestMethod]
        public void Save_Successful()
        {
            int gameId = this.repository.Save(this.bees);
            Assert.AreEqual(1, gameId);
            gameId = this.repository.Save(this.bees);
            Assert.AreEqual(2, gameId);
        }

        [TestMethod]
        public void Restore_Successful()
        {
            int gameId = this.repository.Save(this.bees);
            List<Bee> restored = this.repository.Restore(gameId);
            Assert.AreEqual(6, restored.Count);
            Assert.AreEqual(this.bees[0].Type, restored[0].Type);
        }

        [TestMethod]
        public void Restore_Failed()
        {
            int gameId = this.repository.Save(this.bees);

            List<Bee> restored = this.repository.Restore(gameId + 1);
            Assert.IsNull(restored);
        }

        [TestMethod]
        public void Remove_Successful()
        {
            int gameId = this.repository.Save(this.bees);
            bool res = this.repository.Remove(gameId);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Remove_Failed()
        {
            int gameId = this.repository.Save(this.bees);

            bool res = this.repository.Remove(gameId + 1);
            Assert.IsTrue(!res);
        }
    }
}
