using Day_12_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day12Solver_Tests
    {
        [Theory]
        [InlineData("Day12_Input/test1.input", 10)]
        [InlineData("Day12_Input/test2.input", 19)]
        [InlineData("Day12_Input/test3.input", 226)]
        [InlineData("Day12_Input/puzzle.input", 3802)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day12Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day12_Input/test1.input", 36)]
        [InlineData("Day12_Input/test2.input", 103)]
        [InlineData("Day12_Input/test3.input", 3509)]
        [InlineData("Day12_Input/puzzle.input", 99448)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day12Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
