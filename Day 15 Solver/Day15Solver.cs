using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_15_Solver
{
    public static class Day15Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var graph = ParseInput(lines);
            var width = (int)Math.Sqrt(graph.Count);
            return Dijkstra(graph, graph[(width - 1, width - 1)]);
        }

        public static int Part2Solution(string[] lines)
        {
            var graph = ParseInput(lines);
            var width = (int)Math.Sqrt(graph.Count);
            graph = EnlargeGraph(graph, 5);
            return Dijkstra(graph, graph[(5 * width - 1, 5 * width - 1)]);
        }

        private static int Dijkstra(Dictionary<(int, int), Node> graph, Node target)
        {
            var queue = new PriorityQueue<Node, int>();
            graph[(0, 0)].Distance = 0;
            queue.Enqueue(graph[(0, 0)], 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.Visited)
                {
                    continue;
                }

                current.Visited = true;

                if (current == target)
                {
                    return target.Distance;
                }

                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    var alt = current.Distance + neighbor.Risk;
                    if (alt < neighbor.Distance)
                    {
                        neighbor.Distance = alt;
                    }

                    if (neighbor.Distance != int.MaxValue)
                    {
                        queue.Enqueue(neighbor, neighbor.Distance);
                    }
                }
            }

            return target.Distance;
        }

        private static IEnumerable<Node> GetNeighbors(this Dictionary<(int X, int Y), Node> graph, Node node)
        {
            return graph.Where(x =>
                                (x.Key == (node.X + 1, node.Y) ||
                                x.Key == (node.X - 1, node.Y) ||
                                x.Key == (node.X, node.Y + 1) ||
                                x.Key == (node.X, node.Y - 1)) &&
                                !x.Value.Visited
                            ).Select(x => x.Value);
        }

        private static Dictionary<(int X, int Y), Node> EnlargeGraph(Dictionary<(int X, int Y), Node> graph, int times)
        {
            var toReturn = new Dictionary<(int X, int Y), Node>();
            var width = (int)Math.Sqrt(graph.Count);
            for (var i = 0; i < times; i++)
            {
                for (var j = 0; j < times; j++)
                {
                    foreach (var node in graph)
                    {
                        (int x, int y) newKey = (node.Key.X + width * i, node.Key.Y + width * j);
                        var newRisk = (node.Value.Risk + i + j - 1) % 9 + 1;
                        toReturn.Add(newKey, new Node(newKey.x, newKey.y, newRisk));
                    }
                }
            }

            return toReturn;
        }

        private static Dictionary<(int, int), Node> ParseInput(string[] lines)
        {
            var toReturn = new Dictionary<(int, int), Node>();

            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Length; j++)
                {
                    var risk = int.Parse(lines[i][j].ToString());
                    toReturn.Add((i, j), new Node(i, j, risk, int.MaxValue, false));
                }
            }

            return toReturn;
        }
    }

    public class Node
    {
        public Node(int x, int y, int risk, int distance, bool visited)
        {
            this.X = x;
            this.Y = y;
            this.Risk = risk;
            this.Distance = distance;
            this.Visited = visited;
        }

        public Node(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Risk { get; set; }
        public int Distance { get; set; } = int.MaxValue;
        public bool Visited { get; set; } = false;

        public Node(int x, int y, int risk)
        {
            X = x;
            Y = y;
            Risk = risk;
        }
    }
}
