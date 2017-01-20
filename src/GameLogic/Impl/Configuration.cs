using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;
using System.Configuration;
using GameLogic.Infrastructure;

namespace GameLogic.Impl
{
    class Configuration : IConfiguration
    {
        public List<BeeConfig> GetInitData()
        {
            List<BeeConfig> result = new List<BeeConfig>();
            BeeConfigSection section = (BeeConfigSection)ConfigurationManager.GetSection("BeeConfig");
            if(section != null)
            {
                foreach (BeeConfigElement element in section.Bees)
                {
                    result.Add(new BeeConfig() { Type = element.GetBeeType(), LifeSpan = element.LifeSpan, Count = element.Count, Deduction = element.Deduction });
                }
            }

            return result;
        }
    }
}
