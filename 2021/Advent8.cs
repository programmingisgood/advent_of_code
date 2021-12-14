using System;
using System.Collections.Generic;

// NOTE: I am not happy with my solution to part 2. I went down a bit of a rabbit hole of
// "I'll just brute force a quick solution and then refactor" but the brute force method
// I used took way too long.
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

            // Find the segments matching digit 1.
            public string FindDigitSegments(int digit)
            {
                foreach (string signalPattern in SignalPatterns)
                {
                    if (digit == 1 && signalPattern.Length == 2)
                    {
                        return signalPattern;
                    }
                    else if (digit == 4 && signalPattern.Length == 4)
                    {
                        return signalPattern;
                    }
                    else if (digit == 7 && signalPattern.Length == 3)
                    {
                        return signalPattern;
                    }
                    else if (digit == 8 && signalPattern.Length == 7)
                    {
                        return signalPattern;
                    }
                }

                return "";
            }

            private string SubtractCodes(string removeCodes, string fromString)
            {
                foreach (char c in removeCodes)
                {
                    fromString = fromString.Replace(c.ToString(), "");
                }

                return fromString;
            }

            private bool ContainsCodes(string segment, string codes)
            {
                foreach (char code in codes)
                {
                    if (!segment.Contains(code))
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool GetOutputMatchesSignalPattern(string outputValue, string signalPattern)
            {
                if (outputValue.Length != signalPattern.Length)
                {
                    return false;
                }

                foreach (char outputCode in outputValue)
                {
                    if (!signalPattern.Contains(outputCode))
                    {
                        return false;
                    }
                }

                return true;
            }

            public void FindTwoThreeFivesSegments(string[] twoThreeFivesSegments)
            {
                int i = 0;
                foreach (string signalPattern in SignalPatterns)
                {
                    if (signalPattern.Length == 5)
                    {
                        twoThreeFivesSegments[i] = signalPattern;
                        i++;
                        if (i == 3)
                        {
                            break;
                        }
                    }
                }
            }

            public string FindThreeSegments(string[] twoThreeFivesSegments, string digit1Segments)
            {
                // Find the segments inside twoThreeFivesSegments that has all segment codes from digit1Segments.
                // These are the segments for digit 3.
                foreach (string digitSegment in twoThreeFivesSegments)
                {
                    if (ContainsCodes(digitSegment, digit1Segments))
                    {
                        return digitSegment;
                    }
                }

                return "";
            }

            public string FindSixSegments(string topRightSegmentCode)
            {
                foreach (string signalPattern in SignalPatterns)
                {
                    if (signalPattern.Length == 6 && !ContainsCodes(signalPattern, topRightSegmentCode))
                    {
                        return signalPattern;
                    }
                }

                return "";
            }

            public string FindNineSegments(string bottomLeftSegmentCode)
            {
                foreach (string signalPattern in SignalPatterns)
                {
                    if (signalPattern.Length == 6 && !ContainsCodes(signalPattern, bottomLeftSegmentCode))
                    {
                        return signalPattern;
                    }
                }

                return "";
            }

            public string FindZeroSegments(string middleSegmentCode)
            {
                foreach (string signalPattern in SignalPatterns)
                {
                    if (signalPattern.Length == 6 && !ContainsCodes(signalPattern, middleSegmentCode))
                    {
                        return signalPattern;
                    }
                }

                return "";
            }

            public void FindTwoFiveSegments(string[] twoThreeFivesSegments, string digit3Segments, string topLeftSegmentCode, out string digit2Segments, out string digit5Segments)
            {
                digit2Segments = "";
                digit5Segments = "";
                foreach (string digitSegments in twoThreeFivesSegments)
                {
                    if (ContainsCodes(digitSegments, digit3Segments))
                    {
                        continue;
                    }
                    // If removing the top left code does nothing, it is 2.
                    else if (SubtractCodes(topLeftSegmentCode, digitSegments).Length == digitSegments.Length)
                    {
                        digit2Segments = digitSegments;
                    }
                    else
                    {
                        digit5Segments = digitSegments;
                    }
                }
            }

            public string FindTopSegmentCode(string digit1Segments, string digit7Segments)
            {
                return SubtractCodes(digit1Segments, digit7Segments);
            }

            public void FindMiddleAndTopLeftSegmentCodes(string digit1Segments, string digit3Segments, string digit4Segments, out string topLeftSegmentCode, out string middleSegmentCode)
            {
                string fourMinusOne = SubtractCodes(digit1Segments, digit4Segments);
                topLeftSegmentCode = SubtractCodes(digit3Segments, digit4Segments);
                middleSegmentCode = SubtractCodes(topLeftSegmentCode, fourMinusOne);
            }

            public string FindBottomSegmentCodes(string digit3Segments, string digit7Segments, string middleSegmentCode)
            {
                string remainingSegments = SubtractCodes(digit7Segments, digit3Segments);
                return SubtractCodes(middleSegmentCode, remainingSegments);
            }

            public string FindBottomRightSegmentCode(string digit2Segments, string digit5Segments, string topLeftSegmentCode)
            {
                string remainingSegments = SubtractCodes(digit2Segments, digit5Segments);
                return SubtractCodes(topLeftSegmentCode, remainingSegments);
            }

            public string FindBottomLeftSegmentCode(string digit2Segments, string digit3Segments)
            {
                return SubtractCodes(digit3Segments, digit2Segments);
            }

            public string FindTopRightSegmentCode(string digit2Segments, string digit5Segments, string bottomLeftSegmentCode)
            {
                string remainingSegments = SubtractCodes(digit5Segments, digit2Segments);
                return SubtractCodes(bottomLeftSegmentCode, remainingSegments);
            }

            public int SumOutputValues(string digit0Segments, string digit1Segments, string digit2Segments, string digit3Segments,
                                       string digit4Segments, string digit5Segments, string digit6Segments,
                                       string digit7Segments, string digit8Segments, string digit9Segments)
            {
                int finalOutputValue = 0;
                int orderOfMagnitude = 1000;
                foreach (string outputValue in OutputValues)
                {
                    if (GetOutputMatchesSignalPattern(outputValue, digit1Segments))
                    {
                        finalOutputValue += 1 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit2Segments))
                    {
                        finalOutputValue += 2 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit3Segments))
                    {
                        finalOutputValue += 3 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit4Segments))
                    {
                        finalOutputValue += 4 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit5Segments))
                    {
                        finalOutputValue += 5 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit6Segments))
                    {
                        finalOutputValue += 6 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit7Segments))
                    {
                        finalOutputValue += 7 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit8Segments))
                    {
                        finalOutputValue += 8 * orderOfMagnitude;
                    }
                    if (GetOutputMatchesSignalPattern(outputValue, digit9Segments))
                    {
                        finalOutputValue += 9 * orderOfMagnitude;
                    }

                    orderOfMagnitude /= 10;
                }

                return finalOutputValue;
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
            int outputValueSum = 0;
            string[] twoThreeFivesSegments = new string[3];
            foreach (Entry entry in entries)
            {
                string topSegmentCode;
                string topLeftSegmentCode;
                string topRightSegmentCode;
                string middleSegmentCode;
                string bottomSegmentCode;
                string bottomLeftSegmentCode;
                string bottomRightSegmentCode;

                string digit0Segments;
                string digit1Segments;
                string digit2Segments;
                string digit3Segments;
                string digit4Segments;
                string digit5Segments;
                string digit6Segments;
                string digit7Segments;
                string digit8Segments;
                string digit9Segments;

                // It is easy to determine the segments making up digit 1.
                digit1Segments = entry.FindDigitSegments(1);
                digit4Segments = entry.FindDigitSegments(4);
                digit7Segments = entry.FindDigitSegments(7);
                digit8Segments = entry.FindDigitSegments(8);
                topSegmentCode = entry.FindTopSegmentCode(digit1Segments, digit7Segments);
                entry.FindTwoThreeFivesSegments(twoThreeFivesSegments);
                digit3Segments = entry.FindThreeSegments(twoThreeFivesSegments, digit1Segments);
                entry.FindMiddleAndTopLeftSegmentCodes(digit1Segments, digit3Segments, digit4Segments, out topLeftSegmentCode, out middleSegmentCode);
                bottomSegmentCode = entry.FindBottomSegmentCodes(digit3Segments, digit7Segments, middleSegmentCode);
                entry.FindTwoFiveSegments(twoThreeFivesSegments, digit3Segments, topLeftSegmentCode, out digit2Segments, out digit5Segments);
                bottomRightSegmentCode = entry.FindBottomRightSegmentCode(digit2Segments, digit5Segments, topLeftSegmentCode);
                bottomLeftSegmentCode = entry.FindBottomLeftSegmentCode(digit2Segments, digit3Segments);
                topRightSegmentCode = entry.FindTopRightSegmentCode(digit2Segments, digit5Segments, bottomLeftSegmentCode);
                digit6Segments = entry.FindSixSegments(topRightSegmentCode);
                digit9Segments = entry.FindNineSegments(bottomLeftSegmentCode);
                digit0Segments = entry.FindZeroSegments(middleSegmentCode);

                outputValueSum += entry.SumOutputValues(digit0Segments, digit1Segments, digit2Segments, digit3Segments, digit4Segments,
                                                        digit5Segments, digit6Segments, digit7Segments, digit8Segments, digit9Segments);
            }

            Console.WriteLine("Output values sum: " + outputValueSum);
        }

        private int CountDigits(List<Entry> entries, int digit)
        {
            int count = 0;
            foreach (Entry entry in entries)
            {
                foreach (string digitInEntry in entry.OutputValues)
                {
                    int numSegments = digitInEntry.Length;
                    if ((digit == 1 && numSegments == 2) ||
                        (digit == 4 && numSegments == 4) ||
                        (digit == 7 && numSegments == 3) ||
                        (digit == 8 && numSegments == 7))
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
