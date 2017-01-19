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
            GameInfo gi;
            if(gameId == 0)
            {
                // create new game
                List<BeeConfig> cofiguration = this.confMgr.GetInitData();
                List<Bee> bees = this.factory.CreateBees(cofiguration);
                int id = this.repository.Save(bees);
                gi = new GameInfo(id, null, bees, id != 0 ? GameState.Started : GameState.Failed, string.Empty);
            }
            else
            {
                // load game by gameId, make hit, return result
                List<Bee> bees = this.repository.Restore(gameId);
                // if bees == null then game does not exist.
                if(bees != null)
                {
                    Bee hittedBee = this.beeCatcher.Hit(bees);
                    GameContext context = this.factory.CreateContext(bees, hittedBee);
                    this.gameEngine.Play(context);
                    gi = new GameInfo(gameId, context.SelectedBee, context.Bees, context.GameResult.GameState, context.GameResult.Message);
                }
                else
                {
                    gi = new GameInfo(0, null, null, GameState.Unknown, "Unknown game.");
                }
            }
            return gi;
        }
    }
}
