using System;

namespace Day_17_Solver
{
    public static class Day17Solver
    {
        public static int Part1Solution(string[] lines)
        {
            (var xInit, var xEnd, var yInit, var yEnd) = ParseInput(lines);

            /*

            From reddit: 

            To be able to achieve maximum height we need to be able to fall into the area vertically, thus we can disregard completely the component x. 
            Just assume it's in a value that will reach 0 between the area.
            My target y position was in the negatives. We know that the same speed we will reach upwards will be the speed with which we will reach 0. 
            For example, with speed 3 (speed, new position): (3,3), (2,5), (1,6), (0,6), (-1,5), (-2,3), (-3,0), (-4,-4)....
            The higher the initial velocity the higher the height too, thus we need to select the initial velocity so that initial+1 (in negative) falls at the lower
            end of the target.
            So if the target is -100, -50 we need an initial velocity of 99 in the y department. One less than the absolute number of the minimum y target number.
            Calculating maximum height is nothing more than the sum of 1+2+...+N-1+N, which is (N+1)*N/2.

            */

            var y = yInit;
            var initialVelocity = y * -1 - 1;
            var maxHeight = (initialVelocity + 1) * initialVelocity / 2;
            return maxHeight;
        }

        public static int Part2Solution(string[] lines)
        {
            (var xInit, var xEnd, var yInit, var yEnd) = ParseInput(lines);
            var hits = 0;

            var maxY = Math.Abs(yEnd) * 2;
            var maxX = xEnd * 2;

            for (int y = yInit; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (HitsTarget(x, y, xInit, xEnd, yInit, yEnd))
                    {
                        hits++;
                    }
                }
            }

            return hits;
        }

        private static bool HitsTarget(int vX, int vY, int xInit, int xEnd, int yInit, int yEnd)
        {
            int x = 0;
            int y = 0;

            while (true)
            {
                x += vX;
                y += vY;

                if (x >= xInit && x <= xEnd && y >= yInit && y <= yEnd)
                {
                    return true;
                }

                if (x > xEnd || y < yInit)
                {
                    return false;
                }

                if (vX > 0)
                {
                    vX -= 1;
                }
                else if (vX < 0)
                {
                    vX += 1;
                }

                vY -= 1;
            }
        }

        private static (int xInit, int xEnd, int yInit, int yEnd) ParseInput(string[] lines)
        {
            var line = lines[0];
            var splitValues = line.Split(": ")[1];
            var splitCoords = splitValues.Split(", ");
            var xCoordsInitial = int.Parse(splitCoords[0].Split("=")[1].Split("..")[0]);
            var xCoordsEnd = int.Parse(splitCoords[0].Split("=")[1].Split("..")[1]);

            var yCoordsInitial = int.Parse(splitCoords[1].Split("=")[1].Split("..")[0]);
            var yCoordsEnd = int.Parse(splitCoords[1].Split("=")[1].Split("..")[1]);

            return (xCoordsInitial, xCoordsEnd, yCoordsInitial, yCoordsEnd);
        }
    }
}
