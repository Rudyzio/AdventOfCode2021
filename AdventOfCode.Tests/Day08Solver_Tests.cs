using Day_08_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day08Solver_Tests
    {
        [Theory]
        [InlineData("Day08_Input/test.input", 26)]
        [InlineData("Day08_Input/puzzle.input", 449)]	
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day08Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day08_Input/test.input", 61229)]
        [InlineData("Day08_Input/test1.input", 5353)]
        [InlineData("Day08_Input/puzzle.input", 968175)]
        public void TestPart2Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day08Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
