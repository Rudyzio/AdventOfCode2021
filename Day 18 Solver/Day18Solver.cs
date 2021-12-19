using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_18_Solver
{
    public static class Day18Solver
    {
        public static long Part1Solution(string[] lines)
        {
            var trees = ParseInput(lines);
            Node currentNumber = null;

            while (trees.Count > 0)
            {
                var root = trees.Dequeue();
                if (currentNumber == null)
                {
                    currentNumber = root;
                }
                else
                {
                    currentNumber = AddNodes(currentNumber, root);
                }

                Reduce(currentNumber);
            }
            // System.Console.WriteLine(currentNumber.Print(string.Empty, true));

            return CalculateMagnitude(currentNumber);
        }

        public static long Part2Solution(string[] lines)
        {
            long maxMag = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines.Length; j++)
                {
                    if (i != j)
                    {
                        var ptr1 = 0;
                        var depth1 = 0;
                        var ptr2 = 0;
                        var depth2 = 0;
                        var node1 = Read(lines[i], ref ptr1, depth1);
                        var node2 = Read(lines[j], ref ptr2, depth2);
                        var currentNumber = AddNodes(node1, node2);

                        Reduce(currentNumber);

                        var max = CalculateMagnitude(currentNumber);
                        if (max > maxMag)
                        {
                            maxMag = max;
                        }
                    }
                }
            }

            return maxMag;
        }

        private static void Reduce(Node currentNumber)
        {
            Node node = null;
            do
            {
                node = null;
                node = IsNestedInsideFourPairs(currentNumber);
                if (node != null)
                {
                    Explode(node, currentNumber);
                }
                else
                {
                    node = HasValueBiggerThanTen(currentNumber);
                    if (node != null)
                    {
                        Split(node);
                    }
                }


            } while (node != null);
        }

        private static void Explode(Node node, Node root)
        {
            Pair parentPair = (Pair)node.Parent;
            var leftValue = ((Literal)((Pair)node).Left);
            var rightValue = ((Literal)((Pair)node).Right);

            int stepsToLeft = 0;
            int stepsToRight = 0;

            var foundValueLeft = TraverseLeft(leftValue.Value, node, ref stepsToLeft);
            var foundValueRight = TraverseRight(rightValue.Value, node, ref stepsToRight);

            if (!foundValueLeft)
            {
                parentPair.Left = new Literal(0, leftValue.Depth - 1, parentPair);
            }

            if (!foundValueRight)
            {
                parentPair.Right = new Literal(0, rightValue.Depth - 1, parentPair);
            }

            if (foundValueLeft && foundValueRight)
            {
                if (stepsToRight > stepsToLeft)
                {
                    parentPair.Right = new Literal(0, rightValue.Depth - 1, parentPair);
                }
                else
                {
                    parentPair.Left = new Literal(0, leftValue.Depth - 1, parentPair);

                }
            }

        }
        
        private static bool TraverseLeft(int value, Node node, ref int steps)
        {
            var parentNode = (Pair)node.Parent;
            while (true)
            {
                steps++;
                if (parentNode == null)
                {
                    return false;
                }
                if (node == parentNode.Left)
                {
                    node = parentNode;
                    parentNode = (Pair)parentNode.Parent;
                    continue;
                }
                node = parentNode;

                break;
            }

            node = ((Pair)node).Left;

            while (true)
            {
                steps++;
                if (node.GetType() == typeof(Literal))
                {
                    // System.Console.WriteLine("Changing");
                    // System.Console.WriteLine(node.Print(string.Empty, true));
                    ((Literal)node).Value += value;
                    return true;
                }

                parentNode = (Pair)node;
                node = ((Pair)node).Right;
            }
        }
        
        private static bool TraverseRight(int value, Node node, ref int steps)
        {
            var parentNode = (Pair)node.Parent;
            while (true)
            {
                steps++;
                // System.Console.WriteLine(parentNode.Print(string.Empty, true));
                if (parentNode == null)
                {
                    return false;
                }
                if (node == parentNode.Right)
                {
                    node = parentNode;
                    parentNode = (Pair)parentNode.Parent;
                    continue;
                }
                node = parentNode;

                break;
            }

            node = ((Pair)node).Right;
            while (true)
            {
                steps++;
                parentNode = (Pair)node.Parent;
                if (node.GetType() == typeof(Literal))
                {
                    // System.Console.WriteLine("Changing");
                    // System.Console.WriteLine(node.Print(string.Empty, true));
                    ((Literal)node).Value += value;
                    return true;
                }

                parentNode = (Pair)node;
                node = ((Pair)node).Left;
            }
        }

        private static (Node node, bool explode) CheckNextAction(Node node)
        {
            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                if (lit.Depth >= 4)
                {
                    return (lit.Parent, true);
                }
                if (lit.Value >= 10)
                {
                    return (node, false);
                }
            }

            if (node.GetType() == typeof(Pair))
            {
                Pair pair = (Pair)node;
                var left = CheckNextAction(pair.Left);
                if (left.node != null)
                    return left;
                var right = CheckNextAction(pair.Right);
                if (right.node != null)
                    return right;
            }

            return (null, false);
        }

        private static Node IsNestedInsideFourPairs(Node node)
        {
            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                if (lit.Depth >= 4)
                {
                    return lit.Parent;
                }
            }

            if (node.GetType() == typeof(Pair))
            {
                Pair pair = (Pair)node;
                var left = IsNestedInsideFourPairs(pair.Left);
                if (left != null)
                    return left;
                var right = IsNestedInsideFourPairs(pair.Right);
                if (right != null)
                    return right;
            }

            return null;
        }

        private static Node HasValueBiggerThanTen(Node node)
        {
            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                if (lit.Value >= 10)
                {
                    return node;
                }
            }

            if (node.GetType() == typeof(Pair))
            {
                Pair pair = (Pair)node;
                var left = HasValueBiggerThanTen(pair.Left);
                if (left != null)
                    return left;
                var right = HasValueBiggerThanTen(pair.Right);
                if (right != null)
                    return right;
            }

            return null;
        }

        private static void Split(Node node)
        {
            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                if (lit.Value >= 10)
                {
                    int left = 0;
                    int right = 0;
                    if (lit.Value % 2 != 0)
                    {
                        left = lit.Value / 2;
                        right = lit.Value / 2 + 1;
                    }
                    else
                    {
                        left = lit.Value / 2;
                        right = lit.Value / 2;
                    }

                    if (((Pair)node.Parent).Right == node)
                    {
                        ((Pair)node.Parent).Right = new Pair(null, null, node.Parent);
                        var leftLit = new Literal(left, lit.Depth + 1, ((Pair)node.Parent).Right);
                        var rightLit = new Literal(right, lit.Depth + 1, ((Pair)node.Parent).Right);
                        ((Pair)((Pair)node.Parent).Right).Right = rightLit;
                        ((Pair)((Pair)node.Parent).Right).Left = leftLit;
                    }
                    else
                    {
                        ((Pair)node.Parent).Left = new Pair(null, null, node.Parent);
                        var leftLit = new Literal(left, lit.Depth + 1, ((Pair)node.Parent).Left);
                        var rightLit = new Literal(right, lit.Depth + 1, ((Pair)node.Parent).Left);
                        ((Pair)((Pair)node.Parent).Left).Right = rightLit;
                        ((Pair)((Pair)node.Parent).Left).Left = leftLit;
                    }
                }
            }
        }

        private static Node AddNodes(Node first, Node second)
        {
            var newRoot = new Pair(null, null, null);
            first.Parent = newRoot;
            second.Parent = newRoot;
            newRoot.Left = first;
            newRoot.Right = second;

            AddDepthByOne(newRoot);

            return newRoot;
        }

        private static void AddDepthByOne(Node node)
        {
            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                lit.Depth = lit.Depth + 1;
            }

            if (node.GetType() == typeof(Pair))
            {
                Pair pair = (Pair)node;
                AddDepthByOne(pair.Left);
                AddDepthByOne(pair.Right);
            }
        }

        private static Queue<Node> ParseInput(string[] lines)
        {
            var toReturn = new Queue<Node>();

            foreach (var line in lines)
            {
                var ptr = 0;
                var depth = 0;
                var tree = Read(line, ref ptr, depth);
                toReturn.Enqueue(tree);
            }

            return toReturn;
        }

        private static Node Read(string line, ref int ptr, int depth, Node parent = null)
        {
            var node = new Pair(null, null, parent);
            if (line[ptr] == '[')
            {
                ptr++;
            }

            if (line[ptr] == '[')
            {
                node.Left = Read(line, ref ptr, depth + 1, node);
            }
            else
            {
                if (char.IsDigit(line[ptr]))
                {
                    node.Left = new Literal(int.Parse(line[ptr].ToString()), depth, node);
                }
            }

            ptr += 2;

            if (line[ptr] == '[')
            {
                node.Right = Read(line, ref ptr, depth + 1, node);
            }
            else
            {
                if (char.IsDigit(line[ptr]))
                {
                    node.Right = new Literal(int.Parse(line[ptr].ToString()), depth, node);
                }
            }

            ptr++;

            return node;
        }

        private static long CalculateMagnitude(Node node)
        {

            if (node.GetType() == typeof(Literal))
            {
                Literal lit = (Literal)node;
                return lit.Value;
            }

            if (node.GetType() == typeof(Pair))
            {
                Pair pair = (Pair)node;
                var left = 3 * CalculateMagnitude(pair.Left);
                var right = 2 * CalculateMagnitude(pair.Right);
                return left + right;
            }

            return 0;
        }

    }

    public abstract class Node
    {
        public Node(Node parent)
        {
            Parent = parent;
        }

        public Node Parent { get; set; }

        public abstract string Print(string indent, bool last);
    }

    public class Pair : Node
    {
        public Pair(Node left, Node right, Node parent) : base(parent)
        {
            Left = left;
            Right = right;
        }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public override string Print(string indent, bool last)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(indent);

            if (last)
            {
                stringBuilder.Append("\\-");
                indent += "  ";
            }
            else
            {
                stringBuilder.Append("|-");
                indent += "| ";
            }

            stringBuilder.AppendLine("g");

            indent += "  ";

            if (Left != null)
                stringBuilder.Append(Left.Print(indent, false));
            if (Right != null)
                stringBuilder.Append(Right.Print(indent, true));

            return stringBuilder.ToString();
        }
    }

    public class Literal : Node
    {
        public Literal(int value, int depth, Node parent) : base(parent)
        {
            Value = value;
            Depth = depth;
        }

        public int Value { get; set; }

        public int Depth { get; set; }

        public override string Print(string indent, bool last)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(indent);
            stringBuilder.Append(last ? "\\-" : "|-");
            stringBuilder.AppendLine(Value.ToString() + " -- " + Depth.ToString());
            // stringBuilder.AppendLine(Value.ToString());

            return stringBuilder.ToString();
        }
    }
}
