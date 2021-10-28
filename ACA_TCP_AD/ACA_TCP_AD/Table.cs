using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace ACA_TCP_AD
{
    public class Table
    {
        public Cell[,] Map { get; set; }
        public double[,] DistanceMap { get; set; }
        public double[,] PheromonesMap { get; set; }
        
        public int Height { get; set; }
        public int Width { get; set; }
        public int CountOfCities { get; set; }
        public Table(int height, int width, int vertex)
        {
            Height = height;
            Width = width;
            CountOfCities = vertex;
            Map = new Cell[height, width];

            Random random = new Random();
            int k = 0;
            while (k < vertex)
            {
                int i = random.Next(0, height - 1), j = random.Next(0, width - 1);
                if (Map[i,j] == null)
                {
                    Map[i, j] = new Cell(k, new Point(i, j));
                    k++;
                }
            }

            DistanceMap = new double[vertex, vertex];
            for (int i = 0; i < vertex; i++)
                for (int j = 0; j < vertex; j++)
                {
                    if (i == j)
                        DistanceMap[i, j] = 9999999999999999999;
                    else
                    {
                        Point start = FindCityCoordinates(i), end = FindCityCoordinates(j);
                        Point vector = new Point(end.X - start.X, end.Y - start.Y);
                        double result = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
                        DistanceMap[i, j] = result;
                    }
                }

            PheromonesMap = new double[vertex, vertex];
            for (int i = 0; i < vertex; i++)
                for (int j = 0; j < vertex; j++)
                {
                    if (i == j)
                        PheromonesMap[i, j] = 0;
                    else
                        PheromonesMap[i, j] = 0.2;
                }
        }
        public void PrintMap()
        {
            if (Map != null)
            {
                for (int i = 0; i < Map.GetLength(0); i++)
                {
                    for (int j = 0; j < Map.GetLength(1); j++)
                    {
                        if(Map[i,j] != null)
                        {
                            if(Map[i, j].CityNumber < 10)
                                Console.Write(Map[i, j].CityNumber + "  ");
                            else
                                Console.Write(Map[i, j].CityNumber + " "); ;
                        }
                        else
                        {
                            Console.Write("-" + "  "); ;
                        }
                    }
                    Console.WriteLine();
                }
            }
            else
                throw new Exception("Map не инициализирован в методе PrintMap()");
        }
        public void PrintDistanceMap()
        {
            for (int i = 0; i < DistanceMap.GetLength(0); i++)
            {
                for (int j = 0; j < DistanceMap.GetLength(0); j++)
                {
                    Console.Write(DistanceMap[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void PrintPheromonesMap()
        {
            for (int i = 0; i < PheromonesMap.GetLength(0); i++)
            {
                for (int j = 0; j < PheromonesMap.GetLength(0); j++)
                {
                    if(PheromonesMap[i, j] == 0)
                        Console.Write(" " + PheromonesMap[i, j] + "  ");
                    else
                        Console.Write(PheromonesMap[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public Point FindCityCoordinates(int number)
        {
            if (Map != null)
            {
                List<Cell> list = Map.Cast<Cell>().ToList();
                var result = list.Find(x => x != null && x.CityNumber == number).Coordinates;
                //Console.WriteLine($"координаты города №{number}: [{result.X},{result.Y}]");
                return result;
            }
            else
                throw new Exception("Map не инициализирован в методе FindCity()");
        }
        public void GetBestWay()
        {
            Ant[] ants = new Ant[5];
            int[] citys = new int[CountOfCities];
            for (int i = 0; i < citys.Length; i++)
                citys[i] = i;
            for (int i = 0, j = 0; i < ants.Length; i++, j++)
            {
                if (j == citys.Length)
                    j = 0;
                ants[i] = new Ant(i);
                ants[i].TabooList = new List<Taboo>();  
                FindSolution(ants[i], citys[j]);         
                Console.WriteLine();
                for (int k = 0; k < ants[i].TabooList.Count; k++)
                    Console.Write(ants[i].TabooList[k].CityNumber + "-> ");
                Console.WriteLine(i + "-ый муравей закончил пробежку");
            }
        }
        public void FindSolution(Ant ant, int startCity)
        {
            //Console.Write("я в городе " + startCity + " ");
            Random random = new Random();
            ant.TabooList.Add(new Taboo(startCity));

            List<double> Pij = new List<double>();

            double sum = 0, totalSum = 0;
            for (int j = 0; j < CountOfCities; j++)
            {
                if (ant.TabooList.SingleOrDefault(s => s.CityNumber == j) != null)
                    continue;

                Pij.Add(Math.Pow(PheromonesMap[startCity, j], 1) * Math.Pow((1 / DistanceMap[startCity, j]), 1));//заменить на альфа и бета
                sum += Pij[Pij.Count - 1];
            }
            for (int j = 0; j < CountOfCities - ant.TabooList.Count; j++)
            {
                Pij[j] = Pij[j] / sum;
                totalSum += Pij[j];
                if (j != 0)
                    Pij[j] += Pij[j - 1];
            }

            List<double> bordersList = new List<double>();
            bordersList.Add(0);
            foreach (var item in Pij)
                bordersList.Add(item);
            //Console.Write("[");
            //foreach (var item in bordersList)
            //    Console.Write(item + " ");
            //Console.Write("]");
            double randomChoose = random.NextDouble();
            //Console.WriteLine("\nRandom number: " + randomChoose);

            string[] feature = new string[CountOfCities];
            for (int i = 0; i < ant.TabooList.Count; i++)// пока хз зачем...
                feature[ant.TabooList[i].CityNumber] = ant.TabooList[i].CityNumber.ToString();

            int numberOfCityToGo = 999;//убрать присвоение значения
            for (int i = 0; i < CountOfCities - ant.TabooList.Count; i++)
            {
                if (randomChoose >= bordersList[i] && randomChoose <= bordersList[i + 1])
                {
                    for (int k = 0; k < feature.Length; k++)
                        if (feature[k] == null)
                        {
                            numberOfCityToGo = k;
                            break;
                        }
                    break;
                }
                for (int k = 0; k < feature.Length; k++)
                    if (feature[k] == null)
                    {
                        feature[k] = k.ToString();
                        break;
                    }          
            }
            //Console.WriteLine("Пиздуем в горд " + numberOfCityToGo + " ");
            if (ant.TabooList.Count != CountOfCities)
                FindSolution(ant, numberOfCityToGo);
        }
    }
}
