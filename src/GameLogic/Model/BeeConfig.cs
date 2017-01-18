using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Model
{
    public class BeeConfig
    {
        public BeeType Type { get; set; }
        public int Deduction { get;set;}
        public int LifeSpan { get; set; }
        public int Count { get; set; }
    }
}
