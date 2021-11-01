using System;

namespace ACA_TCP_AD_optimized
{
    public static class Print
    {
        public static void PrintDistanceMap(Table table)
        {   
            for (int i = 0; i < table.DistanceMap.GetLength(0); i++)
            {
                for (int j = 0; j < table.DistanceMap.GetLength(1); j++)
                {
                    string indent = table.DistanceMap[i, j].ToString().Length == 1 ? "  " : " ";
                    Console.Write(table.DistanceMap[i, j] + indent);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void PrintPheromonesMap(Table table)
        {
            for (int i = 0; i < table.PheromonesMap.GetLength(0); i++)
            {
                for (int j = 0; j < table.PheromonesMap.GetLength(1); j++)
                {
                    double value = Math.Round(table.PheromonesMap[i, j], 3);
                    switch (value.ToString().Length)
                    {
                        case 1:
                            Console.Write("   " + value + "    ");
                            break;
                        case 2:
                            Console.Write(" " + value + "     ");
                            break;
                        case 3:
                            Console.Write(" " + value + "    ");
                            break;
                        case 4:
                            Console.Write(" " + value + "   ");
                            break;
                        case 5:
                            Console.Write(" " + value + "  ");
                            break;
                        case 6:
                            Console.Write(" " + value + " ");
                            break;
                        case 7:
                            Console.Write(" " + Math.Round(value, 2) + " ");
                            break;
                        case 8:
                            Console.Write(" " + Math.Round(value, 1) + " ");
                            break;
                        default:
                            Console.WriteLine("Error in PrintPheromonesMap()");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
