using System.Collections.Generic;

namespace ACA_TCP_AD_optimized
{
    public class Ant
    {
        public int Number { get; set; }
        public double Result { get; set; }
        public List<(int, int)> Sequence { get; set; }
        public Ant(int Number)
        {
            this.Number = Number;
        }
    }
}
