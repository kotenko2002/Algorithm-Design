using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA_TCP_AD_optimized
{
    public static class Constants
    {
        public static int alpha = 1;        //[1-10] //ориентирование на ферамоны
        public static int beta = 1;         //[1-10] мб[1-12] //ориентирование на растояние
        public static double P = 0.2;       //[0.1-0.9] //испарение
        public static int M = 35;           //[10,20,30,40,50,60,70,80] //кол. муравьев
                                            //например если лучшие резульататы 40 и 50 муравьев, то запускаешь ещё раз с 
                                            //M = [40-50]
        // начинай с A=[1-10] B=1 P = 0.2 M = 35
    }
}
