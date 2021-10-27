using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ACA_TCP_AD
{
    public class Ant
    {
        public int Number { get; set; }
        public int Result { get; set; }
        public string Sequence { get; set; }
        public List<Taboo> TabooList { get; set; }

        public Ant(int Number)
        {
            this.Number = Number;
        }
    }
}
