using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;

namespace GameLogic.Impl
{
    class Repository : IRepository
    {
        public void Remove(int gameId)
        {
            throw new NotImplementedException();
        }

        public List<Bee> Restore(int gameId)
        {
            throw new NotImplementedException();
        }

        public int Save(List<Bee> bees)
        {
            throw new NotImplementedException();
        }
    }
}
