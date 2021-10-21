using System.Collections.Generic;
using System.Drawing;

namespace Fuking_RBFS
{
    public static class Heuristics
    {
        public static int F2(int[,] board)
        {
            int sum = 0;
            foreach (var queenPosition in GetAllQueens(board))
                sum += QueensConflict(board, queenPosition);
            return sum / 2;
        }

        public static int QueensConflict(int[,] board, Point initialQueenPosition)
        {
            int result = 0;

            foreach (var queen_pos in GetAllQueens(board))
            {
                if (queen_pos == initialQueenPosition)
                    continue;
                if (queen_pos.X == initialQueenPosition.X)
                    result += 1;
                else if (queen_pos.Y == initialQueenPosition.Y)
                    result += 1;
                else if (queen_pos.X - initialQueenPosition.X == queen_pos.Y - initialQueenPosition.Y)
                    result += 1;
                else if (-1 * (queen_pos.X - initialQueenPosition.X) == queen_pos.Y - initialQueenPosition.Y)
                    result += 1;
            }

            return result;
        }

        public static List<Point> GetAllQueens(int[,] board)
        {
            List<Point> result = new List<Point>();

            for (int y = 0; y < board.GetLongLength(0); y++)
            {
                for (int x = 0; x < board.GetLongLength(0); x++)
                {
                    if (board[x, y] == 1)
                        result.Add(new Point(x, y));
                }
            }

            return result;
        }
    }
}
