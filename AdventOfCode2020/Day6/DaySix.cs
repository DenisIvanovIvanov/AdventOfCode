using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day6
{
    public class DaySix : DayBase, IAdventOfCode
    {
        private readonly List<List<string>> _input;

        public DaySix() : base()
        {
            _input = ParseInput(@".\Day6\input.txt");
        }

        public string Name => "--- Day 6: Custom Customs ---";

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
    }
}
