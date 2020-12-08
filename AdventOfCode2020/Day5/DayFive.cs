using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day5
{
    public class DayFive : IAdventOfCode
    {
        private string[] _input;

        public string Name => "--- Day 5: Binary Boarding ---";

        private readonly HashSet<int> _seatId;

        public DayFive()
        {
            _input = File.ReadAllLines(@".\Day5\input.txt");
            _seatId = new HashSet<int>();
        }

        public void PartOne()
        {
            foreach (var line in _input)
            {
                var seatId = line.Replace("B", "1")
                     .Replace("F", "0")
                     .Replace("L", "0")
                     .Replace("R", "1");

                _seatId.Add(Convert.ToInt32(seatId, 2));
            }

            Console.WriteLine(_seatId.Max());
        }

        public void PartTwo()
        {
            var (min, max) = (_seatId.Min(), _seatId.Max());
            var seatId = Enumerable.Range(min, max - min + 1)
                .Single(i => !_seatId.Contains(i));

            Console.WriteLine(seatId);
        }
    }
}
