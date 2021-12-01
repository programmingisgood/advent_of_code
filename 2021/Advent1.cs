
namespace AoC2021
{
    class Advent1
    {
        public void Run()
        {
            Console.WriteLine("Running Advent 1");

            int numIncreases = 0;
            int prevDepth = 0;
            bool firstMeasurement = true;
            foreach (string line in System.IO.File.ReadLines(@"data\1.txt"))
            {
                int currDepth = Int32.Parse(line);
                if (!firstMeasurement && prevDepth < currDepth)
                {
                    numIncreases++;
                }
                prevDepth = currDepth;
                firstMeasurement = false;
            }

            Console.WriteLine("Number of increases: " + numIncreases);
        }
    }
}
