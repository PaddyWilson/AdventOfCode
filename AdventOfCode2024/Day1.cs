using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    //the basic layout of for a new Day
    public class Day1 : BaseDay
    {
        public Day1()
        {
            Day = "1";
            Answer1 = "2285373";
            Answer2 = "21142653";
        }

        protected override string Solution1(string[] input)
        {

            List<int> n1 = new List<int>();
            List<int> n2 = new List<int>();

            foreach (var item in input)
            {
                var t = item.Split("   ");
                n1.Add(int.Parse(t[0]));
                n2.Add(int.Parse(t[1]));
            }

            n1.Sort();
            n2.Sort();

            int output = 0;
            for (int i = 0; i < n1.Count; i++)
            {
                int t = n1[i] - n2[i];
                if (t < 0)
                { t *= -1; }
                output += t;
            }
            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<int> n1 = new List<int>();
            List<int> n2 = new List<int>();

            foreach (var item in input)
            {
                var t = item.Split("   ");
                n1.Add(int.Parse(t[0]));
                n2.Add(int.Parse(t[1]));
            }

            int output = 0;

            foreach (var item1 in n1)
            {
                int c = 0;
                foreach (var item2 in n2)
                {
                    if(item2 == item1)
                        c++;
                }
                output+= item1*c;
            }
            return output.ToString();
        }
    }
}
