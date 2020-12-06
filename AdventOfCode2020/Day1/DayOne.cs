using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day1
{
    public class DayOne : IAdventOfCode
    {
        private readonly int[] _input;
        private const int TARGET = 2020;

        public string Name => "Day 1";

        public DayOne()
        {
            var input = File.ReadAllLines(@".\Day1\input.txt");
            _input = input.Select(s => Convert.ToInt32(s)).ToArray();
        }

        public void PartOne()
        {
            HashSet<int> unique = new HashSet<int>();
            for (int i = 0; i < _input.Length; ++i)
            {
                var diff = TARGET - _input[i];
                if (unique.Contains(diff))
                {
                    Console.WriteLine(diff * _input[i]);
                    break;
                }
                unique.Add(_input[i]);
            }
        }

        public void PartTwo()
        {
            for (int j = 0; j < _input.Length - 2; j++)
            {
                HashSet<int> unique = new HashSet<int>();
                int currSum = TARGET - _input[j];

                for (int i = j + 1; i < _input.Length; i++)
                {
                    var diff = currSum - _input[i];
                    if (unique.Contains(diff))
                    {
                        Console.WriteLine(_input[j] * _input[i] * diff);
                        break;
                    }
                    unique.Add(_input[i]);
                }
            }
        }
    }
}
