using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
				new Day13(),
				new Day14(),
				new Day15(),
				new Day16(),
				new Day17(),
				new Day18(),
				new Day19(),
				new Day20(),
				new Day21(),
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
				days[10].AddTestInputFromFile("Day10test.txt", "312211", "");

				days[11].AddTestInput(new string[] { "abcdefgh" }, "abcdffaa", "");
				days[11].AddTestInput(new string[] { "ghijklmn" }, "ghjaabcc", "");

				days[13].AddTestInputFromFile("Day13test.txt", "330", "");
				days[14].AddTestInputFromFile("Day14test.txt", "1120", "689");
				days[15].AddTestInputFromFile("Day15test.txt", "62842880", "57600000");

				days[17].AddTestInput(new string[] { "20", "15", "10", "5", "5", }, "4", "");

				days[18].AddTestInputFromFile("Day18test.txt", "4", "17");

				days[19].AddTestInput(new string[] {
					"H => HO",
					"H => OH",
					"O => HH",
					"",
					"HOH",
				}, "4", "");

				days[19].AddTestInput(new string[] {
					"H => HO",
					"H => OH",
					"O => HH",
					"",
					"HOHOHO",
				}, "7", "");

				days[19].AddTestInput(new string[] {
					"e => H",
					"e => O",
					"H => HO",
					"H => OH",
					"O => HH",
					"",
					"HOH",
				}, "", "3");

				days[19].AddTestInput(new string[] {
					"e => H",
					"e => O",
					"H => HO",
					"H => OH",
					"O => HH",
					"",
					"HOHOHO",
				}, "", "6");
			}

			////run
			int day = days.Count - 1;
			Console.WriteLine("Running");
			days[day].RunAllSolution1Tests();
			days[day].RunSolution1();
			days[day].RunAllSolution2Tests();
			days[day].RunSolution2();

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
