using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GameLogic.Model;
using GameLogic.Interfaces;
using GameLogic.Impl;
using System.Diagnostics;

namespace BeeGameUnitTest
{
    [TestClass]
    public class BeeCatcherTest
    {
        private List<Bee> bees;
        private IBeeCatcher catcher;

        [TestInitialize]
        public void Initialize()
        {
            this.catcher = new BeeCatcher();
            this.bees = new List<Bee> {
                new Bee(1, 10, BeeType.Queen) ,new Bee(1, 5, BeeType.Worker) ,new Bee(2, 5, BeeType.Worker)
                ,new Bee(3, 3, BeeType.Drone) ,new Bee(4, 3, BeeType.Drone) ,new Bee(5, 3, BeeType.Drone)
            };
        }

        [TestMethod]
        public void Hit_Successful()
        {
            Bee bee = this.catcher.Hit(this.bees);
            Assert.IsNotNull(bee);
            bee = this.catcher.Hit(this.bees);
            Trace.WriteLine(string.Format("Selected bee - [{0}]", bee.Id));
            bee = this.catcher.Hit(this.bees);
            Trace.WriteLine(string.Format("Selected bee - [{0}]", bee.Id));
            bee = this.catcher.Hit(this.bees);
            Trace.WriteLine(string.Format("Selected bee - [{0}]", bee.Id));
            bee = this.catcher.Hit(this.bees);
            Trace.WriteLine(string.Format("Selected bee - [{0}]", bee.Id));
        }

        [TestMethod]
        public void Hit_Successful_AlwaysFirst_OtherKilled()
        {
            this.bees.ForEach(b=> { if(b.Id != 1) b.RemoveLifeSpan(b.LifeSpan); });
            Bee bee = this.catcher.Hit(this.bees);
            Assert.AreEqual(1, bee.Id);
            bee = this.catcher.Hit(this.bees);
            Assert.AreEqual(1, bee.Id);
            bee = this.catcher.Hit(this.bees);
            Assert.AreEqual(1, bee.Id);
        }

        [TestMethod]
        public void Hit_Successful_OnlyFirstTwo_OtherKilled()
        {
            this.bees.ForEach(b => { if (b.Id != 1 && b.Id != 2) b.RemoveLifeSpan(b.LifeSpan); });
            Bee bee = this.catcher.Hit(this.bees);
            Assert.IsTrue(bee.Id == 1 || bee.Id == 2);
            bee = this.catcher.Hit(this.bees);
            Assert.IsTrue(bee.Id == 1 || bee.Id == 2);
            bee = this.catcher.Hit(this.bees);
            Assert.IsTrue(bee.Id == 1 || bee.Id == 2);
        }

        [TestMethod]
        public void Hit_ReturnNull_AllDied()
        {
            this.bees.ForEach(b => {  b.RemoveLifeSpan(b.LifeSpan); });
            Bee bee = this.catcher.Hit(this.bees);
            Assert.IsNull(bee);
        }
    }
}
