using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day2
{
    public class DayTwo : IAdventOfCode
    {
        private readonly List<Password> _passwords;

        class Password
        {
            public int Min { get; set; }

            public int Max { get; set; }

            public char Letter { get; set; }

            public string PasswordString { get; set; }
        }

        public string Name => "Day 2";

        public DayTwo()
        {
            var input = File.ReadAllLines(@".\Day2\input.txt");
            _passwords = input.Select(i =>
            {
                var splitByEmpty = i.Split(' '); // [0] is range, [1] char we look for, [2] is password itself
                var range = splitByEmpty[0].Split('-');

                return new Password()
                {
                    Min = int.Parse(range[0]),
                    Max = int.Parse(range[1]),
                    Letter = splitByEmpty[1][0],
                    PasswordString = splitByEmpty[2]
                };
            }).ToList();
        }

        public void PartOne()
        {
            var validPasswords = 0;
            foreach (var password in _passwords)
            {
                var letterCount = password.PasswordString.Where(p => p == password.Letter).Count();
                if (letterCount >= password.Min && letterCount <= password.Max)
                    validPasswords++;
            }
            Console.WriteLine(validPasswords);
        }

        public void PartTwo()
        {
            var validPasswords = 0;
            foreach (var password in _passwords)
            {
                var firstCharacter = password.PasswordString[password.Min - 1] == password.Letter;
                var secondCharacter = password.PasswordString[password.Max - 1] == password.Letter;
                if (firstCharacter ^ secondCharacter)
                    validPasswords++;
            }

            Console.WriteLine(validPasswords);
        }
    }
}
