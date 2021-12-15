using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent9
    {
        private class HeightMap
        {
            private int width = 0;
            private int height = 0;

            private int[,] heights = new int[0, 0];

            public HeightMap(int setWidth, int setHeight)
            {
                width = setWidth;
                height = setHeight;

                heights = new int[width, height];
            }

            public void SetHeight(int x, int y, int setHeight)
            {
                heights[x, y] = setHeight;
            }

            public int GetHeight(int x, int y)
            {
                return heights[x, y];
            }

            public void FindLowPoints(List<int> lowPoints)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int lowestNeighbor = FindLowestNeighbor(x, y);
                        if (heights[x, y] < lowestNeighbor)
                        {
                            lowPoints.Add(heights[x, y]);
                        }
                    }
                }
            }

            private int FindLowestNeighbor(int x, int y)
            {
                int[] neighborHeights = new int[4] { Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue };

                // Left
                if (x - 1 >= 0)
                {
                    neighborHeights[0] = heights[x - 1, y];
                }
                // Right
                if (x + 1 < width)
                {
                    neighborHeights[1] = heights[x + 1, y];
                }

                // Top
                if (y - 1 >= 0)
                {
                    neighborHeights[2] = heights[x, y - 1];
                }
                // Bottom
                if (y + 1 < height)
                {
                    neighborHeights[3] = heights[x, y + 1];
                }

                Array.Sort(neighborHeights);
                return neighborHeights[0];
            }
        }

        public void Run()
        {
            Console.WriteLine("Running Advent 9, Part 1");

            HeightMap heightMap = new HeightMap(100, 100);
            Parse(heightMap);

            List<int> lowPoints = new List<int>();
            heightMap.FindLowPoints(lowPoints);

            Console.WriteLine("Risk Level: " + CalcRiskLevel(lowPoints));

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 9, Part 2");

            //Console.WriteLine("Output values sum: " + outputValueSum);
        }

        private int CalcRiskLevel(List<int> lowPoints)
        {
            int riskLevelSum = 0;
            foreach (int lowPoint in lowPoints)
            {
                riskLevelSum += lowPoint + 1;
            }

            return riskLevelSum;
        }

        private void Parse(HeightMap heightMap)
        {
            int y = 0;
            foreach (string line in System.IO.File.ReadLines(@"data\9.txt"))
            {
                int x = 0;
                foreach (char heightChar in line)
                {
                    int height = Int32.Parse(heightChar.ToString());
                    heightMap.SetHeight(x, y, height);

                    x++;
                }

                y++;
            }
        }
    }
}
