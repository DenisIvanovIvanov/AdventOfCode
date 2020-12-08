using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day7
{
    public class Bag
    {
        public static string ShinyGoldName = "shiny gold";
        private static readonly Regex ColorMatcherRegex = new Regex(@"^[^\s]+\s+[^\s]+", RegexOptions.Compiled);
        private static readonly Regex BagsMatcherRegex = new Regex(@"(\d+) ([^\s]+\s+[^\s]+)", RegexOptions.Compiled);
        public readonly string Name;
        public readonly IDictionary<string, int> Content;

        public Bag(string line)
        {
            Name = ColorMatcherRegex.Match(line).Value;
            Content = BagsMatcherRegex.Matches(line)
                .ToDictionary(
                    m => m.Groups[2].Value,
                    m => int.Parse(m.Groups[1].Value)
                );
        }

        public static Bag GetBag(string key, List<Bag> allBags) => allBags.First(b => b.Name.Equals(key));

        public bool ContainsBag(string name, List<Bag> allBags)
            => Content.Keys.Any(key => key.Equals(name) || GetBag(key, allBags).ContainsBag(name, allBags));

        public long CalculateBags(List<Bag> allBags)
            => Content.Sum(kv => kv.Value * (1 + GetBag(kv.Key, allBags).CalculateBags(allBags)));
    }

    public class DaySeven : IAdventOfCode
    {
        private readonly string[] _input;

        public string Name => "--- Day 7: Handy Haversacks ---";

        public DaySeven()
        {
            _input = File.ReadAllLines(@".\Day7\input.txt");
        }

        public void PartOne()
        {
            var bags = _input.Select(l => new Bag(l)).ToList();
            var count = bags.Count(b => b.ContainsBag(Bag.ShinyGoldName, bags));
            Console.WriteLine(count);
        }

        public void PartTwo()
        {
            var bags = _input.Select(l => new Bag(l)).ToList();
            var count = Bag.GetBag(Bag.ShinyGoldName, bags).CalculateBags(bags);
            Console.WriteLine(count);
        }
    }
}
