using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    class Service : IService
    {
        private IBeeCatcher beeCatcher;
        private IConfiguration confMgr;
        private IGameFactory factory;
        private IGamePlayEngine gameEngine;
        private IRepository repository;

        public Service(IRepository repository, IBeeCatcher catcher, IConfiguration config, IGameFactory factory, IGamePlayEngine engine)
        {
            this.repository = repository;
            this.beeCatcher = catcher;
            this.confMgr = config;
            this.factory = factory;
            this.gameEngine = engine;
        }

        public GameInfo ProcessHit(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
