using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day1 : BaseDay
    {
        public Day1()
        {
            Day = "1";
            Answer1 = "53386";
            Answer2 = "53312";
        }

        protected override string Solution1(string[] input)
        {
            int total = 0;

            foreach (string s in input)
            {
                int first = 0;
                int last = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    if (first == 0)
                    {
                        for (int j = '1'; j < '9' + 1; j++)
                        {
                            if (s[i] == j)
                            {
                                first = j - 48;
                            }
                        }
                    }

                    if (last == 0)
                    {
                        for (int j = '1'; j < '9' + 1; j++)
                        {
                            if (s[s.Length -1 - i] == j)
                            {
                                last = j - 48;
                            }
                        }
                    }
                }

                total += int.Parse(first.ToString() + last.ToString());
            }

            return total.ToString();
        }

        protected override string Solution2(string[] input)
        {
            int total = 0;

            foreach (string s in input)
            {
                int first = 0;
                int last = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    if (first == 0)
                    {
                        first = FindNumber(s, i);

                        //for (int j = '1'; j < '9' + 1; j++)
                        //{
                        //    if (s[i] == j)
                        //    {
                        //        first = j - 48;
                        //    }
                        //    else 
                        //    {

                        //    }
                        //}
                    }

                    if (last == 0)
                    {
                        last = FindNumber(s, s.Length - 1 - i);

                        //for (int j = '1'; j < '9' + 1; j++)
                        //{
                        //    if (s[s.Length - 1 - i] == j)
                        //    {
                        //        last = j - 48;
                        //    }
                        //}
                    }
                }

                total += int.Parse(first.ToString() + last.ToString());
            }

            return total.ToString();
        }

        private int FindNumber(string text, int index)
        {
            List<string> numbers = new()
            {
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            };

            int count = 0;
            foreach (string num in numbers)
            {
                count++;
                int matchCount = 0;

                for (int i = 0; i < num.Length; i++)
                {
                    if (index + i >= text.Length)
                        break;

                    if (text[index + i] == num[i])
                        matchCount++;
                }

                if(matchCount == num.Length)
                {
                    return count;
                }
            }

            for (int j = '1'; j < '9' + 1; j++)
            {
                if (text[index] == j)
                {
                    return j - 48;
                }
            }

            return 0;
        }
    }
}
