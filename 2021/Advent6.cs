using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2021
{
    class Advent6
    {
        public void Run()
        {
            Console.WriteLine("Running Advent 6, Part 1");

            // This is a mapping of days remaining and number of fish.
            Dictionary<long, long> fish = new Dictionary<long, long>();
            Parse(fish);

            Console.WriteLine("Number of fish after 80 days: " + Propagate(80, fish));

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 5, Part 2");

            fish.Clear();
            Parse(fish);
            Console.WriteLine("Number of fish after 256 days: " + Propagate(256, fish));
        }

        private long Propagate(int numDays, Dictionary<long, long> fish)
        {
            for (long d = 0; d < numDays; d++)
            {
                long numFishToAdd = 0;
                for (long day = 0; day < 9; day++)
                {
                    long numFishOnDay = fish[day];
                    fish[day] = 0;
                    // Check if it is time to spawn.
                    if (day == 0)
                    {
                        numFishToAdd += numFishOnDay;
                    }
                    else
                    {
                        fish[day - 1] += numFishOnDay;
                    }
                }

                // Move the fish that are spawning new fish back to day 6.
                fish[6] += numFishToAdd;
                // Add new fish to day 8.
                fish[8] += numFishToAdd;
            }

            long totalFish = 0;
            foreach (KeyValuePair<long, long> fishGroups in fish)
            {
                totalFish += fishGroups.Value;
            }
            return totalFish;
        }
        
        private void Parse(Dictionary<long, long> fish)
        {
            for (long i = 0; i < 9; i++)
            {
                fish[i] = 0;
            }

            string[] fishes = System.IO.File.ReadAllText(@"data\6.txt").Split(',');
            foreach (string fishStr in fishes)
            {
                long daysRemaining = Int64.Parse(fishStr);
                fish[daysRemaining]++;
            }
        }
    }
}
