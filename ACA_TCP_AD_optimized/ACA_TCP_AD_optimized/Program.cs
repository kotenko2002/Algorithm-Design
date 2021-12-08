using System.Collections.Generic;
using System;

namespace ACA_TCP_AD_optimized
{
    class Program
    {
        static void Main(string[] args)
        {
            int vertex = 40;   //кол. городов
            int iteration = 40; //кол. итераций
            
            for (int i = 0; i < 10; i++)
            {
                TCP(vertex, iteration);
                Constants.alpha += 1;//автоинкремент к параметру, в данном случаи альфа, потом поставишь бета
                //потом сотавишь 0.1 вместо 1 и бета поменяешь на коеф испарения(Р)
                //потом сотавишь 1 вместо 0.1 и коеф испарения(Р) поменяешь на кол. муравьев(М)
            }
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
            }
            Console.WriteLine($"Best path: {table.Lmin}; A = {Constants.alpha}, B = {Constants.beta}, P = {Constants.P}, M = {Constants.M}");
        }
        static void ExampleOn20Vertex()
        {
            Table table = new Table(20);
            List<(int, int)> sequence = new List<(int, int)>();
            Print.PrintPheromonesMap(table);
            int iterations = 100;
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
            Console.WriteLine($"\nBest path length: {table.BestPath}");
            
            Print.PrintDistanceMap(table);
        }
        static void Prikol()
        {
            Console.ReadKey();
            Table table = new Table(20);
            List<(int, int)> sequence = new List<(int, int)>();
            Print.PrintPheromonesMap(table);
            System.Threading.Thread.Sleep(50);
            Console.Clear();
            int iterations = 101;
            for (int i = 0; i < iterations; i++)
            {
                if (i == iterations - 1)
                    sequence = table.Iteration();
                else
                    table.Iteration();
                
                Print.PrintPheromonesMap(table);
                Console.WriteLine($"Pheromone map at the {i}-th iteration");
                System.Threading.Thread.Sleep(50);
                Console.Clear();
                
            }

            for (int j = 0; j < sequence.Count; j++)
            {
                Console.Write($"({sequence[j].Item1}->{sequence[j].Item2}) ");
            }
            Console.WriteLine($"\nBest path length: {table.BestPath}");
            Print.PrintPheromonesMap(table);
            Print.PrintDistanceMap(table);
        }
    }
}


