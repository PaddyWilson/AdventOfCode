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
            BaseDay.Year = "2019";
			BaseDay.InputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input", BaseDay.Year + Path.DirectorySeparatorChar.ToString());

			List<BaseDay> days = new List<BaseDay>() {
                new Day0(),
                new Day1(),
                new Day2(),
                new Day3(),//it takes a long time ~about 4 mins and likes ram
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
                //day 3
                {
                    days[3].AddTestInput(
                        new string[] {
                            "R8,U5,L5,D3",
                            "U7,R6,D4,L4"
                        }, "6", "30");
                    days[3].AddTestInput(
                        new string[] {
                            "R75,D30,R83,U83,L12,D49,R71,U7,L72",
                            "U62,R66,U55,R34,D71,R55,D58,R83"
                        }, "159", "610");
                    days[3].AddTestInput(
                        new string[] {
                            "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
                            "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"
                        }, "135", "410");
                }

                //day 6
                {
                    days[6].AddTestInputFromFile("day6-1 test.txt", "42", "");
                    days[6].AddTestInputFromFile("day6-2 test.txt", "", "4");
                }
            }

            //run selected
            int day = 1;
            Console.WriteLine("Running");
            days[day].RunAllSolution1Tests();
            days[day].RunSolution1();
            days[day].RunAllSolution2Tests();
            days[day].RunSolution2();

            //run last
            //Console.WriteLine("Running Last");
            //days[days.Count - 1].RunAllSolution1Tests();
            //days[days.Count - 1].RunSolution1();
            //days[days.Count - 1].RunAllSolution2Tests();
            //days[days.Count - 1].RunSolution2();

            //run all
            Console.WriteLine("Running All");
            foreach (var item in days)
            {
                item.RunSolution1();
                item.RunAllSolution1Tests();
                item.RunSolution2();
                item.RunAllSolution2Tests();
            }

			Helpers.ConsoleReadKeyWindows();
		}
    }
}
