using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;

namespace GameLogic.Impl
{
    class BeeCatcher : IBeeCatcher
    {
        private static readonly Random rand = new Random();
        static BeeCatcher() { }

        public virtual Bee Hit(List<Bee> bees)
        {
            List<Bee> toProcess = bees.Select(b => b).Where(b => b.IsAlive()).ToList(); // take all alive bees
            int min = 0;
            int max = toProcess.Count();
            if (max == 0)
                return null;

            int index = rand.Next(min,max);
            return bees.FirstOrDefault(b=>b.Id == toProcess[index].Id);
        }
    }
}
