using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    public struct GameInfo
    {
        public GameInfo(int gameId, Bee selected, List<Bee> bees, GameState state, string message)
        {
            this.SelectedBee = selected;
            this.Bees = bees;
            this.State = state;
            this.Message = message;
            this.GameId = gameId;
        }

        public int GameId { get; private set; }
        public Bee SelectedBee { get; private set; }
        public List<Bee> Bees { get; private set; }
        public GameState State { get; private set; }
        public string Message { get; private set; }
    }
}
