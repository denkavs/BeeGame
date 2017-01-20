using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic.Impl;
using GameLogic.Interfaces;
using System.Configuration;
using GameLogic.Infrastructure;
using GameLogic.Model;

namespace BeeGameUnitTest
{
    [TestClass]
    public class ConfigurationTest
    {
        private static BeeConfigSection section;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            section = CreateSection();
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            ClearSection();
        }

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

                    BeeConfigElement element = new BeeConfigElement("queen", 5, 1, 10);
                    tempSection.Bees.Add(element);
                    element = new BeeConfigElement("worker", 2, 3, 3);
                    tempSection.Bees.Add(element);

                    element = new BeeConfigElement("drone", 2, 5, 2);
                    tempSection.Bees.Add(element);

                    element = new BeeConfigElement("ghost", 2, 1, 2);
                    tempSection.Bees.Add(element);

                    // Save the application configuration file.
                    tempSection.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("BeeConfig");

                    section = tempSection;
                }
            }
            catch(Exception e)
            {
            }

            return section;
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void GetInitData_Successful()
        {
            IConfiguration conf = new GameLogic.Impl.Configuration();
            var result = conf.GetInitData();
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(BeeType.Queen, result[0].Type);
            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(10, result[0].LifeSpan);
            Assert.AreEqual(BeeType.Unknown, result[3].Type);
            Assert.AreEqual(1, result[3].Count);
            Assert.AreEqual(2, result[3].LifeSpan);
        }

        [TestMethod]
        public void GetInitData_SectionNotExist()
        {
            IConfiguration conf = new GameLogic.Impl.Configuration();
            var result = conf.GetInitData();
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(BeeType.Queen, result[0].Type);
            Assert.AreEqual(1, result[0].Count);
            Assert.AreEqual(10, result[0].LifeSpan);
            Assert.AreEqual(BeeType.Unknown, result[3].Type);
            Assert.AreEqual(1, result[3].Count);
            Assert.AreEqual(2, result[3].LifeSpan);
        }
    }
}
