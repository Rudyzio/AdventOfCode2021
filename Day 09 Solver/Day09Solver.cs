using System.Collections.Generic;
using System.Linq;

namespace Day_09_Solver
{
    public static class Day09Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var heightMap = ReadInput(lines);
            var toReturn = 0;

            for (var i = 0; i < heightMap.Length; i++)
            {
                for (var j = 0; j < heightMap[i].Length; j++)
                {
                    var adjacent = heightMap.GetAdjacent(i, j);
                    if (adjacent.All(x => x > heightMap[i][j]))
                    {
                        toReturn += heightMap[i][j] + 1;
                    }
                }
            }

            return toReturn;
        }

        public static int Part2Solution(string[] lines)
        {
            var heightMap = ReadInput(lines);
            var basins = new List<int>();

            for (var i = 0; i < heightMap.Length; i++)
            {
                for (var j = 0; j < heightMap[i].Length; j++)
                {
                    var adjacent = heightMap.GetAdjacentWithPosition(i, j);
                    if (adjacent.All(x => x.Value > heightMap[i][j]))
                    {
                        var basinSize = CalculateBasinSize(heightMap, i, j);
                        // System.Console.WriteLine("Basin size: " + basinSize);
                        basins.Add(basinSize);
                    }
                }
            }

            var toReturn = 1;
            foreach (var basin in basins.OrderByDescending(x => x).Take(3))
            {
                // System.Console.WriteLine(basin);
                toReturn *= basin;
            }

            return toReturn;
        }

        private static int CalculateBasinSize(int[][] heightMap, int x, int y)
        {
            var queue = new Queue<(KeyValuePair<int, int> Position, int Value)>();
            queue.Enqueue((new KeyValuePair<int, int>(x, y), heightMap[x][y]));
            var basin = new List<KeyValuePair<int, int>> { new KeyValuePair<int, int>(x, y) };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var adjacent = heightMap.GetAdjacentWithPosition(current.Position.Key, current.Position.Value);

                // System.Console.WriteLine($"Current value: {current.Value}");
                foreach (var adj in adjacent)
                {
                    if (basin.Any(x => x.Key == adj.Position.Key && x.Value == adj.Position.Value))
                    {
                        continue;
                    }

                    if (adj.Value < 9 && current.Value < adj.Value)
                    {
                        // System.Console.WriteLine($"Adjacent: {adj.Value}");
                        queue.Enqueue(adj);
                        basin.Add(adj.Position);
                    }
                }
            }

            return basin.Count();
        }

        private static int[][] ReadInput(string[] lines)
        {
            var heightMap = new int[lines.Length][];

            for (var i = 0; i < lines.Length; i++)
            {
                heightMap[i] = new int[lines[i].Length];
                for (var j = 0; j < lines[i].Length; j++)
                {
                    heightMap[i][j] = int.Parse(lines[i][j].ToString());
                }
            }

            return heightMap;
        }

        private static List<int> GetAdjacent(this int[][] heightMap, int x, int y)
        {
            List<int> adjacentList = new();

            if (x > 0)
            {
                // Above
                adjacentList.Add(heightMap[x - 1][y]);
            }

            if (y > 0)
            {
                // Left
                adjacentList.Add(heightMap[x][y - 1]);
            }

            if (y + 1 < heightMap[0].Length)
            {
                // Left
                adjacentList.Add(heightMap[x][y + 1]);
            }

            if (x + 1 < heightMap.Length)
            {
                // Left
                adjacentList.Add(heightMap[x + 1][y]);
            }

            return adjacentList;
        }

        private static List<(KeyValuePair<int, int> Position, int Value)> GetAdjacentWithPosition(this int[][] heightMap, int x, int y)
        {
            List<(KeyValuePair<int, int>, int)> adjacentList = new();

            if (x > 0)
            {
                // Above
                adjacentList.Add((new KeyValuePair<int, int>(x - 1, y), heightMap[x - 1][y]));
            }

            if (y > 0)
            {
                // Left
                adjacentList.Add((new KeyValuePair<int, int>(x, y - 1), heightMap[x][y - 1]));
            }

            if (y + 1 < heightMap[0].Length)
            {
                // Left
                adjacentList.Add((new KeyValuePair<int, int>(x, y + 1), heightMap[x][y + 1]));
            }

            if (x + 1 < heightMap.Length)
            {
                // Left
                adjacentList.Add((new KeyValuePair<int, int>(x + 1, y), heightMap[x + 1][y]));
            }

            return adjacentList;
        }
    }
}
