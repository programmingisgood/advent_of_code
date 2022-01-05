using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent9
    {
        private class HeightMap
        {
            public int Width { get; private set; } = 0;
            public int Depth { get; private set; } = 0;

            private int[,] heights = new int[0, 0];

            public HeightMap(int setWidth, int setDepth)
            {
                Width = setWidth;
                Depth = setDepth;

                heights = new int[Width, Depth];
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
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Depth; y++)
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
                if (x + 1 < Width)
                {
                    neighborHeights[1] = heights[x + 1, y];
                }

                // Top
                if (y - 1 >= 0)
                {
                    neighborHeights[2] = heights[x, y - 1];
                }
                // Bottom
                if (y + 1 < Depth)
                {
                    neighborHeights[3] = heights[x, y + 1];
                }

                Array.Sort(neighborHeights);
                return neighborHeights[0];
            }
        }

        private class Basin
        {
            private HashSet<int> indicesInBasin = new HashSet<int>();

            public bool Contains(int )
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

            List<Basin> basins = new List<Basin>();
            FindBasins(heightMap, basins);

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

        private void FindBasins(HeightMap heightMap, List<Basin> basins)
        {
            // Go through each index in the height map and check if it is
            // a part of a basin.
            for (int x = 0; x < heightMap.Width; x++)
            {
                for (int y = 0; y < heightMap.Depth; y++)
                {
                    AddToBasins(heightMap, x, y, basins);
                }
            }
        }

        private void AddToBasins(HeightMap heightMap, int x, int y, List<Basin> basins)
        {
            if (heightMap.GetHeight(x, y) < 9)
            {
                // Check if this position is already in a basin.
                foreach (Basin basin in basins)
                {
                    if (basin.Contains(x, y))
                    {
                        return;
                    }
                }

                // This is a new basin.
                Basin newBasin = new Basin();
                newBasin.Add(x, y);
                basins.Add(newBasin);
            }
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
