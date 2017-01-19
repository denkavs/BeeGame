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
        private static readonly Dictionary<int, List<Bee>> deport = new Dictionary<int, List<Bee>>();
        private static int index = 0;
        private static readonly Object locker = new object();
        static Repository()
        {
        }

        public bool Remove(int gameId)
        {
            lock (locker)
            {
                if (deport.ContainsKey(gameId))
                {
                    return deport.Remove(gameId);
                }
            }
            return false;
        }

        public List<Bee> Restore(int gameId)
        {
            List<Bee> result = null;
            lock (locker)
            {
                if (deport.ContainsKey(gameId))
                {
                    result = new List<Bee>();
                    deport[gameId].ForEach(b=> { result.Add(new Bee(b.Id, b.LifeSpan, b.Type)); } );
                }
            }
            return result;
        }

        public int Save(List<Bee> bees)
        {
            int result = 0;

            if(bees != null && bees.Count > 0)
            {
                lock (locker)
                {
                    result = ++index;
                    deport.Add(result, bees);
                }
            }
            return result;
        }
    }
}
