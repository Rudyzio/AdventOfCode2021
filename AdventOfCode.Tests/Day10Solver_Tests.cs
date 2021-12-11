using Day_10_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day10Solver_Tests
    {
        [Theory]
        [InlineData("Day10_Input/test.input", 26397)]
        [InlineData("Day10_Input/puzzle.input", 462693)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day10Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day10_Input/test.input", 288957)]
        [InlineData("Day10_Input/puzzle.input", 3094671161)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day10Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
