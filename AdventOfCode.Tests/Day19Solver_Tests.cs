using Day_19_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day19Solver_Tests
    {
        [Theory]
        [InlineData("Day19_Input/test.input", 79)]
        [InlineData("Day19_Input/puzzle.input", 338)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day19Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day19_Input/test.input", 3621)]
        [InlineData("Day19_Input/puzzle.input", 9862)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day19Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
