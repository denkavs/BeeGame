using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IGameFactory
    {
        List<Bee> CreateBees(List<BeeConfig> conf);
        GameContext CreateContext(List<Bee> bees, Bee selectedBee);
    }
}
