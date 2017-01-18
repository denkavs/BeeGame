using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    public class Bee
    {
        public Bee(int id, int lifeSpan, BeeType type)
        {
            this.Id = id;
            this.lifeSpan = lifeSpan;
            this.Type = type;
        }

        private int lifeSpan;
        public int Id { get; private set; }
        public BeeType Type { get; private set; }
        public int LifeSpan { get {
                return this.lifeSpan;
            }
        }
        public virtual bool IsAlive()
        {
            return this.LifeSpan > 0;
        }
        public void RemoveLifeSpan(int count)
        {
            this.lifeSpan = this.lifeSpan - count;
            if (this.lifeSpan < 0)
                this.lifeSpan = 0;
        }
    }
}
