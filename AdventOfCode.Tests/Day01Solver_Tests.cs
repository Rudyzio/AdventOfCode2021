using Day_01_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day01Solver_Tests
    {
        [Theory]
        [InlineData("Day01_Input/test.input", 7)]
        [InlineData("Day01_Input/puzzle.input", 1581)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day01Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day01_Input/test.input", 5)]
        [InlineData("Day01_Input/puzzle.input", 1618)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day01Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
