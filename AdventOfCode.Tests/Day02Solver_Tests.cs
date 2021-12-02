using Day_02_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day02Solver_Tests
    {
        [Theory]
        [InlineData("Day02_Input/test.input", 150)]
        [InlineData("Day02_Input/puzzle.input", 1989265)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day02Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day02_Input/test.input", 900)]
        [InlineData("Day02_Input/puzzle.input", 2089174012)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day02Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
