using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_Laba1_part1
{
    public class Board
    {
        public int Size { get; set; }
        public bool[,] Map { get; set; }
        public Board(int Size, bool[,] Map)
        {
            this.Size = Size;
            this.Map = Map;
        }
        public void Draw()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < Size; j++)
                {
                    
                    if(i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Print(Map[i,j]);
                        }
                        else if (j % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Print(Map[i, j]);
                        }
                    }
                    if (i % 2 == 1)
                    {
                        if (j % 2 == 1)
                        {
                            
                            Console.BackgroundColor = ConsoleColor.Black;
                            Print(Map[i, j]);
                        }
                        else if (j % 2 == 0)
                        {
                            
                            Console.BackgroundColor = ConsoleColor.Green;
                            Print(Map[i, j]);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public void Print(bool Map)
        {
            if (Map)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Q" + " ");
            }
                
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("—" + " ");
            }
                
        }
        public int СonflictsCount()
        {
            int result = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int row = 0; row < Size; row++)
                    {
                        if (Map[i, j] && Map[i, row] && row != j)
                            result++;
                        if (row != 0)
                        {
                            if (i - row >= 0 && j - row >= 0 && Map[i, j] && Map[i - row, j - row])
                                result++;
                            if (i - row >= 0 && j + row < Size && Map[i, j] && Map[i - row, j + row])
                                result++;
                            if (i + row < Size && j - row >= 0 && Map[i, j] && Map[i + row, j - row])
                                result++;
                            if (i + row < Size && j + row < Size && Map[i, j] && Map[i + row, j + row])
                                result++;
                        }
                    }
                }
            }
            return result / 2;
        }
    }
}
