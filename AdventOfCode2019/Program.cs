using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseDay.InputDir = @"C:\Users\GGGGG\Desktop\Code\AdventOfCode\Input\2019\";
            BaseDay.Year = "2019";

            List<BaseDay> days = new List<BaseDay>() {
                new Day0(),
                new Day1(),
                new Day2(),
                new Day3(),//it takes a long time ~about 4 mins
                new Day4(),
                new Day5(),
                new Day6(),
                new Day7(),
                new Day8(),
                new Day9(),
                new Day10()
            };

            //tests
            {
                //day 6
                {
                    days[6].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day6-1 test.txt"), "42", "");
                    days[6].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day6-2 test.txt"), "", "4");
                }
            }

            ////run selected
            int day = 6;
            Console.WriteLine("Running");
            days[day].RunAllSolution1Tests();
            days[day].RunSolution1();
            days[day].RunAllSolution2Tests();
            days[day].RunSolution2();

            ////run last
            Console.WriteLine("Running Last");
            days[days.Count - 1].RunAllSolution1Tests();
            days[days.Count - 1].RunSolution1();
            days[days.Count - 1].RunAllSolution2Tests();
            days[days.Count - 1].RunSolution2();

            ////run all
            Console.WriteLine("Running All");
            foreach (var item in days)
            {
                item.RunSolution1();
                item.RunAllSolution1Tests();
                item.RunSolution2();
                item.RunAllSolution2Tests();
            }

            Console.ReadKey();
        }
    }
}
