using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4
{
    public class DayFour : DayBase, IAdventOfCode
    {
        private readonly HashSet<string> _requiredFields = new HashSet<string>
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };

        private readonly HashSet<string> _validEyeColors = new HashSet<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };

        private readonly List<string> _validPassports;
        private readonly string _heightRegex = @"(cm|in)\b";
        private readonly string _hexColorRegex = @"^#(?:[0-9a-fA-F]{3}){1,2}$";

        public string Name => "--- Day 4: Passport Processing ---";

        public DayFour() : base()
        {
            _validPassports = GetAllPassportsHavingRequiredKeys();
        }

        public void PartOne() => Console.WriteLine(_validPassports.Count);

        public void PartTwo()
        {
            int validPassports = 0;
            foreach (var validPassport in _validPassports)
            {
                if (ValidatePassport(validPassport.Split(' ').ToList()))
                    validPassports++;
            }

            Console.WriteLine(validPassports);
        }

        private bool ValidatePassport(List<string> passport)
        {
            bool valid = true;

            foreach (var kv in passport)
            {
                if (!valid)
                    break;

                var split = kv.Split(':');
                var key = split[0];
                var value = split[1];
                valid = key switch
                {
                    "byr" => ValidateDateRange(value, 1920, 2002),
                    "iyr" => ValidateDateRange(value, 2010, 2020),
                    "eyr" => ValidateDateRange(value, 2020, 2030),
                    "hgt" => ValidateHeight(value),
                    "hcl" => Regex.Match(value, _hexColorRegex).Success,
                    "ecl" => _validEyeColors.Contains(value),
                    "pid" => value.Length == 9,
                    "cid" => true,
                    _ => throw new ArgumentException(nameof(key)),
                };
            }

            return valid;
        }

        private bool ValidateHeight(string value)
        {
            var match = Regex.Match(value, _heightRegex);
            if (match.Success)
            {
                value = value[0..^2];
                return match.Value switch
                {
                    "cm" => int.TryParse(value, out int eyrValue) && !(eyrValue < 150 || eyrValue > 193),
                    "in" => int.TryParse(value, out int inchValue) && !(inchValue < 59 || inchValue > 76),
                    _ => false,
                };
            }

            return false;
        }

        private bool ValidateDateRange(string value, int start, int end)
            => !(value.Length < 4 || value.Length > 4) 
                && (int.TryParse(value, out int eyrValue) 
                && !(eyrValue < start || eyrValue > end));

        private List<string> GetAllPassportsHavingRequiredKeys()
        {
            var passports = ParseInput(@".\Day4\input.txt");
            var validPassports = new List<string>();

            foreach (var passport in passports)
            {
                var asSingleString = String.Join(' ', passport);

                if (_requiredFields.All(f => asSingleString.Split(' ').Select(f => f.Split(':')[0]).Contains(f)))
                    validPassports.Add(asSingleString);
            }

            return validPassports;
        }
    }
}
