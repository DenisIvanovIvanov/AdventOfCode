using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day3
{
    public class DayThree : IAdventOfCode
    {
        private readonly string[] _input;

        public DayThree()
        {
            _input = File.ReadAllLines(@".\Day3\input.txt");
        }

        public string Name => "--- Day 3: Toboggan Trajectory ---";

        public void PartOne()
        {
            int trees = 1;

            int[][] slopes = new int[][]
            {
                new int[] { 3, 1}
            };

            foreach (var slopeTrees in YieldSlopeTrees(slopes))
                trees *= slopeTrees;

            Console.WriteLine(trees);
        }

        public void PartTwo()
        {
            // [right, down]
            int[][] slopes = new int[][]
            {
                new[] { 1, 1 },
                new[] { 3, 1 },
                new[] { 5, 1 },
                new[] { 7, 1 },
                new[] { 1, 2 }
            };

            long sum = 1;
            foreach (var slopeTrees in YieldSlopeTrees(slopes))
                sum *= slopeTrees;

            Console.WriteLine(sum);
        }

        private IEnumerable<int> YieldSlopeTrees(int[][] slopes)
        {
            for (int j = 0; j < slopes.Length; j++)
            {
                var slope = slopes[j];
                int trees = 0;
                int rightIndex = 0;
                for (int i = 0; i < _input.Length; i += slope[1])
                {
                    var line = _input[i];
                    if (rightIndex >= line.Length)
                        rightIndex -= line.Length;

                    var block = line.Substring(rightIndex, 1);
                    if (block == "#")
                        trees++;

                    rightIndex += slope[0];
                }

                yield return trees;
            }
        }
    }
}
