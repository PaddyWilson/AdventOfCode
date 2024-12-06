using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    //the basic layout of for a new Day
    public class Day5 : BaseDay
    {
        public Day5()
        {
            Day = "5";
            Answer1 = "6505";
            Answer2 = "6897";
        }

        protected override string Solution1(string[] input)
        {
            List<List<int>> manuals;
            Dictionary<int, List<int>> rules;
            Parse(input, out manuals, out rules);

            int output = 0;
            foreach (var manual in manuals)
            {
                if (IsValidManual(rules, manual))
                    output += manual[(int)(manual.Count / 2)];
            }

            return output.ToString();
        }

        private static bool IsValidManual(Dictionary<int, List<int>> rules, List<int> manual)
        {
            int validCount = 0;
            for (int pageN = 0; pageN < manual.Count; pageN++)
            {
                bool validPage = true;
                List<int>? r;
                rules.TryGetValue(manual[pageN], out r);
                if (r == null)
                {
                    validCount++;
                    continue;
                }

                //check before
                for (int i = 0; i < pageN; i++)
                {
                    if (r.Contains(manual[i]))
                        validPage = false;
                }

                //check after
                //no need to check after
                if (validPage)
                    validCount++;
            }
            return validCount == manual.Count;
        }

        protected override string Solution2(string[] input)
        {
            List<List<int>> manuals;
            Dictionary<int, List<int>> rules;
            Parse(input, out manuals, out rules);

            int output = 0;
            foreach (var manual in manuals)
            {
                //dont include valid manuals
                if (IsValidManual(rules, manual))
                    continue;

                bool movedItem = true;
                while (movedItem)
                {
                    for (int pageN = manual.Count - 1; pageN >= 0; pageN--)
                    {
                        List<int>? r;
                        rules.TryGetValue(manual[pageN], out r);
                        if (r == null)
                        {
                            continue;
                        }
                        //check before
                        for (int i = 0; i < pageN; i++)
                        {
                            if (r.Contains(manual[i]))
                            {
                                int temp = manual[pageN];
                                manual.Remove(temp);
                                manual.Insert(i, temp);
                            }
                        }

                        //check after
                        if (IsValidManual(rules, manual))
                        {
                            output += manual[(int)(manual.Count / 2)];
                            movedItem = false;
                            break;
                        }
                    }
                }
            }

            return output.ToString();
        }

        private static void Parse(string[] input, out List<List<int>> updates, out Dictionary<int, List<int>> rules)
        {
            int inputIndex = 0;

            updates = new();
            rules = new();
            while (true)
            {
                if (input[inputIndex].Length == 0)
                {
                    inputIndex++;
                    break;
                }

                var pages = input[inputIndex].ExtractInts();
                if (!rules.ContainsKey(pages[0]))
                    rules.Add(pages[0], new());
                rules[pages[0]].Add(pages[1]);
                inputIndex++;
            }

            while (true)
            {
                if (inputIndex == input.Length)
                    break;

                var pages = input[inputIndex].ExtractInts();
                updates.Add(pages);
                inputIndex++;
            }
        }
    }
}
