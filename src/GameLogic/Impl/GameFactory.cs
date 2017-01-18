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
            throw new NotImplementedException();
        }

        public GameContext CreateContext(List<Bee> bees, Bee selectedBee)
        {
            throw new NotImplementedException();
        }
    }
}
