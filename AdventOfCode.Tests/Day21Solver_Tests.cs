using Day_21_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day21Solver_Tests
    {
        [Theory]
        [InlineData("Day21_Input/test.input", 739785)]
        [InlineData("Day21_Input/puzzle.input", 556206)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day21Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day21_Input/test.input", 444356092776315)]
        [InlineData("Day21_Input/puzzle.input", 630797200227453)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day21Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
