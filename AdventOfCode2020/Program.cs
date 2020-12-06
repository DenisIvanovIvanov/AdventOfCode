using AdventOfCode2020.Day1;
using AdventOfCode2020.Day2;
using AdventOfCode2020.Day3;
using AdventOfCode2020.Day4;
using AdventOfCode2020.Day5;
using AdventOfCode2020.Day6;
using System;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            IAdventOfCode[] solutions = new IAdventOfCode[]
            {
                new DayOne(),
                new DayTwo(),
                new DayThree(),
                new DayFour(),
                new DayFive(),
                new DaySix()
            };
            
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution.Name);
                solution.PartOne();
                solution.PartTwo();
                Console.WriteLine("===============================================");
            }

            Console.Read();
        }
    }
}
