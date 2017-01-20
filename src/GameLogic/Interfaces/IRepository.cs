using GameLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Interfaces
{
    public interface IRepository
    {
        // bees keeps bees for saving
        // gameId keeps existing id game. default value indicates that we will save new game.
        // Returns game id
        int Save(List<Bee> bees, int gameId = 0);
        List<Bee> Restore(int gameId);
        // Returns true if successful else false
        bool Remove(int gameId);
    }
}
