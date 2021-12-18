using Day_17_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day17Solver_Tests
    {
        [Theory]
        [InlineData("Day17_Input/test.input", 45)]
        [InlineData("Day17_Input/puzzle.input", 13203)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day17Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day17_Input/test.input", 112)]
        [InlineData("Day17_Input/puzzle.input", 5644)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day17Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
