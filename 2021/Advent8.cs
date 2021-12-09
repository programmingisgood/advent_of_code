using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent8
    {
        private class Entry
        {
            public List<string> SignalPatterns { get; }
            public List<string> OutputValues { get; }

            public Entry()
            {
                SignalPatterns = new List<string>();
                OutputValues = new List<string>();
            }

            public void AddSignalPattern(string signalPattern)
            {
                SignalPatterns.Add(signalPattern);
            }

            public void AddOutputValue(string outputValue)
            {
                OutputValues.Add(outputValue);
            }
        }

        public void Run()
        {
            Console.WriteLine("Running Advent 8, Part 1");

            List<Entry> entries = new List<Entry>();
            Parse(entries);

            int count = CountDigits(entries, 1);
            count += CountDigits(entries, 4);
            count += CountDigits(entries, 7);
            count += CountDigits(entries, 8);

            Console.WriteLine("Total number of 1, 4, 7, and 8: " + count);

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 8, Part 2");
        }

        private int CountDigits(List<Entry> entries, int digit)
        {
            int count = 0;
            foreach (Entry entry in entries)
            {
                foreach (string digitInEntry in entry.OutputValues)
                {
                    int numSegments = digitInEntry.Length;
                    if (digit == 1 && numSegments == 2)
                    {
                        count++;
                    }
                    else if (digit == 4 && numSegments == 4)
                    {
                        count++;
                    }
                    else if (digit == 7 && numSegments == 3)
                    {
                        count++;
                    }
                    else if (digit == 8 && numSegments == 7)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void Parse(List<Entry> entries)
        {
            foreach (string line in System.IO.File.ReadLines(@"data\8.txt"))
            {
                Entry entry = new Entry();
                string[] digits = line.Split(' ');
                for (int i = 0; i < 10; i++)
                {
                    entry.AddSignalPattern(digits[i]);
                }

                for (int i = 11; i < 15; i++)
                {
                    entry.AddOutputValue(digits[i]);
                }

                entries.Add(entry);
            }
        }
    }
}
