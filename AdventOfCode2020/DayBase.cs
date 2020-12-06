using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public abstract class DayBase
    {
        protected virtual List<List<string>> ParseInput(string path)
        {
            var result = new List<List<string>>()
            {
                new List<string>()
            };

            var currentIndex = 0;

            foreach (var line in File.ReadAllLines(path))
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
