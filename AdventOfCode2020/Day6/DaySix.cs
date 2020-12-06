using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day6
{
    public class DaySix : IAdventOfCode
    {
        private readonly List<List<string>> _input;

        public DaySix()
        {
            _input = ParseInput();
        }

        public string Name => "Day 6";

        public void PartOne()
        {
            var sum = _input.Sum(g =>
            {
                return g.SelectMany(s => s)
                    .Distinct()
                    .Count();
            });

            Console.WriteLine(sum);
        }

        public void PartTwo()
        {
            var total = 0;
            foreach (var group in _input)
            {
                total += group.SelectMany(s => s).Distinct().Where(c => group.All(g => g.Contains(c))).Count();
            }

            Console.WriteLine(total);
        }

        private List<List<string>> ParseInput()
        {
            var result = new List<List<string>>()
            {
                new List<string>()
            };

            var currentIndex = 0;

            foreach (var line in File.ReadAllLines(@".\Day6\input.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    result.Add(new List<string>());
                    ++currentIndex;
                    continue;
                }

                result[currentIndex].Add(line);
            }

            return result;
        }
    }
}
