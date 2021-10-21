using FFImageLoading.Concurrency;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuking_RBFS
{
    class RBFS
    {
        public static int Iteration = 0;
        public static int States = 0;
        public static Node RecursiveBestFirstSearch(Node node, int f_limit)
        {
            Iteration++;
            SimplePriorityQueue<Node> queue = new SimplePriorityQueue<Node>();

            if (Heuristics.F2(node.Map) == 0)
                return node;

            if (node.Depth > 9)
                return null;

            node.children = GetAllChildren(node);
            States += 56;

            if (node.children == null)
                return null;

            foreach (var child in node.children)
            {
                int f_n = child.Depth + (Heuristics.F2(child.Map)); // g(s) + h(s)
                queue.Enqueue(child, f_n); 
            }

            while (true)
            {
                var best = queue.Dequeue();

                if (best.Depth + (Heuristics.F2(best.Map)) > f_limit)
                    return null;
                else
                {
                    var alternative = queue.Dequeue();
                    var result = RecursiveBestFirstSearch(best, alternative.Depth + (Heuristics.F2(alternative.Map)));

                    if (result != null)
                        return result;
                }
            }
        }
        public static List<Node> GetAllChildren(Node node)
        {
            List<int[,]> children = new List<int[,]>();

            foreach (var queenPosition in Heuristics.GetAllQueens(node.Map))
            {
                int queenColumn = queenPosition.Y;
                int queenRow = queenPosition.X;

                for (int i = 0; i < 8; i++)
                {
                    if (i != queenRow)
                    {
                        int[,] boardCopy = new int[8, 8];
                        Array.Copy(node.Map, 0, boardCopy, 0, node.Map.Length);
                        boardCopy[i, queenColumn] = 1;
                        boardCopy[queenRow, queenColumn] = 0;

                        children.Add(boardCopy);
                    }
                }
            }

            List<Node> result = new List<Node>();

            foreach (var child in children)
                result.Add(new Node(child, node, node.Depth + 1));

            return result;
        }
    }
}
