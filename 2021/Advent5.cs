using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2021
{
    class Advent5
    {
        private struct Coord// : System.IEquatable<Coord>
        {
            public int x;
            public int y;

            public Coord(int setX, int setY)
            {
                x = setX;
                y = setY;
            }

            /*public bool Equals(Coord other)
            {
                return x.Equals(other.x) && y.Equals(other.y);
            }

            public override bool Equals(Object other)
            {
                if (other is Coord)
                {
                    // Convert object to Vector3
                    Coord otherVector = (Coord) other;

                    // Check for equality
                    return otherVector.Equals(this);
                }
                else
                {
                    return false;
                }
            }

            public static bool operator ==(Coord coord1, Coord coord2)
            {
                if ((object)coord1 == null || ((object)coord2) == null)
                {
                    return System.Object.Equals(coord1, coord2);
                }

                return coord1.Equals(coord2);
            }

            public static bool operator !=(Coord coord1, Coord coord2)
            {
                return !(coord1 == coord2);
            }

            public override int GetHashCode()
            {
                var hashCode = this.x.GetHashCode();
                hashCode = (hashCode * 397) ^ this.x.GetHashCode();
                hashCode = (hashCode * 397) ^ this.y.GetHashCode();
                return hashCode;
            }*/
        }

        private class Line
        {
            public Coord Start { get; }
            public Coord End { get; }

            public Line(int[] values)
            {
                Start = new Coord(values[0], values[1]);
                End = new Coord(values[2], values[3]);
            }
        }

        public void Run()
        {
            Console.WriteLine("Running Advent 5, Part 1");

            List<Line> lines = new List<Line>();
            Parse(lines);

            List<Line> linesPart1 = new List<Line>();
            foreach (Line line in lines)
            {
                // Only add horizontal and vertical lines.
                if (line.Start.x == line.End.x || line.Start.y == line.End.y)
                {
                    linesPart1.Add(line);
                }
            }

            Dictionary<Coord, int> board = GenerateBoard(linesPart1);

            int numOverlaps = FindNumOverlaps(board);
            Console.WriteLine("Number Overlaps: " + numOverlaps);

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 5, Part 2");

            List<Line> linesPart2 = new List<Line>();
            foreach (Line line in lines)
            {
                int distX = Math.Abs(line.Start.x - line.End.x);
                int distY = Math.Abs(line.Start.y - line.End.y);
                bool isDiagonal = distX == distY;
                // Only add horizontal, vertical, and 45 degree lines.
                if (line.Start.x == line.End.x || line.Start.y == line.End.y || isDiagonal)
                {
                    linesPart2.Add(line);
                }
            }

            board = GenerateBoard(linesPart2);

            numOverlaps = FindNumOverlaps(board);
            Console.WriteLine("Number Overlaps: " + numOverlaps);
        }

        private int FindNumOverlaps(Dictionary<Coord, int> board)
        {
            int numOverlaps = 0;
            foreach (KeyValuePair<Coord, int> pair in board)
            {
                if (pair.Value > 1)
                {
                    numOverlaps++;
                }
            }

            return numOverlaps;
        }

        private Dictionary<Coord, int> GenerateBoard(List<Line> lines)
        {
            Dictionary<Coord, int> board = new Dictionary<Coord, int>();

            foreach (Line line in lines)
            {
                int x = line.Start.x;
                int distX = Math.Abs(line.Start.x - line.End.x);
                int y = line.Start.y;
                int distY = Math.Abs(line.Start.y - line.End.y);
                while (distX >= 0 || distY >= 0)
                {
                    Coord coord = new Coord(x, y);
                    if (board.TryGetValue(coord, out int count))
                    {
                        count++;
                        board[coord] = count;
                    }
                    else
                    {
                        board[coord] = 1;
                    }

                    if (line.Start.y < line.End.y)
                    {
                        y++;
                    }
                    else if (line.Start.y > line.End.y)
                    {
                        y--;
                    }

                    distY--;

                    if (line.Start.x < line.End.x)
                    {
                        x++;
                    }
                    else if (line.Start.x > line.End.x)
                    {
                        x--;
                    }
                    distX--;
                }
            }

            return board;
        }

        private void Parse(List<Line> lines)
        {
            foreach (string line in System.IO.File.ReadLines(@"data\5.txt"))
            {
                Regex ItemRegex = new Regex(@"(\d+)", RegexOptions.Compiled);
                int[] values = new int[4];
                int c = 0;
                foreach (Match itemMatch in ItemRegex.Matches(line))
                {
                    values[c] = Int32.Parse(itemMatch.Value);
                    c++;
                }
                lines.Add(new Line(values));
            }
        }
    }
}
