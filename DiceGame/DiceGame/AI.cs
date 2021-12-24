using System;
using System.Collections.Generic;
using System.Text;

namespace DiceGame
{
    public static class AI
    {
        public static int FullGroup = 36;
        public static List<(int, double)> distributionSeries = new List<(int, double)>()
        {
            (2, 1), (3, 2), (4, 3), (5, 4), (6, 5), (7, 6),
            (8, 5), (9, 4), (10, 3), (11, 2), (12, 1)
        };

        public static bool GetComputerNeedToRollFirst(int playerPoints, int computerPoints)
        {
            if (playerPoints > computerPoints || computerPoints < 10)
                return true;
            List<int> computerChildren = GetAllChildren(computerPoints);
            List<int> playerChildren = GetAllChildren(playerPoints);

            int playerWin = 0, playerOverdo = 0;
            foreach (var computerPoint in computerChildren)
                if (GetComputerNeedToRollSecond(computerPoint, playerPoints))
                    playerWin++;
            foreach (var playerPoint in playerChildren)
                if (playerPoint > 21)
                    playerOverdo++;



            if (playerWin > (FullGroup / 2))
                return true;
            if (playerOverdo > (FullGroup / 2))
                return false;
            else
            {
                int PCneedToRoll = 0;
                foreach (var playerPoint in playerChildren)
                {
                    if (GetComputerNeedToRollFirst(playerPoint, computerPoints))
                        PCneedToRoll++;
                }
                if (PCneedToRoll >= (FullGroup / 2))
                    return true;
                else
                    return false;
            }
        }

        public static bool GetComputerNeedToRollSecond(int playerPoints, int computerPoints)
        {
            List<int> playerChildren = GetAllChildren(playerPoints);
            int playerWin = 0, playerOverdo = 0;
            foreach (var playerPoint in playerChildren)
            {
                if (playerPoint > computerPoints && playerPoint <= 21)
                    playerWin++;
                if (playerPoint > 21)
                    playerOverdo++;
            }
            if (playerWin > (FullGroup / 2))
                return true;
            if (playerOverdo > (FullGroup / 2))
                return false;
            else
            {
                int PCneedToRoll = 0;
                foreach (var playerPoint in playerChildren)
                {
                    if (GetComputerNeedToRollSecond(playerPoint, computerPoints))
                        PCneedToRoll++;
                }
                if (PCneedToRoll >= (FullGroup / 2))
                    return true;
                else
                    return false;
            }
        }

        public static List<int> GetAllChildren(int points)
        {
            List<int> children = new List<int>();

            for (int i = 0; i < distributionSeries.Count; i++)
                for (int j = 0; j < distributionSeries[i].Item2; j++)
                    children.Add(points + distributionSeries[i].Item1);

            return children;
        }

    }
}
