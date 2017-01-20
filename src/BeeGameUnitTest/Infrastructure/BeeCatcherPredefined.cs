using GameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic.Model;

namespace BeeGameUnitTest.Infrastructure
{
    class BeeCatcherPredefined : IBeeCatcher
    {
        private Queue<int> selIndexes;

        public BeeCatcherPredefined(params int [] selIndexes)
        {
            this.selIndexes = new Queue<int>(selIndexes);
        }

        public virtual Bee Hit(List<Bee> bees)
        {
            if (bees == null || bees.Count == 0)
                return null;

            int index = this.selIndexes.Dequeue();
            return bees[index];
        }
    }
}
