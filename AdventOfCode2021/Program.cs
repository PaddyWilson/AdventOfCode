using System;
using System.Collections.Generic;
using System.IO;
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
				new Day2(),
				new Day3(),
				new Day4(),
				new Day5(),
				new Day6(),
				new Day7(),
				new Day8(),
				new Day9(),
				new Day10(),
				new Day11(),
				new Day12()
			};

			//tests
			{
				{
					days[3].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day3test.txt"), "198", "230");
					days[4].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day4test.txt"), "4512", "1924");
					days[5].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day5test.txt"), "5", "12");
					days[6].AddTestInput(new string[] { "3,4,3,1,2" }, "5934", "26984457539");
					days[7].AddTestInput(new string[] { "16,1,2,0,4,2,7,1,2,14" }, "37", "168");
					days[8].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day8test.txt"), "26", "61229");
					days[9].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day9test.txt"), "15", "1134");
					days[10].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day10test.txt"), "26397", "288957");
					days[11].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day11test.txt"), "1656", "195");
					days[12].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day12testSmall.txt"), "10", "36");
					days[12].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day12testBig.txt"), "19", "103");
					days[12].AddTestInput(File.ReadAllLines(BaseDay.InputDir + "day12testBigger.txt"), "226", "3509");
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
			Console.WriteLine("Running All");
			foreach (var item in days)
			{
				item.RunAllSolution1Tests();
				item.RunSolution1();
				item.RunAllSolution2Tests();
				item.RunSolution2();
			}

			Console.ReadKey();
		}
	}
}
