using Day_16_Solver;
using Xunit;
namespace AdventOfCode.Tests
{
    public class Day16Solver_Tests
    {
        [Theory]
        [InlineData("Day16_Input/test1.input", 16)]
        [InlineData("Day16_Input/test2.input", 12)]
        [InlineData("Day16_Input/test3.input", 23)]
        [InlineData("Day16_Input/test4.input", 31)]
        [InlineData("Day16_Input/puzzle.input", 895)]
        public void TestPart1Solution(string inputFile, int expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day16Solver.Part1Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Day16_Input/test5.input", 3)]
        [InlineData("Day16_Input/test6.input", 54)]
        [InlineData("Day16_Input/test7.input", 7)]
        [InlineData("Day16_Input/test8.input", 9)]
        [InlineData("Day16_Input/test9.input", 1)]
        [InlineData("Day16_Input/test10.input", 0)]
        [InlineData("Day16_Input/test11.input", 0)]
        [InlineData("Day16_Input/test12.input", 1)]
        [InlineData("Day16_Input/puzzle.input", 1148595959144)]
        public void TestPart2Solution(string inputFile, long expected)
        {
            // Arrange
            string[] lines = System.IO.File.ReadAllLines($"../../../{inputFile}");

            // Act
            var result = Day16Solver.Part2Solution(lines);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
