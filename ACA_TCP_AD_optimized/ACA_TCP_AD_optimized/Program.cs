using System.Collections.Generic;
using System;

namespace ACA_TCP_AD_optimized
{
    class Program
    {
        static void Main(string[] args)
        {
            TCP(200, 1000);
        }
        static void TCP(int vertex, int iterations)
        {
            Table table = new Table(vertex);
            List<(int, int)> sequence = new List<(int, int)>();
            for (int i = 0; i < iterations; i++)
            {
                if (i == iterations - 1)
                    sequence = table.Iteration();
                else
                    table.Iteration();
                Console.WriteLine($"Best path: {table.Lmin} at the {i}-th iteration");
            }
            for (int j = 0; j < sequence.Count; j++)
            {
                Console.Write($"({sequence[j].Item1}->{sequence[j].Item2}) ");
            }
        }
        static void ExampleOn20Vertex()
        {
            Table table = new Table(20);
            List<(int, int)> sequence = new List<(int, int)>();
            Print.PrintPheromonesMap(table);
            int iterations = 250;
            for (int i = 0; i < iterations; i++)
            {
                if (i == iterations - 1)
                    sequence = table.Iteration();
                else
                    table.Iteration();
                Console.WriteLine($"Pheromone map at the {i}-th iteration");
                Print.PrintPheromonesMap(table);
                
            }
            for (int j = 0; j < sequence.Count; j++)
            {
                Console.Write($"({sequence[j].Item1}->{sequence[j].Item2}) ");
            }
            Console.WriteLine($"\nBest path length: {table.Lmin}");
            
            Print.PrintDistanceMap(table);
        }
    }
}
