using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Day8
{
    public class DayEight : IAdventOfCode
    {
        private readonly string[] _input;
        private HashSet<int> _linesVisited;

        public string Name => "--- Day 8: Handheld Halting ---";

        public DayEight()
        {
            _input = File.ReadAllLines(@".\Day8\input.txt");
        }

        public void PartOne()
        {
            _linesVisited = new HashSet<int>();
            int acc = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                if (_linesVisited.Contains(i))
                {
                    Console.WriteLine(acc);
                    break;
                }

                _linesVisited.Add(i);

                // parse instruction
                var lineSplit = _input[i].Split(' ');
                var instruction = lineSplit[0];
                var counter = int.Parse(lineSplit[1]);

                if (instruction.Equals("nop"))
                    continue;

                if (instruction.Equals("acc"))
                {
                    acc += counter;
                    continue;
                }

                if (instruction.Equals("jmp"))
                    i = i + counter - 1;
            }
        }

        public void PartTwo()
        {
            for (int i = 0; i < _input.Length - 1; i++)
            {
                int acc = FindSolution(i, out int pos);
                if (pos == _input.Length)
                {
                    Console.WriteLine(acc);
                    break;
                }
            }
        }

        private int FindSolution(int currentOuterIndex, out int pos)
        {
            var arr = new int[_input.Length];
            var acc = 0;
            pos = 0;
            while (arr[pos] == 0)
            {
                arr[pos] = 1;
                var split = _input[pos].Split(' ');
                var instruction = split[0];
                if (pos == currentOuterIndex)
                {
                    if (split[0] == "nop")
                        instruction = "jmp";

                    if (split[0] == "jmp")
                        instruction = "nop";
                }
                switch (instruction)
                {
                    case "nop":
                        pos++;
                        break;
                    case "jmp":
                        pos += int.Parse(split[1]);
                        break;
                    case "acc":
                        acc += int.Parse(split[1]);
                        pos++;
                        break;
                }
                if (pos >= _input.Length)
                    break;
            }

            return acc;
        }
    }
}
