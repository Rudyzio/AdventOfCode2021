using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_16_Solver
{
    public static class Day16Solver
    {
        public static long Part1Solution(string[] lines)
        {
            var hexadecimal = lines[0];
            var binary = HexadecimalToBinary(hexadecimal);
            (var value, var length, var version) = ProcessPacket(binary, 0);

            return version;
        }

        public static long Part2Solution(string[] lines)
        {
            var hexadecimal = lines[0];
            var binary = HexadecimalToBinary(hexadecimal);
            (var value, var length, var version) = ProcessPacket(binary, 0);
            return value;
        }

        private static (long Value, int Length, long Version) ProcessPacket(string input, int packetInitialPos)
        {
            var packetVersion = GetPacketVersion(input, packetInitialPos);
            var packetTypeId = GetPacketTypeId(input, packetInitialPos);
            int lengthOp = 0;
            long versionOp = 0;
            List<long> valuesOp = new();

            if (packetTypeId != 4)
            {
                (lengthOp, versionOp, valuesOp) = GetSubPacketOperatorValues(input, packetInitialPos, packetVersion);
            }

            switch (packetTypeId)
            {
                case 0:
                    return (valuesOp.Sum(), lengthOp, versionOp);
                case 1:
                    return (valuesOp.Aggregate(1L, (a, b) => a * b), lengthOp, versionOp);
                case 2:
                    return (valuesOp.Min(), lengthOp, versionOp);
                case 3:
                    return (valuesOp.Max(), lengthOp, versionOp);
                case 4:
                    (var value, var length) = ProcessLiteralValuePacket(input, packetInitialPos + 6);
                    return (value, length + 6, packetVersion);
                case 5:
                    return (valuesOp[0] > valuesOp[1] ? 1 : 0, lengthOp, versionOp);
                case 6:
                    return (valuesOp[0] < valuesOp[1] ? 1 : 0, lengthOp, versionOp);
                case 7:
                    return (valuesOp[0] == valuesOp[1] ? 1 : 0, lengthOp, versionOp);
            }

            // Never goes here
            return (0, 0, packetVersion);
        }

        private static (int Length, long Version, List<long> values) GetSubPacketOperatorValues(string input, int packetInitialPos, long packetVersion)
        {
            var toReturn = new List<long>();

            var lengthTypeId = GetLengthTypeId(input, packetInitialPos);

            if (lengthTypeId == 0)
            {
                var subPacketsLength = GetSubPacketsLength(input, packetInitialPos);

                int counter = 0;
                while (counter < subPacketsLength)
                {
                    (var value, var length, var version) = ProcessPacket(input, packetInitialPos + 22 + counter);
                    counter += length;
                    packetVersion += version;
                    toReturn.Add(value);
                }
                return (counter + 22, packetVersion, toReturn);
            }
            else
            {
                var nSubPackets = GetNumberOfSubPackets(input, packetInitialPos);
                int packetsCounted = 0;
                int counter = 0;
                while (packetsCounted < nSubPackets)
                {
                    (var value, var length, var version) = ProcessPacket(input, packetInitialPos + 18 + counter);
                    counter += length;
                    packetsCounted++;
                    packetVersion += version;
                    toReturn.Add(value);
                }
                return (counter + 18, packetVersion, toReturn);
            }
        }

        private static (long Value, int Length) ProcessLiteralValuePacket(string input, int position)
        {
            int currentPosition = position;
            bool lastGroup = false;
            string binaryValue = string.Empty;
            do
            {
                var lastGroupBit = int.Parse(input[currentPosition].ToString());

                if (lastGroupBit == 0)
                {
                    lastGroup = true;
                }

                binaryValue += input.Substring(currentPosition + 1, 4);
                currentPosition += 5;
            }
            while (!lastGroup);

            return (BinaryToDecimal(binaryValue), currentPosition - position);
        }

        private static string HexadecimalToBinary(string input)
        {
            return string.Join(string.Empty,
                input.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                ));
        }

        private static long GetPacketVersion(string input, int start)
        {
            var binaryPacketVersion = input.Substring(start, 3);
            return BinaryToDecimal(binaryPacketVersion);
        }

        private static long GetPacketTypeId(string input, int start)
        {
            var binaryPacketTypeId = input.Substring(start + 3, 3);
            return BinaryToDecimal(binaryPacketTypeId);
        }

        private static int GetLengthTypeId(string input, int start)
        {
            return int.Parse(input[start + 6].ToString());
        }

        private static long GetSubPacketsLength(string input, int start)
        {
            var subPacketLength = input.Substring(start + 7, 15);
            return BinaryToDecimal(subPacketLength);
        }

        private static long GetNumberOfSubPackets(string input, int start)
        {
            var nSubPackets = input.Substring(start + 7, 11);
            return BinaryToDecimal(nSubPackets);
        }

        private static long BinaryToDecimal(string input)
        {
            return Convert.ToInt64(input, 2);
        }
    }
}
