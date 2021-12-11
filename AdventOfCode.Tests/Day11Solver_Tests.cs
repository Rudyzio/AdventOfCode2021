using Day_11_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day11Solver_Tests
    {
        [Theory]
        [InlineData("Day11_Input/test.input", 1656)]
        [InlineData("Day11_Input/test1.input", 259)]
        [InlineData("Day11_Input/puzzle.input", 1683)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day11_Input/test.input", 195)]
        [InlineData("Day11_Input/puzzle.input", 788)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day11Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
