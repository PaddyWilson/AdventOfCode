using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace AdventOfCode2015
{
	class Program
	{
		static void Main(string[] args)
		{
			BaseDay.Year = "2015";
			BaseDay.InputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input", BaseDay.Year + Path.DirectorySeparatorChar.ToString());

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
				new Day12(),
			};

			//tests
			{
				//day 8
				{
					days[8].AddTestInputFromFile("Day8test.txt", "12", "19");
				}

				days[9].AddTestInput(new string[] {
					"London to Dublin = 464",
					"London to Belfast = 518",
					"Dublin to Belfast = 141"}, "605", "982");
				//days[10].AddTestInputFromFile("day10test.txt", "312211", "");

				days[11].AddTestInput(new string[] { "abcdefgh" }, "abcdffaa", "");
				days[11].AddTestInput(new string[] { "ghijklmn" }, "ghjaabcc", "");
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

			//windows closes the console window, linux does not
			//keep open on windows
			Helpers.ConsoleReadKeyWindows();
		}
	}
}
