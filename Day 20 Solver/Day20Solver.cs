using System;
using System.Linq;
using System.Text;

namespace Day_20_Solver
{
    public static class Day20Solver
    {
        public static int Part1Solution(string[] lines)
        {
            (var algorithm, var image) = ParseInput(lines);

            int steps = 2;
            int step = 0;
            int toReturn = 0;
            var outside = false;
            while (step < steps)
            {
                (var newImage, var lit) = Enhance(algorithm, image, outside);
                image = newImage;
                toReturn = lit;

                if (algorithm[0])
                {
                    outside = !outside;
                }

                step++;
            }

            return toReturn;
        }

        public static int Part2Solution(string[] lines)
        {
            (var algorithm, var image) = ParseInput(lines);

            int steps = 50;
            int step = 0;
            int toReturn = 0;
            var outside = false;
            while (step < steps)
            {
                (var newImage, var lit) = Enhance(algorithm, image, outside);
                image = newImage;
                toReturn = lit;

                if (algorithm[0])
                {
                    outside = !outside;
                }

                step++;
            }

            return toReturn;
        }

        private static (bool[,], int) Enhance(bool[] algorithm, bool[,] image, bool outside)
        {
            image = image.Resize(image.GetLength(0) + 2, image.GetLength(1) + 2);
            var toReturn = image.Copy();

            for (var i = 0; i < image.GetLength(0); i++)
            {
                image[i, 0] = outside;
                image[i, image.GetLength(0) - 1] = outside;
            }

            for (var j = 0; j < image.GetLength(1); j++)
            {
                image[0, j] = outside;
                image[image.GetLength(1) - 1, j] = outside;
            }

            int lit = 0;

            for (var i = 0; i < toReturn.GetLength(0); i++)
            {
                for (var j = 0; j < toReturn.GetLength(1); j++)
                {
                    var binary = GetBinary(image, i, j, outside);
                    var dec = BinaryToDecimal(binary);

                    toReturn[i, j] = algorithm[dec];
                    if (algorithm[dec])
                    {
                        lit++;
                    }
                }
            }

            return (toReturn, lit);
        }

        private static string GetBinary(bool[,] image, int i, int j, bool outside)
        {
            var builder = new StringBuilder();

            // Top left
            if (i - 1 >= 0 && j - 1 >= 0)
            {
                builder.Append(image[i - 1, j - 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Top
            if (i - 1 >= 0)
            {
                builder.Append(image[i - 1, j] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Top right
            if (i - 1 >= 0 && j + 1 < image.GetLength(1))
            {
                builder.Append(image[i - 1, j + 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Left
            if (j - 1 >= 0)
            {
                builder.Append(image[i, j - 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Center
            builder.Append(image[i, j] ? "1" : "0");

            // Right
            if (j + 1 < image.GetLength(1))
            {
                builder.Append(image[i, j + 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Bottom left
            if (i + 1 < image.GetLength(0) && j - 1 >= 0)
            {
                builder.Append(image[i + 1, j - 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Bottom
            if (i + 1 < image.GetLength(0))
            {
                builder.Append(image[i + 1, j] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            // Bottom right
            if (i + 1 < image.GetLength(0) && j + 1 < image.GetLength(1))
            {
                builder.Append(image[i + 1, j + 1] ? "1" : "0");
            }
            else
            {
                builder.Append(outside ? "1" : "0");
            }

            return builder.ToString();
        }

        private static int BinaryToDecimal(string input)
        {
            // System.Console.WriteLine(input);
            return Convert.ToInt32(input, 2);
        }

        private static (bool[] algorithm, bool[,] image) ParseInput(string[] lines)
        {
            var firstLine = lines[0];
            var algorithm = new bool[firstLine.Length];
            var image = new bool[lines.Count() - 2, lines[2].Count()];
            for (var i = 0; i < firstLine.Length; i++)
            {
                if (firstLine[i] == '#')
                {
                    algorithm[i] = true;
                }
                else
                {
                    algorithm[i] = false;
                }
            }

            for (var i = 2; i < lines.Count(); i++)
            {
                for (var j = 0; j < lines[i].Count(); j++)
                {
                    if (lines[i][j] == '#')
                    {
                        image[i - 2, j] = true;
                    }
                    else
                    {
                        image[i - 2, j] = false;
                    }
                }
            }

            return (algorithm, image);
        }

        private static T[,] Resize<T>(this T[,] original, int rows, int cols)
        {
            var newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));
            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i + 1, j + 1] = original[i, j];
            return newArray;
        }

        private static T[,] Copy<T>(this T[,] original)
        {
            int rows = original.GetLength(0);
            int cols = original.GetLength(1);
            var newArray = new T[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }

        private static void Print(this bool[,] image)
        {
            for (var i = 0; i < image.GetLength(0); i++)
            {
                var builder = new StringBuilder();
                for (var j = 0; j < image.GetLength(1); j++)
                {
                    if (image[i, j])
                    {
                        builder.Append("#");
                    }
                    else
                    {
                        builder.Append(".");
                    }
                }
                System.Console.WriteLine(builder.ToString());
            }
        }
    }
}
