using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day6 : BaseDay
    {
        public Day6()
        {
            Day = "6";
            Answer1 = "4568778";
            Answer2 = "28973936";
        }

        protected override string Solution1(string[] input)
        {
            List<List<long>> list = new();
            foreach (string s in input)
            {
                list.Add(new List<long>());
                s.Split(":")[1].Trim().Split(" ").ToList().ForEach((string item) => { if (item != "") list[^1].Add(long.Parse(item.Trim())); });
            }

            long output = 1;
            for (int i = 0; i < list[0].Count; i++)
                output *= GetPossibleWinCount(list[0][i], list[1][i]);
            return output.ToString();
        }

        protected override string Solution2(string[] input)
        {
            List<long> list = new();
            foreach (string s in input)
            {
                string temp = "";
                s.Split(":")[1].Trim().Split(" ").ToList().ForEach((string item) => { if (item != "") temp += item.Trim(); });
                list.Add(long.Parse(temp));
            }

            return GetPossibleWinCount(list[0], list[1]).ToString();
        }

        long GetPossibleWinCount(long time, long dist)
        {
            long count = 0;
            for (long t = 0; t < time; t++)
            {
                long speed = t;
                long remainTime = time -t;
                long result = speed * remainTime;
                if (result > dist)
                    count++;
            }
            return count;
        }
}
}
