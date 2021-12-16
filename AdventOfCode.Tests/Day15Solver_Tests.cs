using Day_15_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day15Solver_Tests
    {
        [Theory]
        [InlineData("Day15_Input/test.input", 40)]
        [InlineData("Day15_Input/test1.input", 12)]
        [InlineData("Day15_Input/puzzle.input", 361)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day15_Input/test.input", 315)]
        [InlineData("Day15_Input/test1.input", 204)]
        [InlineData("Day15_Input/puzzle.input", 2838)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day15Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
