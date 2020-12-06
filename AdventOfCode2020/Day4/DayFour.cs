using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day4
{
    public class DayFour : IAdventOfCode
    {
        private readonly HashSet<string> _requiredFields = new HashSet<string>
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl", 
            "pid"
        };

        private readonly HashSet<string> _validEyeColors = new HashSet<string>
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth"
        };

        private StreamReader _reader;
        private string _heightRegex = @"(cm|in)\b";

        public string Name => "Day 4";

        public void PartOne()
        {
            Prepare();
            int validPassports = 0;
            List<string> passportFields = new List<string>();
            while (!_reader.EndOfStream)
            {
                string line = _reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    foreach (var field in line.Split(' ').Where(f => !f.StartsWith("cid"))) // cid is optional
                        passportFields.Add(field.Split(':')[0]); // we are not interested in value
                }

                if (string.IsNullOrEmpty(line) || _reader.EndOfStream)
                {
                    if (_requiredFields.All(f => passportFields.Contains(f)))
                        validPassports++;

                    passportFields.Clear();
                }
            }

            _reader.Close();
            Console.WriteLine(validPassports);
        }

        public void PartTwo()
        {
            Prepare();
            int validPassports = 0;
            List<string> passportFields = new List<string>();
            while (!_reader.EndOfStream)
            {
                string line = _reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    foreach (var field in line.Split(' ').Where(f => !f.StartsWith("cid"))) // cid is optional
                        passportFields.Add(field);
                }

                if (string.IsNullOrEmpty(line) || _reader.EndOfStream)
                {
                    // first check if we have all keys required
                    if (_requiredFields.All(f => passportFields.Select(f => f.Split(':')[0]).Contains(f)))
                    {
                        if (ValidatePassport(passportFields))
                            validPassports++;
                    }

                    passportFields.Clear();
                }
            }

            _reader.Close();
            Console.WriteLine(validPassports);
        }

        private void Prepare()
        {
            _reader = new StreamReader(@".\Day4\input.txt");
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
                    "hcl" => Regex.Match(value, @"^#(?:[0-9a-fA-F]{3}){1,2}$").Success,
                    "ecl" => _validEyeColors.Contains(value),
                    "pid" => value.Length == 9,
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
                switch (match.Value)
                {
                    case "cm":
                        if (int.TryParse(value, out int eyrValue))
                        {
                            if (eyrValue < 150 || eyrValue > 193)
                                return false;

                            return true;
                        }

                        return false;
                    case "in":
                        if (int.TryParse(value, out int inchValue))
                        {
                            if (inchValue < 59 || inchValue > 76)
                                return false;

                            return true;
                        }

                        return false;
                    default: return false;
                }
            }

            return false;
        }

        private bool ValidateDateRange(string value, int start, int end)
        {
            if (value.Length < 4 || value.Length > 4)
                return false;

            if (int.TryParse(value, out int eyrValue))
                if (eyrValue < start || eyrValue > end)
                    return false;

            return true;
        }
    }
}
