namespace Day_02_Solver
{
    public static class Day02Solver
    {
        public static int Part1Solution(string[] lines)
        {
            int x = 0;
            int depth = 0;
            foreach (var line in lines)
            {
                var splitted = line.Split(" ");
                var command = splitted[0];
                var value = int.Parse(splitted[1]);

                switch (command)
                {
                    case "forward":
                        x += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                }
            }

            return x * depth;
        }

        public static int Part2Solution(string[] lines)
        {
            int x = 0;
            int depth = 0;
            int aim = 0;
            foreach (var line in lines)
            {
                // System.Console.WriteLine($"Command: {line}");
                var splitted = line.Split(" ");
                var command = splitted[0];
                var value = int.Parse(splitted[1]);

                switch (command)
                {
                    case "forward":
                        x += value;
                        depth += (aim * value);
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
                // System.Console.WriteLine($"X:{x} ; Depth:{depth} ; Aim:{aim}");
            }

            return x * depth;
        }
    }
}
