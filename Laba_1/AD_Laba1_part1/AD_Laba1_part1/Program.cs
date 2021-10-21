using System;
using System.Collections.Generic;


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
            while (board.СonflictsCount() != 0)
                board = bfs.Search(board, ref visited, ref iterations, ref queue);

            board.Draw();
            Console.WriteLine($"Количество рассмотенных состояний: {visited.Count}");
            Console.WriteLine($"Количество инераций в BFS: {iterations}");
            Console.WriteLine($"Длина очереди, где записаны развёрнутые потомки: {queue.Count}\n");
            
        }
    }
}
