using System;
using System.Collections.Generic;
using System.Text;

namespace Teeko__alpha_beta_pruning
{
    public static class AI
    {
        public static (int X, int Y)[] fieldsToMove = new (int X, int Y)[]
        {
            (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)
        };
        public static (char[,], long) MiniMaxMove(char[,] array, int depth, long alpha, long beta, bool miximizigPlayerTurnToMove)
        {
            if (depth == 0 || WinnerFinder.CheckWinner(array, 'C'))
                return (array, EvaluationClass.FMove(array));

            if (miximizigPlayerTurnToMove)
            {
                (char[,], long) maxEval = (null, -999999999999999999);
                List<char[,]> children = GetAllChildren(array, 'C');// дети ПК
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMaxMove(child, depth - 1, alpha, beta, false);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    maxEval = (maxEval.Item2 < eval.Item2) ? (buffArray, eval.Item2) : maxEval;

                    alpha = (alpha < eval.Item2) ? eval.Item2 : alpha;
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                (char[,], long) minEval = (null, 999999999999999999);
                List<char[,]> children = GetAllChildren(array, 'P');// дети Игрока, вот так и становятся отцами...
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMaxMove(child, depth - 1, alpha, beta, true);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    minEval = (minEval.Item2 > eval.Item2) ? (buffArray, eval.Item2) : minEval;

                    beta = (beta > eval.Item2) ? eval.Item2 : beta;
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        public static List<char[,]> GetAllChildren(char[,] array, char symbol)
        {
            List<char[,]> children = new List<char[,]>();
            List<(int X, int Y)> coordinates = GetAllCheckers(array, symbol);

            foreach (var checker in coordinates)
            {
                foreach (var item in fieldsToMove)
                {
                    if ((item.X + checker.X >= 0 && item.X + checker.X <= 4) && (item.Y + checker.Y >= 0 && item.Y + checker.Y <= 4)
                        && array[item.X + checker.X, item.Y + checker.Y] == '0')
                    {
                        char[,] child = new char[5, 5];
                        Array.Copy(array, 0, child, 0, array.Length);
                        child[checker.X, checker.Y] = '0';
                        child[item.X + checker.X, item.Y + checker.Y] = symbol;
                        children.Add(child);
                    }
                }
            }
            return children;
        }

        public static (char[,], long) MiniMaxPut(char[,] array, int depth, long alpha, long beta, bool miximizigPlayerTurnToMove)
        {
            if (depth == 0 || WinnerFinder.CheckWinner(array, 'C'))
                return (array, EvaluationClass.FPut(array));

            if (miximizigPlayerTurnToMove)
            {
                (char[,], long) maxEval = (null, -999999999999999999);
                List<char[,]> children = GetAllChildrenToPut(array, 'C');// дети ПК
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMaxPut(child, depth - 1, alpha, beta, false);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    maxEval = (maxEval.Item2 < eval.Item2) ? (buffArray, eval.Item2) : maxEval;

                    alpha = (alpha < eval.Item2) ? eval.Item2 : alpha;
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                (char[,], long) minEval = (null, 999999999999999999);
                List<char[,]> children = GetAllChildrenToPut(array, 'P');// дети Игрока, вот так и становятся отцами...
                foreach (var child in children)
                {
                    (char[,], long) eval = MiniMaxPut(child, depth - 1, alpha, beta, true);
                    char[,] buffArray = new char[5, 5];
                    Array.Copy(child, 0, buffArray, 0, array.Length);
                    minEval = (minEval.Item2 > eval.Item2) ? (buffArray, eval.Item2) : minEval;

                    beta = (beta > eval.Item2) ? eval.Item2 : beta;
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        public static List<char[,]> GetAllChildrenToPut(char[,] array, char symbol)
        {
            List<char[,]> children = new List<char[,]>();
            for (int i = 0; i < array.GetLength(0); i++)
            {

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == '0')
                    {
                        char[,] buffArray = new char[5, 5];
                        Array.Copy(array, 0, buffArray, 0, array.Length);
                        buffArray[i, j] = symbol;
                        children.Add(buffArray);
                    }

                }
            }
            return children;
        }

        public static List<(int X, int Y)> GetAllCheckers(char[,] array, char symbol)
        {
            List<(int X, int Y)> coordinates = new List<(int X, int Y)>();

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j] == symbol)
                        coordinates.Add((i, j));

            return coordinates;
        }
    }
}
