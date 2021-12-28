using Day_20_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day20Solver_Tests
    {
        [Theory]
        [InlineData("Day20_Input/test.input", 35)]
        [InlineData("Day20_Input/puzzle.input", 5361)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day20Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day20_Input/test.input", 3351)]
        [InlineData("Day20_Input/puzzle.input", 16826)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day20Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
