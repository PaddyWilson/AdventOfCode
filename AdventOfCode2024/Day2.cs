using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    //the basic layout of for a new Day
    public class Day2 : BaseDay
    {
        public Day2()
        {
            Day = "2";
            Answer1 = "680";
            Answer2 = "710";
        }

        protected override string Solution1(string[] input)
        {
            int safeCount = 0;
            foreach (var item in input)
            {
                List<int> reports = item.SplitToInt();

                List<int> diffs = new List<int>();
                for (int i = 0; i < reports.Count - 1; i++)
                    diffs.Add(reports[i] - reports[i + 1]);

                bool maybe = true;
                //convert to positive
                if (diffs[0] < 0)
                    for (int i = 0; i < diffs.Count; i++)
                    {
                        diffs[i] = diffs[i] * -1;
                        if (diffs[i] < 0)
                        {
                            maybe = false;
                            break;
                        }
                    }
                if (!maybe)//diffs still has negitives
                    continue;

                for (int i = 0; i < diffs.Count; i++)
                {
                    if (diffs[i] < 1 || diffs[i] > 3)
                    {
                        maybe = false;
                        break;
                    }
                }
                if (!maybe)
                    continue;
                safeCount++;
            }
            return safeCount.ToString();
        }

        protected override string Solution2(string[] input)
        {
            int safeCount = 0;
            foreach (var item in input)
            {
                List<int> reports = item.SplitToInt();

                for (int lo = 0; lo <= reports.Count; lo++)
                {
                    List<int> report = new List<int>(reports);
                    if (lo > 0)
                        report.RemoveAt(lo - 1);//can remove one element if that means that it passes

                    List<int> diffs = new List<int>();
                    for (int i = 0; i < report.Count - 1; i++)
                        diffs.Add(report[i] - report[i + 1]);

                    bool maybe = true;
                    //convert to positive
                    if (diffs[0] < 0)
                        for (int i = 0; i < diffs.Count; i++)
                        {
                            diffs[i] = diffs[i] * -1;
                            if (diffs[i] < 0)
                            {
                                maybe = false;
                                break;
                            }
                        }
                    if (!maybe)//diffs still has negitives
                        continue;

                    for (int i = 0; i < diffs.Count; i++)
                    {
                        if (diffs[i] < 1 || diffs[i] > 3)
                        {
                            maybe = false;
                            break;
                        }
                    }
                    if (maybe)
                    {
                        safeCount++;
                        break;
                    }
                }
            }
            return safeCount.ToString();
        }
    }
}
