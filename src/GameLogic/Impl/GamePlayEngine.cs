using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;

namespace GameLogic.Impl
{
    class GamePlayEngine : IGamePlayEngine
    {
        private int droneDeduction;
        private int queenDeduction;
        private int workerDeduction;

        public GamePlayEngine(IConfiguration config)
        {
            List<BeeConfig> BeeConfigLst = config.GetInitData();
            this.queenDeduction = BeeConfigLst.Select(c => c).Where(c => c.Type == BeeType.Queen).First().Deduction;
            this.workerDeduction = BeeConfigLst.Select(c => c).Where(c => c.Type == BeeType.Worker).First().Deduction;
            this.droneDeduction = BeeConfigLst.Select(c => c).Where(c => c.Type == BeeType.Drone).First().Deduction;
        }

        public void Play(GameContext context)
        {
            context.GameResult.GameState = GameState.InProgress;
            context.GameResult.Message = "Hit again";

            if (context.SelectedBee.Type == BeeType.Queen)
            {
                context.SelectedBee.RemoveLifeSpan(this.queenDeduction);

                if (!context.SelectedBee.IsAlive())
                {
                    // kill all bees
                    context.Bees.ForEach(b => { int life = b.LifeSpan; b.RemoveLifeSpan(life); });
                    context.GameResult.GameState = GameState.Finished;
                    context.GameResult.Message = "Queen bee was died.";
                }
                return;
            }

            if(context.SelectedBee.Type == BeeType.Worker)
            {
                context.SelectedBee.RemoveLifeSpan(this.workerDeduction);
            }

            if (context.SelectedBee.Type == BeeType.Drone)
            {
                context.SelectedBee.RemoveLifeSpan(this.droneDeduction);
            }
        }
    }
}
