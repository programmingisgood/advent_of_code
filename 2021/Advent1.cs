
namespace AoC2021
{
    class Advent1
    {
        public void Run()
        {
            Console.WriteLine("Running Advent 1, Part 1");

            List<int> inputs = new List<int>();
            foreach (string line in System.IO.File.ReadLines(@"data\1.txt"))
            {
                inputs.Add(Int32.Parse(line));
            }

            int numIncreases = 0;
            int prevDepth = 0;
            bool firstMeasurement = true;
            foreach (int currDepth in inputs)
            {
                if (!firstMeasurement && prevDepth < currDepth)
                {
                    numIncreases++;
                }
                prevDepth = currDepth;
                firstMeasurement = false;
            }

            Console.WriteLine("Number of increases: " + numIncreases);

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 1, Part 2");

            numIncreases = 0;
            for (int i = 0; i < inputs.Count; i++)
            {
                if (i + 3 >= inputs.Count)
                {
                    break;
                }

                int currSlidingWindow = inputs[i] + inputs[i + 1] + inputs[i + 2];
                int nextSlidingWindow = inputs[i + 1] + inputs[i + 2] + inputs[i + 3];
                if (nextSlidingWindow > currSlidingWindow)
                {
                    numIncreases++;
                }
            }

            Console.WriteLine("Number of increases (sliding windows): " + numIncreases);
        }
    }
}
