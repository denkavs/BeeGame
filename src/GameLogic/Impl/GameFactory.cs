using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;

namespace GameLogic.Impl
{
    class GameFactory : IGameFactory
    {
        public List<Bee> CreateBees(List<BeeConfig> conf)
        {
            List<Bee> bees = new List<Bee>();
            int index = 0;
            if(conf != null)
            {
                foreach (BeeConfig config in conf)
                {
                    for (int i = 0; i < config.Count; i++)
                    {
                        index++;
                        bees.Add(new Bee(index, config.LifeSpan, config.Type));
                    }
                }
            }

            return bees;
        }

        public GameContext CreateContext(List<Bee> bees, Bee selectedBee)
        {
            GameContext context = new GameContext();
            context.GameResult = new GameResult();
            context.Bees = bees;
            context.SelectedBee = selectedBee;
            return context;
        }
    }
}
