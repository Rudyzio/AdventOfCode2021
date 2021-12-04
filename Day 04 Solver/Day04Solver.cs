using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_04_Solver
{
    public static class Day04Solver
    {
        public static int Part1Solution(string[] lines)
        {
            var numberKeys = GetNumberKeys(lines[0]);
            var boards = GetBoards(lines);

            foreach (var number in numberKeys)
            {
                foreach (var board in boards)
                {
                    board.MarkPositionWithValue(number);
                    if (board.IsWinningBoard())
                    {
                        var unmarkedSum = board.GetUnmarkedSum();
                        return unmarkedSum * number;
                    }
                }
            }

            return 0;
        }

        public static int Part2Solution(string[] lines)
        {
            var numberKeys = GetNumberKeys(lines[0]);
            var boards = GetBoards(lines);

            foreach (var number in numberKeys)
            {
                foreach (var board in boards)
                {
                    board.MarkPositionWithValue(number);
                    if (boards.Count == 1)
                    {
                        if (board.IsWinningBoard())
                        {
                            var unmarkedSum = board.GetUnmarkedSum();
                            return unmarkedSum * number;
                        }
                    }
                }
                boards = boards.Where(x => !x.IsWinningBoard()).ToList();

            }

            return 0;
        }

        private static List<int> GetNumberKeys(string line)
        {
            return line.Split(",").Select(int.Parse).ToList();
        }

        private static List<Board> GetBoards(string[] lines)
        {
            var boards = new List<Board>();

            var currentBoard = new Board();
            var x = 0;
            var y = 0;

            foreach (var line in lines)
            {
                if (line.Contains(","))
                {
                    continue;
                }

                var splitted = line.Split(" ");
                if (string.IsNullOrEmpty(line))
                {
                    // System.Console.WriteLine();
                    if (currentBoard.Positions.Count > 0)
                    {
                        boards.Add(currentBoard);
                        // currentBoard.Print();
                        currentBoard = new Board();
                        x = 0;
                        y = 0;
                    }
                    continue;
                }

                foreach (var value in splitted)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        currentBoard.AddPosition(x, y, int.Parse(value));
                        x++;
                    }
                }

                y++;
                x = 0;
            }
            // System.Console.WriteLine();
            boards.Add(currentBoard);
            // currentBoard.Print();
            return boards;
        }
    }

    public class Board
    {
        public Board()
        {
            Positions = new List<Position>();
        }

        public List<Position> Positions { get; set; }

        public void AddPosition(int x, int y, int value)
        {
            // System.Console.WriteLine($"Added position x:{x} y:{y} with value: {value}");
            Positions.Add(new Position(x, y, value));
        }

        public void MarkPositionWithValue(int value)
        {
            var pos = Positions.SingleOrDefault(x => x.Value == value);
            if (pos != null)
                pos.Marked = true;
        }

        public bool IsWinningBoard()
        {
            var maxValueX = Positions.Select(x => x.X).Max(x => x);
            var maxValueY = Positions.Select(x => x.Y).Max(x => x);

            var marked = Positions.Count(x => x.Marked);

            if (marked < maxValueX)
            {
                return false;
            }

            // Check vertical
            for (var i = 0; i <= maxValueY; i++)
            {
                var row = Positions.Where(x => x.Y == i);
                if (row.All(x => x.Marked))
                {
                    return true;
                }
            }

            // Check horizontal
            for (var j = 0; j <= maxValueX; j++)
            {
                var column = Positions.Where(x => x.X == j);
                if (column.All(x => x.Marked))
                {
                    return true;
                }
            }

            return false;
        }

        public int GetUnmarkedSum()
        {
            return Positions.Where(x => !x.Marked).Sum(x => x.Value);
        }

        public void Print()
        {
            var maxValueX = Positions.Select(x => x.X).Max(x => x);
            var maxValueY = Positions.Select(x => x.Y).Max(x => x);

            for (var i = 0; i <= maxValueY; i++)
            {
                StringBuilder toPrint = new StringBuilder();
                for (var j = 0; j <= maxValueX; j++)
                {
                    var pos = Positions.Single(x => x.Y == i && x.X == j);
                    if (pos.Marked)
                    {
                        toPrint.Append($"{pos.Value}M ");
                    }
                    else
                    {
                        toPrint.Append($"{pos.Value} ");
                    }
                }
                System.Console.WriteLine(toPrint);
            }
        }
    }

    public class Position
    {
        public Position(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
            Marked = false;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool Marked { get; set; }
        public int Value { get; set; }
    }
}
