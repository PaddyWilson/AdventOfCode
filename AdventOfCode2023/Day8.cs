using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    //the basic layout of for a new Day
    public class Day8 : BaseDay
    {
        public Day8()
        {
            Day = "8";
            Answer1 = "22357";
            Answer2 = "10371555451871";
        }

        protected override string Solution1(string[] input)
        {
            char[] direction = input[0].ToCharArray();
            Dictionary<string, (string, string)> map = new();
            for (int i = 2; i < input.Length; i++)
            {
                var temp = input[i].Replace("= ", "").Replace("(", "").Replace(",", "").Replace(")", "").Split(' ');
                map.Add(temp[0], (temp[1], temp[2]));
            }

            string current = "AAA";
            int stepCount = 0;

            int directionIndex = 0;
            while (current != "ZZZ")
            {
                stepCount++;
                if (direction[directionIndex] == 'R')
                    current = map[current].Item2;
                else
                    current = map[current].Item1;

                directionIndex++;
                if (directionIndex >= direction.Length)
                    directionIndex = 0;
            }

            return stepCount.ToString();
        }

        protected override string Solution2(string[] input)
        {
            char[] direction = input[0].ToCharArray();
            Dictionary<string, (string, string)> map = new();
            for (int i = 2; i < input.Length; i++)
            {
                var temp = input[i].Replace("= ", "").Replace("(", "").Replace(",", "").Replace(")", "").Split(' ');
                map.Add(temp[0], (temp[1], temp[2]));
            }

            //about 30 sec
            return Part2Optimized(direction, map).ToString();
            //about 403 hours by my calculation
            return Part2Bruteforce(direction, map).ToString();
        }

        ulong Part2Optimized(char[] direction, Dictionary<string, (string, string)> map)
        {
            List<(ulong, ulong)> distances = new();
            foreach (var item in map)
            {
                if (item.Key[2] != 'A')
                    continue;

                string current = item.Key;
                ulong stepCount = 0;

                int directionIndex = 0;
                while (current[2] != 'Z')
                {
                    stepCount++;
                    if (direction[directionIndex] == 'R')
                        current = map[current].Item2;
                    else
                        current = map[current].Item1;

                    directionIndex++;
                    if (directionIndex >= direction.Length)
                        directionIndex = 0;
                }

                distances.Add((stepCount, stepCount));
            }

            while (true)
            {
                ulong highest = distances[0].Item2;
                int matching = 1;
                ulong dis = distances[0].Item2;
                for (int i = 1; i < distances.Count; i++)
                {
                    if (distances[i].Item2 == dis)
                        matching++;

                    if (highest < distances[i].Item2)
                        highest = distances[i].Item2;

                }
                if (matching == distances.Count())
                    return dis;

                for (int i = 0; i < distances.Count; i++)
                {
                    while (distances[i].Item2 < highest)
                        distances[i] = (distances[i].Item1, distances[i].Item2 + distances[i].Item1);
                }
            }
        }

        ulong Part2Bruteforce(char[] direction, Dictionary<string, (string, string)> map)
        {
            List<string> current = new();
            foreach (var item in map.Keys)
            {
                if (item[2] == 'A')
                    current.Add(item);
            }

            ulong stepCount = 0;
            int directionIndex = 0;

            while (true)
            {
                stepCount++;

                int atEnd = 0;
                for (int i = 0; i < current.Count; i++)
                {
                    if (direction[directionIndex] == 'R')
                        current[i] = map[current[i]].Item2;
                    else
                        current[i] = map[current[i]].Item1;

                    if (current[i][2] == 'Z')
                        atEnd++;
                }

                //if (stepCount % 10000000 == 0)
                //    Console.WriteLine("Need=" + current.Count + " Have=" + atEnd + " Step=" + stepCount);

                if (atEnd == current.Count)
                    return stepCount;

                directionIndex++;
                if (directionIndex >= direction.Length)
                    directionIndex = 0;
            }
        }
    }
}
