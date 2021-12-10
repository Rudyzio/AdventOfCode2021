using Day_09_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day09Solver_Tests
    {
        [Theory]
        [InlineData("Day09_Input/test.input", 15)]
        [InlineData("Day09_Input/puzzle.input", 496)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day09Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day09_Input/test.input", 1134)]
        [InlineData("Day09_Input/puzzle.input", 902880)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day09Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
