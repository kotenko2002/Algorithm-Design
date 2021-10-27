using System;

namespace ACA_TCP_AD
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(7,10,6);

            table.PrintMap();
            //table.PrintDistanceMap();
            //table.PrintPheromonesMap();

            table.GetBestWay();
            
        }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
