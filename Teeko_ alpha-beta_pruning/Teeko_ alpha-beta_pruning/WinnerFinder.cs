using System;
using System.Collections.Generic;
using System.Text;

namespace Teeko__alpha_beta_pruning
{
    public static class WinnerFinder
    {
        public static bool CheckWinner(char[,] array, char symbol)
        {
            if (CheckHorizont(array, symbol))
                return true;
            if (CheckDiagonals(array, symbol))
                return true;
            if (CheckVertical(array, symbol))
                return true;
            return false;
        }
        static bool CheckHorizont(char[,] array, char symbol)
        {
            char[] subArray = new char[] { symbol, symbol, symbol, symbol };
            List<char[]> horizontalList = new List<char[]>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                horizontalList.Add(new char[5]);
                for (int j = 0; j < array.GetLength(1); j++)
                    horizontalList[i][j] = array[i, j];
            }
            for (int i = 0; i < 5; i++)
            {
                if (isSubArray(horizontalList[i], subArray, horizontalList[i].Length, subArray.Length))
                    return true;
            }
            return false;
        }
        static bool CheckDiagonals(char[,] array, char symbol)
        {
            if (array[0, 3] == symbol && array[1, 2] == symbol && array[2, 1] == symbol && array[3, 0] == symbol)
                return true;
            if (array[1, 4] == symbol && array[2, 3] == symbol && array[3, 2] == symbol && array[4, 1] == symbol)
                return true;
            if (array[0, 4] == symbol && array[1, 3] == symbol && array[2, 2] == symbol && array[3, 1] == symbol)
                return true;
            if (array[4, 0] == symbol && array[3, 1] == symbol && array[2, 2] == symbol && array[1, 3] == symbol)
                return true;

            if (array[0, 1] == symbol && array[1, 2] == symbol && array[2, 3] == symbol && array[3, 4] == symbol)
                return true;
            if (array[1, 0] == symbol && array[2, 1] == symbol && array[3, 2] == symbol && array[4, 3] == symbol)
                return true;
            if (array[0, 0] == symbol && array[1, 1] == symbol && array[2, 2] == symbol && array[3, 3] == symbol)
                return true;
            if (array[4, 4] == symbol && array[3, 3] == symbol && array[2, 2] == symbol && array[1, 1] == symbol)
                return true;
            return false;
        }
        static bool CheckVertical(char[,] array, char symbol)
        {
            char[,] copyArray = new char[5, 5];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    copyArray[i, j] = array[i, j];

            int n = array.GetLength(0), m = array.GetLength(1);
            char[,] rotatedArray = new char[m, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    rotatedArray[j, n - i - 1] = copyArray[i, j];

            if (CheckHorizont(rotatedArray, symbol))
                return true;
            return false;
        }
        static bool isSubArray(char[] A, char[] B, int n, int m)
        {
            int i = 0, j = 0;
            while (i < n && j < m)
            {
                if (A[i] == B[j])
                {
                    i++;
                    j++;
                    if (j == m)
                        return true;
                }
                else
                {
                    i = i - j + 1;
                    j = 0;
                }
            }
            return false;
        }
    }
}
