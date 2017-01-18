using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    public enum BeeType
    {
        None,
        Queen,
        Worker,
        Drone
    }

    public enum GameState
    {
        None,
        Started,
        InProgress,
        Finished,
        Failed
    }
}
