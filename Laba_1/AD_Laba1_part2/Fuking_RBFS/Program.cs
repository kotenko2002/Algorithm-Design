using System;
using System.Diagnostics;

namespace Fuking_RBFS
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] testArray1 = {
            { 0,0,0,0,0,0,1,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,1},
            { 1,0,0,0,0,1,0,0},
            { 0,1,1,0,1,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,1,0,0,0,0}};
            int[,] testArray2 = {
            { 0,1,0,1,1,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,0,0},
            { 1,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,1,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,1,0,0,0,0,0},
            { 0,0,0,0,0,0,0,1}};
            int[,] testArray3 = {
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0}};
            int[,] array = new int[8, 8];
            Random random = new Random();
            for (int i = 0; i < array.GetLongLength(0); i++)
                array[random.Next(0, 8), i] = 1;
            Node node = new Node(array, null, 1);
            Print(node.Map);
            Console.WriteLine($"Количество конфликтов: {Heuristics.F2(node.Map)}");
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = RBFS.RecursiveBestFirstSearch(node, 999999999);
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine();
            Print(result.Map);
            Console.WriteLine($"КОНФЛИКТЫ {Heuristics.F2(result.Map)}");
            Console.WriteLine($"Количество инераций: {RBFS.Iteration}");
            Console.WriteLine($"Количество состояний: {RBFS.States}");
            Console.WriteLine($"Глубина поиска: {result.Depth}");
            Console.WriteLine(elapsedTime, "RunTime");
        }
        static void Print(int[,] map)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Draw(map[i, j]);
                        }
                        else if (j % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Draw(map[i, j]);
                        }
                    }
                    if (i % 2 == 1)
                    {
                        if (j % 2 == 1)
                        {

                            Console.BackgroundColor = ConsoleColor.Black;
                            Draw(map[i, j]);
                        }
                        else if (j % 2 == 0)
                        {

                            Console.BackgroundColor = ConsoleColor.Green;
                            Draw(map[i, j]);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        static void Draw(int n)
        {
            if (n == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Q" + " ");
            }
            else if (n == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-" + " ");
            }
        }
    }
}
