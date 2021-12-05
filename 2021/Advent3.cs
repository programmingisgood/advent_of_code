using System;
using System.Collections.Generic;

namespace AoC2021
{
    class Advent3
    {
        private class BitCounter
        {
            private int numOnes = 0;
            private int numZeros = 0;

            public void SetBit(bool on)
            {
                if (on)
                {
                    numOnes++;
                }
                else
                {
                    numZeros++;
                }
            }

            public int GetMostCommonMask(int bitPosition)
            {
                if (numOnes >= numZeros)
                {
                    return 1 << bitPosition;
                }
                return 0;
            }

            public int GetLeastCommonMask(int bitPosition)
            {
                if (numOnes < numZeros)
                {
                    return 1 << bitPosition;
                }
                return 0;
            }
        }

        public void Run()
        {
            Console.WriteLine("Running Advent 3, Part 1");

            List<int> report = new List<int>();
            BitCounter[] bitCounters = new BitCounter[12];
            for (int i = 0; i < bitCounters.Length; i++)
            {
                bitCounters[i] = new BitCounter();
            }

            foreach (string line in System.IO.File.ReadLines(@"data\3.txt"))
            {
                int entry = 0;
                int index = 0;
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    char c = line[i];
                    int bitIndex = 11 - i;
                    bool bitIsOn = c == '1';
                    entry += bitIsOn ? 1 << bitIndex : 0;
                    bitCounters[bitIndex].SetBit(bitIsOn);
                    index++;
                }
                report.Add(entry);
            }

            int gammaRate = 0;
            int epsilonRate = 0;
            for (int i = 0; i < bitCounters.Length; i++)
            {
                gammaRate += bitCounters[i].GetMostCommonMask(i);
                epsilonRate += bitCounters[i].GetLeastCommonMask(i);
            }

            Console.WriteLine("Gamma Rate: " + gammaRate + " Epsilon Rate: " + epsilonRate + " Power: " + gammaRate * epsilonRate);

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 3, Part 2");

            List<int> reportCopyOxy = new List<int>(report);
            List<int> reportCopyCO2 = new List<int>(report);
            for (int b = 11; b >= 0; b--)
            {
                if (reportCopyOxy.Count > 1)
                {
                    BitCounter bitCounter = CountBits(reportCopyOxy, b);
                    for (int r = 0; r < reportCopyOxy.Count; r++)
                    {
                        int entry = reportCopyOxy[r];
                        int mask = 1 << b;
                        int mostCommon = bitCounter.GetMostCommonMask(b);
                        int maskedEntry = entry & mask;
                        if (mostCommon != maskedEntry)
                        {
                            reportCopyOxy.RemoveAt(r);
                            r--;
                        }

                        if (reportCopyOxy.Count == 1)
                        {
                            break;
                        }
                    }
                }

                if (reportCopyCO2.Count > 1)
                {
                    BitCounter bitCounter = CountBits(reportCopyCO2, b);
                    for (int r = 0; r < reportCopyCO2.Count; r++)
                    {
                        int entry = reportCopyCO2[r];
                        int mask = 1 << b;
                        int leastCommon = bitCounter.GetLeastCommonMask(b);
                        int maskedEntry = entry & mask;
                        if (leastCommon != maskedEntry)
                        {
                            reportCopyCO2.RemoveAt(r);
                            r--;
                        }

                        if (reportCopyCO2.Count == 1)
                        {
                            break;
                        }
                    }
                }
            }

            if (reportCopyOxy.Count != 1 || reportCopyCO2.Count != 1)
            {
                Console.WriteLine("Lists are greater than 1");
            }

            int oxygenGeneratorRating = reportCopyOxy[0];
            int co2ScrubberRating = reportCopyCO2[0];

            int lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;

            Console.WriteLine("Oxygen Generator Rating: " + oxygenGeneratorRating + " CO2 Scrubber Rating: " + co2ScrubberRating + " Life Support Rating: " + lifeSupportRating);
        }

        private BitCounter CountBits(List<int> report, int bitPosition)
        {
            BitCounter bitCounter = new BitCounter();
            foreach (int entry in report)
            {
                int mask = 1 << bitPosition;
                bitCounter.SetBit((entry & mask) != 0);
            }
            return bitCounter;
        }
    }
}
