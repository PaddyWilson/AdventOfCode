using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    #pragma warning disable CS8602 // Dereference of a possibly null reference.
    //the basic layout of for a new Day
    public class Day3 : BaseDay
    {
        public Day3()
        {
            Day = "3";
            Answer1 = "173785482";
            Answer2 = "83158140";
        }

        protected override string Solution1(string[] input)
        {
            string pattern = @"mul\([0-9]{1,3},[0-9]{1,3}\)";
            int output = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var rMatches = Regex.Matches(input[i], pattern, RegexOptions.IgnoreCase);
                int t = rMatches.Count;
                foreach (var item in rMatches)
                {
                    var numbers = item.ToString().Replace("mul(", "").Replace(")", "").Split(",");
                    output += int.Parse(numbers[0]) * int.Parse(numbers[1]);
                }
            }
            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            string pattern = @"do\(\)|don't\(\)|mul\([0-9]{1,3},[0-9]{1,3}\)";
            int output = 0;

            bool compute = true;

            for (int i = 0; i < input.Length; i++)
            {
                var rMatches = Regex.Matches(input[i], pattern);//, RegexOptions.Multiline);

                foreach (var item in rMatches)
                {
                    if (item.ToString().Contains("do()"))
                    {
                        compute = true;
                        continue;
                    }

                    if (item.ToString().Contains("don't()"))
                    {
                        compute = false;
                        continue;
                    }

                    if (compute)
                    {
                        var numbers = item.ToString().Replace("mul(", "").Replace(")", "").Split(",");
                        output += int.Parse(numbers[0]) * int.Parse(numbers[1]);
                    }
                }
            }
            return output.ToString();
        }
    }
    #pragma warning restore CS8602 // Dereference of a possibly null reference.
}
