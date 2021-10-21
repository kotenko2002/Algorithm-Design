using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_Laba1_part1
{
    public class Helper
    {
        public static bool Compare(int size, bool[,] board_a, bool[,] board_b)
        {
            int count = 0;
            bool[,] new_ar = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board_a[i, j] == board_b[i, j])
                        count++;
                }
            }
            if (count == (size * size))
                return true;
            return false;
        }
        public static bool[,] Copy(int size, bool[,] sampleToCopy)
        {
            bool[,] copy = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    copy[i, j] = sampleToCopy[i, j];
            }
            return copy;
        }
        public static bool[,] Create()
        {
            bool[,] result = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[i, j] = false;
                }
            }
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                result[i, random.Next(0, 8)] = true;
            }
            return result;
        }
    }
}
