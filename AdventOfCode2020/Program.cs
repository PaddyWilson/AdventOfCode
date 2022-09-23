using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode2020
{
	class Program
	{
		static void Main(string[] args)
		{
			BaseDay.Year = "2020";
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
				new Day22(),
				new Day23(),
				new Day24()
			};

			//tests
			{
				{
					days[7].AddTestInputFromFile("Day7test1.txt", "", "32");
					days[7].AddTestInputFromFile("Day7test2.txt", "", "126");

					days[10].AddTestInput(new string[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, "", "8");
					days[10].AddTestInput(new string[] { "28","33","18","42","31","14","46","20","48","47","24",
															"23","49","45","19","38","39","11","1","32","25","35","8",
															"17","7","9","4","2","34","10","3"}, "", "19208");
					days[10].AddTestInput(new string[] { "10", "6", "4", "7", "1", "5" }, "", "4");
					days[10].AddTestInput(new string[] { "4", "11", "7", "8", "1", "6", "5" }, "", "7");
					days[10].AddTestInput(new string[] { "3", "1", "6", "2" }, "", "4");
					days[10].AddTestInput(new string[] { "17", "6", "10", "5", "13", "7", "1", "4", "12", "11", "14" }, "", "28");

					days[11].AddTestInputFromFile("Day11test.txt", "37", "26");

					days[12].AddTestInput(new string[] { "F10", "N3", "F7", "R90", "F11" }, "25", "286");

					days[13].AddTestInput(new string[] { "939", "7,13,x,x,59,x,31,19" }, "", "1068781");
					days[13].AddTestInput(new string[] { "939", "17,x,13,19" }, "", "3417");
					days[13].AddTestInput(new string[] { "939", "67,7,59,61" }, "", "754018");
					days[13].AddTestInput(new string[] { "939", "67,x,7,59,61" }, "", "779210");
					days[13].AddTestInput(new string[] { "939", "67,7,x,59,61" }, "", "1261476");
					days[13].AddTestInput(new string[] { "939", "1789,37,47,1889" }, "", "1202161486");

					days[15].AddTestInput(new string[] { "0,3,6" }, "436", "");

					days[16].AddTestInputFromFile("Day16test.txt", "71", "");

					days[17].AddTestInput(new string[] { ".#.", "..#", "###" }, "112", "848");

					days[18].AddTestInput(new string[] { "1 + 2 * 3 + 4 * 5 + 6" }, "71", "231");
					days[18].AddTestInput(new string[] { "1 + (2 * 3) + (4 * (5 + 6))" }, "51", "51");
					days[18].AddTestInput(new string[] { "2 * 3 + (4 * 5)" }, "26", "46");
					days[18].AddTestInput(new string[] { "5 + (8 * 3 + 9 + 3 * 4 * 3)" }, "437", "1445");
					days[18].AddTestInput(new string[] { "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))" }, "12240", "669060");
					days[18].AddTestInput(new string[] { "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2" }, "13632", "23340");

					days[19].AddTestInputFromFile("Day19test.txt", "2", "");

					days[20].AddTestInputFromFile("Day20test.txt", "20899048083289", "");

					days[21].AddTestInputFromFile("Day21test.txt", "5", "");

					days[22].AddTestInputFromFile("Day22test1.txt", "306", "291");
					days[22].AddTestInputFromFile("Day22test2.txt", "", "34173");

					days[23].AddTestInput(new string[] { "389125467" }, "67384529", "149245887792");

					days[24].AddTestInputFromFile("Day24test.txt", "10", "2208");
				}
			}

			////run selected
			int day = 24;
			Console.WriteLine("Running ");
			days[day].RunAllSolution1Tests();
			days[day].RunSolution1();
			days[day].RunAllSolution2Tests();
			days[day].RunSolution2();

			////run last
			//Console.WriteLine("Running Last");
			//days[days.Count - 1].RunAllSolution1Tests();
			//days[days.Count - 1].RunSolution1();
			//days[days.Count - 1].RunAllSolution2Tests();
			//days[days.Count - 1].RunSolution2();

			//run all
			Console.WriteLine("Running All");
			foreach (var item in days)
			{
				if (item.ToString() == "AdventOfCode2020.Day13")
				{
					Console.WriteLine("Day 13 takes HOURS to run. Skiping");
					continue;
				}

				item.RunAllSolution1Tests();
				item.RunSolution1();
				item.RunAllSolution2Tests();
				item.RunSolution2();
			}

			Helpers.ConsoleReadKeyWindows();
		}
	}
}
