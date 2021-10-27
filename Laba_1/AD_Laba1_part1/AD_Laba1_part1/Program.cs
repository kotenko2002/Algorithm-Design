using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AD_Laba1_part1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<bool[,]> visited = new List<bool[,]>();
            Queue<bool[,]> queue = new Queue<bool[,]>();

            Board board = new Board(8, Helper.Create());
            board.Draw();

            Console.WriteLine($"Количество конфликтных ситуаций: {board.СonflictsCount()}\n");

           
            queue.Enqueue(board.Map);

            int iterations = 0;
            BFS bfs = new BFS();
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            while (board.СonflictsCount() != 0)
                board = bfs.Search(board, ref visited, ref iterations, ref queue);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            board.Draw();
            Console.WriteLine($"Количество инераций : {iterations}");
            Console.WriteLine($"Количество состояний: {visited.Count}");
            Console.WriteLine($"Длина очереди: {queue.Count}");
            Console.WriteLine(elapsedTime, "RunTime");

        }
    }
}
