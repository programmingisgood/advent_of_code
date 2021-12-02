
namespace AoC2021
{
    class Advent2
    {
        private struct Instruction
        {
            public enum Dir
            {
                Forward,
                Up,
                Down
            }

            public Dir dir;
            public int dist;

            public Instruction(string setDir, int setDist)
            {
                dir = Dir.Forward;
                if (setDir[0] == 'u')
                {
                    dir = Dir.Up;
                }
                else if (setDir[0] == 'd')
                {
                    dir = Dir.Down;
                }
                
                dist = setDist;
            }
        }

        public void Run()
        {
            Console.WriteLine("Running Advent 2, Part 1");

            List<Instruction> instructions = new List<Instruction>();
            foreach (string line in System.IO.File.ReadLines(@"data\2.txt"))
            {
                string[] parts = line.Split(' ');
                string dir = parts[0];
                int dist = Int32.Parse(parts[1]);
                instructions.Add(new Instruction(dir, dist));
            }

            int x = 0;
            int y = 0;
            foreach (Instruction instruction in instructions)
            {
                if (instruction.dir == Instruction.Dir.Forward)
                {
                    x += instruction.dist;
                }
                else if (instruction.dir == Instruction.Dir.Up)
                {
                    y -= instruction.dist;
                }
                else if (instruction.dir == Instruction.Dir.Down)
                {
                    y += instruction.dist;
                }
            }

            Console.WriteLine("X: " + x + " times y: " + y + " = " + (x * y));

            /////////////////////////////////////////////////////////////////

            Console.WriteLine("Running Avent 2, Part 2");

            x = 0;
            y = 0;
            int aim = 0;
            foreach (Instruction instruction in instructions)
            {
                if (instruction.dir == Instruction.Dir.Forward)
                {
                    x += instruction.dist;
                    y += aim * instruction.dist;
                }
                else if (instruction.dir == Instruction.Dir.Up)
                {
                    aim -= instruction.dist;
                }
                else if (instruction.dir == Instruction.Dir.Down)
                {
                    aim += instruction.dist;
                }
            }

            Console.WriteLine("X: " + x + " times y: " + y + " = " + (x * y));
        }
    }
}
