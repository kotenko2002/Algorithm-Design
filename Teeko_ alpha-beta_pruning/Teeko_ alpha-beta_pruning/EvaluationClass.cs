﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Teeko__alpha_beta_pruning
{
    public static class EvaluationClass
    {
        public static List<(string, long)> evaluationCriteriaForComputer = new List<(string, long)>()
        {
                 ($"{'C'}{'C'}{'C'}{'C'}", 100000000000),
                ($"{'0'}{'C'}{'C'}{'C'}{'0'}", 50000000000 - 1),
                ($"{'0'}{'C'}{'C'}{'C'}", 100000),
                ($"{'C'}{'0'}{'C'}{'C'}", 100000),
                ($"{'C'}{'C'}{'0'}{'C'}", 100000),
                ($"{'C'}{'C'}{'C'}{'0'}", 100000),
                ($"{'0'}{'C'}{'C'}{'0'}", 400),
                ($"{'C'}{'C'}{'0'}", 50),
                ($"{'0'}{'C'}{'C'}", 50)
        };
        public static List<(string, long)> evaluationCriteriaForPlayer = new List<(string, long)>()
            {
            ($"{'P'}{'P'}{'P'}{'P'}", 100000000000),
                ($"{'0'}{'P'}{'P'}{'P'}{'0'}", 50000000000 - 1),
                ($"{'0'}{'P'}{'P'}{'P'}", 100000),
                ($"{'P'}{'0'}{'P'}{'P'}", 100000),
                ($"{'P'}{'P'}{'0'}{'P'}", 100000),
                ($"{'P'}{'P'}{'P'}{'0'}", 100000),
                ($"{'0'}{'P'}{'P'}{'0'}", 400),
                ($"{'P'}{'P'}{'0'}", 50),
                ($"{'0'}{'P'}{'P'}", 50)
            };
        public static (byte, byte)[] diagonals = new (byte, byte)[]
            {
                (0,3),  (1,2),  (2,1),  (3,0), (8,8),
                (0,4),  (1,3),  (2,2),  (3,1), (4,0), (8,8),
                (1,4),  (2,3),  (3,2),  (4,1), (8,8),

                (0,1),  (1,2),  (2,3),  (3,4), (8,8),
                (0,0),  (1,1),  (2,2),  (3,3), (4,4), (8,8),
                (1,0),  (2,1),  (3,2),  (4,3), (8,8),
            };
        public static long F(char[,] array)
        {
            long playerPoints = 0, ComputerPoints = 0;

            string strArray = GetStringFromArray(array);

            for (int i = 0; i < evaluationCriteriaForComputer.Count; i++)
            {
                if (strArray.Contains(evaluationCriteriaForComputer[i].Item1))
                    ComputerPoints += evaluationCriteriaForComputer[i].Item2;
                if (strArray.Contains(evaluationCriteriaForPlayer[i].Item1))
                    playerPoints += evaluationCriteriaForPlayer[i].Item2;
            }
            //Console.WriteLine($"Очки игрока: {playerPoints}\nОчки ИИ: {ComputerPoints}");
            return ComputerPoints - playerPoints;
        }
        // мб порезать строку на 3 шт. т-е горизонталь, вертикаль, диагональ главная и побочная
        static string GetStringFromArray(char[,] array)
        {
            string sequence = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    sequence += array[i, j];
                sequence += "-";
            }
            int n = array.GetLength(0);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    sequence += array[n - j - 1, i];
                sequence += "-";
            }
            sequence += "-";
            for (int i = 0; i < diagonals.Length; i++)
            {
                if (diagonals[i].Item1 != 8)
                    sequence += array[diagonals[i].Item1, diagonals[i].Item2];
                else
                    sequence += "-";
            }
            return sequence;
        }
    }
}
