using System;
using System.Collections.Generic;
using AOC;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseDay.InputDir = @"C:\Users\GGGGG\Desktop\Code\AdventOfCode\Input\2021\";
            BaseDay.Year = "2021";

            List<BaseDay> days = new List<BaseDay>() {
                new Day0(),
                new Day1(),
                new Day2()
            };

            //tests
            {
                //day 6
                {
                    //days[6].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day6-1 test.txt"), "42", "");
                    //days[6].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day6-2 test.txt"), "", "4");
                }
            }

            ////run selected
            //int day = 0;
            //Console.WriteLine("Running");
            //days[day].RunAllSolution1Tests();
            //days[day].RunSolution1();
            //days[day].RunAllSolution2Tests();
            //days[day].RunSolution2();

            ////run last
            Console.WriteLine("Running Last");
            days[days.Count - 1].RunAllSolution1Tests();
            days[days.Count - 1].RunSolution1();
            days[days.Count - 1].RunAllSolution2Tests();
            days[days.Count - 1].RunSolution2();

            ////run all
            //Console.WriteLine("Running All");
            //foreach (var item in days)
            //{
            //    item.RunSolution1();
            //    item.RunAllSolution1Tests();
            //    item.RunSolution2();
            //    item.RunAllSolution2Tests();
            //}

            Console.ReadKey();
        }
    }
}
