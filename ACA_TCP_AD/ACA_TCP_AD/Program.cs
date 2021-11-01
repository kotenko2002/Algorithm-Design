using System;
using System.Diagnostics;

namespace ACA_TCP_AD
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(50, 60, 30);
            for (int i = 0; i < 30; i++)
            {
                table.GetBestWay();
                Console.WriteLine(i + "-aя итерация " + table.Lmin);
            }
        }
        static void City40()
        {
            Table table = new Table(30, 55, 40);
            table.PrintMap();
            table.PrintDistanceMap();
            table.PrintPheromonesMap();

            for (int i = 0; i < 30; i++)
            {
                table.GetBestWay();
                table.PrintPheromonesMap();
                Console.WriteLine(i + "-aя итерация " + table.Lmin);

            }

            Console.Write("Лучший путь: ");
            Console.Write("[ ");
            foreach (var item in table.BestSequence)
            {
                Console.Write($"({item.Item1}->{item.Item2}) ");
            }
            Console.WriteLine("] ");
            Console.WriteLine("Длинна лучшего пути: " + Math.Round(table.Lmin, 0));
        }
    }
}
