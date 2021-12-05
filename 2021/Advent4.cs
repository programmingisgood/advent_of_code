using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent4
    {
        private class Board
        {
            private int width = 0;
            private int height = 0;

            private int[,] values = new int[0, 0];
            private bool[,] marks = new bool[0, 0];
            private int numValues = 0;

            public Board(int setWidth, int setHeight)
            {
                width = setWidth;
                height = setHeight;

                values = new int[width, height];
                marks = new bool[width, height];
                numValues = 0;
            }

            public void AddValue(int value)
            {
                values[numValues % width, numValues / height] = value;
                marks[numValues % width, numValues / height] = false;
                numValues++;
            }

            public bool GetHasAllValues()
            {
                return numValues == width * height;
            }

            public bool MarkValue(int value)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (values[x, y] == value)
                        {
                            marks[x, y] = true;
                            if (IsComplete())
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            public bool IsComplete()
            {
                // First check columns.
                for (int x = 0; x < width; x++)
                {
                    int numMarks = 0;
                    for (int y = 0; y < height; y++)
                    {
                        if (marks[x, y])
                        {
                            numMarks++;
                        }
                    }
                    if (numMarks == height)
                    {
                        return true;
                    }
                }

                // Next check rows.
                for (int y = 0; y < height; y++)
                {
                    int numMarks = 0;
                    for (int x = 0; x < width; x++)
                    {
                        if (marks[x, y])
                        {
                            numMarks++;
                        }
                    }
                    if (numMarks == width)
                    {
                        return true;
                    }
                }

                return false;
            }

            public int Score(int finalDraw)
            {
                // Start by finding the sum of all unmarked numbers on that board.
                int sumUnmarked = 0;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (!marks[x, y])
                        {
                            sumUnmarked += values[x, y];
                        }
                    }
                }

                // Multiply that sum by the number that was just called when the board won.
                return sumUnmarked * finalDraw;
            }
        }

        private int BoardWidth = 5;
        private int BoardHeight = 5;

        public void Run()
        {
            Console.WriteLine("Running Advent 4, Part 1");

            List<int> draws = new List<int>();
            List<Board> boards = new List<Board>();
            Parse(draws, boards);

            int winningScore = 0;

            // Draw numbers and mark boards.
            foreach (int draw in draws)
            {
                foreach (Board board in boards)
                {
                    // Returns true if this board is complete.
                    if (board.MarkValue(draw))
                    {
                        winningScore = board.Score(draw);
                        break;
                    }
                }

                if (winningScore > 0)
                {
                    break;
                }
            }

            Console.WriteLine("Winning first board score: " + winningScore);

            /////////////////////////////////////////////////////////////////

            winningScore = 0;

            // Draw numbers and mark boards.
            foreach (int draw in draws)
            {
                foreach (Board board in boards)
                {
                    if (board.IsComplete())
                    {
                        continue;
                    }

                    // Returns true if this board is complete.
                    if (board.MarkValue(draw))
                    {
                        winningScore = board.Score(draw);
                    }
                }
            }

            Console.WriteLine("Winning last board score: " + winningScore);

            Console.WriteLine("Running Avent 4, Part 2");
        }

        private void Parse(List<int> draws, List<Board> boards)
        {
            Board currBoard = new Board(BoardWidth, BoardHeight);
            foreach (string line in System.IO.File.ReadLines(@"data\4.txt"))
            {
                // The first line are the number draws.
                if (draws.Count == 0)
                {
                    string[] values = line.Split(',');
                    foreach (string value in values)
                    {
                        draws.Add(Int32.Parse(value));
                    }
                    continue;
                }

                if (line.Length > 0)
                {
                    string[] values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in values)
                    {
                        currBoard.AddValue(Int32.Parse(value));
                    }
                }

                if (currBoard.GetHasAllValues())
                {
                    boards.Add(currBoard);
                    currBoard = new Board(BoardWidth, BoardHeight);
                }
            }
        }
    }
}
