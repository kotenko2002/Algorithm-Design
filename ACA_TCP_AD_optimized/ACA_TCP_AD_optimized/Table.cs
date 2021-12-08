using System.Collections.Generic;
using System.Linq;
using System;

namespace ACA_TCP_AD_optimized
{
    public class Table
    {
        public double[,] DistanceMap { get; set; }
        public double[,] PheromonesMap { get; set; }

        public int CountOfCities { get; set; }
        public double Lmin { get; set; }
        public double BestPath { get; set; }

        public Table(int vertex)
        {
            Random random = new Random();
            CountOfCities = vertex;
            

            DistanceMap = new double[CountOfCities, CountOfCities];
            for (int i = 0; i < CountOfCities; i++)
                for (int j = 0; j < CountOfCities; j++)
                    if (i != j && DistanceMap[i, j] == 0 && DistanceMap[j, i] == 0)
                        DistanceMap[i, j] = DistanceMap[j, i] = random.Next(5, 150);

            PheromonesMap = new double[CountOfCities, CountOfCities];
            for (int i = 0; i < CountOfCities; i++)
                for (int j = 0; j < CountOfCities; j++)
                    if (i != j)
                        PheromonesMap[i, j] = 0.2;
            Lmin = 999999999;
        }
        public List<(int, int)> Iteration()
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
                ants[i].Sequence = new List<(int, int)>();
                WalkThroughCities(ants[i], citys[j]);
            }

            BestPath = ants.Min(x => x.Result);
            if (BestPath < Lmin)
                Lmin = BestPath;
            List<(int, int)> bestWay = ants.FirstOrDefault(x => x.Result == BestPath).Sequence;
            
            for (int i = 0; i < CountOfCities; i++)
                for (int j = 0; j < CountOfCities; j++)
                {
                    if (i == j)
                        continue;
                    else
                        PheromonesMap[i, j] = (1 - Constants.P) * PheromonesMap[i, j];
                }
            for (int i = 0; i < PheromonesMap.GetLength(0); i++)
                for (int j = 0; j < PheromonesMap.GetLength(0); j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < ants.Length; k++)
                        {
                            var result = ants[k].Sequence.Find(pair => pair.Item1 == i && pair.Item2 == j);
                            if (result != (0, 0))
                                PheromonesMap[i, j] += Lmin / ants[k].Result;
                        }
                    }
                }
            return bestWay;
        }
        public void WalkThroughCities(Ant ant, int startCity)
        {
            Random random = new Random();
            ant.Sequence.Add(new (startCity, 999));
            List<double> Pij = new List<double>();

            double sum = 0, totalSum = 0;
            for (int j = 0; j < CountOfCities; j++)
            {
                if (ant.Sequence.Where(s => s.Item1 == j).Take(1).ToArray().Length != 0)
                    continue;

                Pij.Add(Math.Pow(PheromonesMap[startCity, j], Constants.alpha) * Math.Pow((1 / DistanceMap[startCity, j]), Constants.beta));
                sum += Pij[Pij.Count - 1];
            }
            for (int j = 0; j < CountOfCities - ant.Sequence.Count; j++)
            {
                Pij[j] = Pij[j] / sum;
                totalSum += Pij[j];
                if (j != 0)
                    Pij[j] += Pij[j - 1];//перезаписываем гранцы с второго числа
            }

            List<double> bordersList = new List<double>();
            bordersList.Add(0);
            foreach (var item in Pij)
                bordersList.Add(item);
            double randomChoose = random.NextDouble();

            string[] tabooCheckList = new string[CountOfCities];
            for (int i = 0; i < ant.Sequence.Count; i++)
                tabooCheckList[ant.Sequence[i].Item1] = ant.Sequence[i].Item1.ToString();
            //
            int NextCity = 999;
            for (int i = 0; i < CountOfCities - ant.Sequence.Count; i++)
            {
                if (randomChoose >= bordersList[i] && randomChoose <= bordersList[i + 1])
                {
                    for (int k = 0; k < tabooCheckList.Length; k++)
                        if (tabooCheckList[k] == null)
                        {
                            NextCity = k;
                            break;
                        }
                    break;
                }
                for (int k = 0; k < tabooCheckList.Length; k++)
                    if (tabooCheckList[k] == null)
                    {
                        tabooCheckList[k] = k.ToString();
                        break;
                    }
            }
            List<(int, int)> NewBuffSequence = new List<(int, int)>();
            for (int i = 0; i < ant.Sequence.Count; i++)
            {
                if(ant.Sequence[i].Item2 != 999)
                    NewBuffSequence.Add(new(ant.Sequence[i].Item1, ant.Sequence[i].Item2));
                else
                {
                    if (NextCity == 999)
                        NextCity = ant.Sequence[0].Item1;// BreakPoint туть

                    NewBuffSequence.Add(new(ant.Sequence[i].Item1, NextCity));
                }
                    
            }
            ant.Sequence = NewBuffSequence;
            //теперь можем проверить поле Sequence
            ant.Result += DistanceMap[startCity, NextCity];
            if (ant.Sequence.Count != CountOfCities)
                WalkThroughCities(ant, NextCity);
        }
        public double GetLmin()
        {
            return 0;
        }
    }
}
