using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent7
    {
        public void Run()
        {
            Console.WriteLine("Running Advent 7, Part 1");

            // This is a mapping of days remaining and number of fish.
            List<int> positions = new List<int>();
            Parse(positions, out int smallestDist, out int largestDist);

            int prevBestFuelCost = Int32.MaxValue;
            int checkDist = smallestDist + ((largestDist - smallestDist) / 2);
            int bestFuelCost = FindBestFuelCost(positions, prevBestFuelCost, checkDist, -1, smallestDist, largestDist);

            Console.WriteLine("Best fuel cost: " + bestFuelCost);

            bestFuelCost = Int32.MaxValue;
            for (int d = smallestDist; d <= largestDist; d++)
            {
                int fuelCost = CalcFuelCost(positions, d);
                if (fuelCost < bestFuelCost)
                {
                    bestFuelCost = fuelCost;
                }
            }

            Console.WriteLine("Best fuel cost (brute force): " + bestFuelCost);

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 7, Part 2");
        }

        private int CalcFuelCost(List<int> positions, int atDist)
        {
            int fuelCost = 0;
            for (int i = 0; i < positions.Count; i++)
            {
                fuelCost += Math.Abs(atDist - positions[i]);
            }

            return fuelCost;
        }

        private int FindBestFuelCost(List<int> positions, int prevBestFuelCost, int checkDist, int prevCheckDist, int smallestDist, int largestDist)
        {
            int fuelCostLeft = CalcFuelCost(positions, checkDist);
            int fuelCostRight = CalcFuelCost(positions, checkDist + 1);
            if (fuelCostLeft < prevBestFuelCost && fuelCostLeft < fuelCostRight)
            {
                if (prevCheckDist == -1)
                {
                    prevCheckDist = smallestDist;
                }
                int newCheckDist = checkDist - (Math.Abs(checkDist - prevCheckDist) / 2);
                return FindBestFuelCost(positions, fuelCostLeft, newCheckDist, checkDist, smallestDist, largestDist);
            }
            else if (fuelCostRight < prevBestFuelCost)
            {
                if (prevCheckDist == -1)
                {
                    prevCheckDist = largestDist;
                }
                int newCheckDist = checkDist + (Math.Abs(prevCheckDist - checkDist) / 2);
                return FindBestFuelCost(positions, fuelCostRight, newCheckDist, checkDist, smallestDist, largestDist);
            }
            else
            {
                // We found it.
                return prevBestFuelCost;
            }
        }

        private void Parse(List<int> positions, out int smallestDist, out int largestDist)
        {
            smallestDist = Int32.MaxValue;
            largestDist = 0;
            string[] posEntries = System.IO.File.ReadAllText(@"data\7.txt").Split(',');
            foreach (string posEntry in posEntries)
            {
                int pos = Int32.Parse(posEntry);
                positions.Add(pos);

                if (pos > largestDist)
                {
                    largestDist = pos;
                }

                if (pos < smallestDist)
                {
                    smallestDist = pos;
                }
            }
        }
    }
}
