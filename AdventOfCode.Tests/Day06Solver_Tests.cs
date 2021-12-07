using Day_06_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day06Solver_Tests
    {
        [Theory]
        [InlineData("Day06_Input/test.input", 5934)]
        [InlineData("Day06_Input/puzzle.input", 373378)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day06Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day06_Input/test.input", 26984457539)]
        [InlineData("Day06_Input/puzzle.input", 1682576647495)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day06Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
