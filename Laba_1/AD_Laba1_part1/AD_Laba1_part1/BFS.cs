using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_Laba1_part1
{
    class BFS
    {
        public Board Search(Board map, ref List<bool[,]> visited, ref int iterations, ref Queue<bool[,]> queue)
        {
            bool enter_key = true;
            while (enter_key)    
            {
                iterations++;
                enter_key = false;
                foreach (var item in visited)
                {
                    int prikol = map.Size;
                    if (Helper.Compare(map.Size, queue.Peek(), item))
                    {
                        queue.Dequeue();
                        enter_key = true;
                    }
                }
            }

            bool[,] copy = new bool[map.Size, map.Size];
            copy = Helper.Copy(map.Size, queue.Peek());

            for (int j = 0; j < map.Size; j++)
            {
                copy = Helper.Copy(map.Size, queue.Peek());
                for (int i = 0; i < map.Size; i++)
                    copy[i, j] = false;

                for (int i = 0; i < map.Size; i++)
                {
                    copy[i, j] = true;
                    if (map.Map[i, j] != true)
                        queue.Enqueue(Helper.Copy(map.Size, copy));
                    copy[i, j] = false;
                }
            }

            visited.Add(queue.Peek());
            return new Board(map.Size, queue.Dequeue());
        }
    }
}
