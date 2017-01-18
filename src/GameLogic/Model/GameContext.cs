using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    public class GameContext
    {
        public Bee SelectedBee { get; set; }
        public List<Bee> Bees { get; set; }
        public GameResult GameResult { get; set; }
    }
}
