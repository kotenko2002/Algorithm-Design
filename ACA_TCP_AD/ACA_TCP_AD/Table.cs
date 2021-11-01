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
        public List<(int, int)> BestSequence { get; set; }
        //public int Height { get; set; }
        //public int Width { get; set; }
        public int CountOfCities { get; set; }
        public double Lmin { get; set; }
        public Table(int height, int width, int vertex)
        {
            //Height = height;
            //Width = width;
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

            Lmin = Iteration().OrderBy(x => x.Result).First().Result;
            //Console.WriteLine($"Lmin = {Lmin} ");
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
                    if(Math.Round(DistanceMap[i, j], 0).ToString().Length == 1)
                        Console.Write(Math.Round(DistanceMap[i, j], 0) + "  ");
                    else
                        Console.Write(Math.Round(DistanceMap[i, j], 0) + " ");
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
                        Console.Write(Math.Round(PheromonesMap[i, j],2) + " ");
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
                return result;
            }
            else
                throw new Exception("Map не инициализирован в методе FindCity()");
        }
        public Ant[] Iteration()
        {
            Ant[] ants = new Ant[Constants.M];
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
                //мб ошибка снизу
                ants[i].Result += GetDistanceBetweenCities(FindCityCoordinates(ants[i].TabooList[ants[i].TabooList.Count - 1].CityNumber), FindCityCoordinates(citys[j]));
                ants[i].TabooList.Add(new Taboo(citys[j]));
            }
            return ants;
        }
        public void GetBestWay()
        {
            Ant[] generation = Iteration();
            //обновляем список феромонов
            for (int i = 0; i < CountOfCities; i++)
                for (int j = 0; j < CountOfCities; j++)
                {
                    if (i == j)
                        continue;
                    else
                        PheromonesMap[i, j] = (1 - Constants.P) * PheromonesMap[i, j];
                }
            for (int i = 0; i < generation.Length; i++)
            {
                generation[i].Sequence = new List<(int, int)>();
                for (int j = 0; j < generation[i].TabooList.Count - 1; j++)
                    generation[i].Sequence.Add(((generation[i].TabooList[j].CityNumber, generation[i].TabooList[j + 1].CityNumber)));
            }
            //for (int i = 0; i < generation[0].Sequence.Count; i++)
            //    Console.Write($"[{generation[0].Sequence[i].Item1} {generation[0].Sequence[i].Item2}] ");

            for (int i = 0; i < PheromonesMap.GetLength(0); i++)
                for (int j = 0; j < PheromonesMap.GetLength(0); j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < generation.Length; k++)
                        {
                            var result = generation[k].Sequence.Find(pair => pair.Item1 == i && pair.Item2 == j);
                            if (result != (0, 0))
                                PheromonesMap[i, j] += Lmin / generation[k].Result;
                        }
                    }
                }
            //Console.WriteLine("Лучший маршрут:" + generation.OrderBy(x => x.Result).First().Result + " км");
            Lmin = generation.OrderBy(x => x.Result).First().Result;

            BestSequence = generation.OrderBy(x => x.Result).First().Sequence;
        }
        public void FindSolution(Ant ant, int startCity)
        {
            Random random = new Random();
            ant.TabooList.Add(new Taboo(startCity));
            List<double> Pij = new List<double>();

            double sum = 0, totalSum = 0;
            for (int j = 0; j < CountOfCities; j++)
            {
                if (ant.TabooList.SingleOrDefault(s => s.CityNumber == j) != null)
                    continue;

                Pij.Add(Math.Pow(PheromonesMap[startCity, j], Constants.alpha) * Math.Pow((1 / DistanceMap[startCity, j]), Constants.beta));//заменить на альфа и бета
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
            double randomChoose = random.NextDouble();

            string[] feature = new string[CountOfCities];
            for (int i = 0; i < ant.TabooList.Count; i++)
                feature[ant.TabooList[i].CityNumber] = ant.TabooList[i].CityNumber.ToString();

            int numberOfCityToGo = 999;
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
            if(numberOfCityToGo != 999)
                ant.Result += GetDistanceBetweenCities(FindCityCoordinates(startCity), FindCityCoordinates(numberOfCityToGo));
            if (ant.TabooList.Count != CountOfCities)
                FindSolution(ant, numberOfCityToGo);
        }
        public double GetDistanceBetweenCities(Point start, Point end)
        {
            Point vector = new Point(end.X - start.X, end.Y - start.Y);
            double result = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            return result;
        }
    }
}
