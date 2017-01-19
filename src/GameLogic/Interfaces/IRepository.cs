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
        // Returns gameId
        int Save(List<Bee> bees);
        List<Bee> Restore(int gameId);
        // Returns true if successful else false
        bool Remove(int gameId);
    }
}
