using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode2020
{
	class Program
	{
		static void Main(string[] args)
		{
			BaseDay.InputDir = @"C:\Users\GGGGG\Desktop\AdventOfCode2020\Input\";

			List<BaseDay> days = new List<BaseDay>() {
				//new Day0(),
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
				new Day22()
			};
			//day 7 test
			{
				string[] day7Test1 = new string[]{
				"light red bags contain 1 bright white bag, 2 muted yellow bags.",
				"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
				"bright white bags contain 1 shiny gold bag.",
				"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
				"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
				"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
				"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
				"faded blue bags contain no other bags.",
				"dotted black bags contain no other bags.",
			};

				string[] Day7Test2 = new string[]{
				"shiny gold bags contain 2 dark red bags.",
				"dark red bags contain 2 dark orange bags.",
				"dark orange bags contain 2 dark yellow bags.",
				"dark yellow bags contain 2 dark green bags.",
				"dark green bags contain 2 dark blue bags.",
				"dark blue bags contain 2 dark violet bags.",
				"dark violet bags contain no other bags."
			};

				days[6].AddTestInput(day7Test1, "", "32");
				days[6].AddTestInput(Day7Test2, "", "126");
			}

			//day 10 tests
			{
				string[] Day10Test1 = new string[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" };
				string[] Day10Test2 = new string[]{"28","33","18","42","31","14","46","20","48","47","24",
				"23","49","45","19","38","39","11","1","32","25","35","8",
				"17","7","9","4","2","34","10","3" };

				string[] Day10Test3 = new string[] { "10", "6", "4", "7", "1", "5" };
				string[] Day10Test4 = new string[] { "4", "11", "7", "8", "1", "6", "5" };
				string[] Day10Test5 = new string[] { "3", "1", "6", "2" };
				string[] Day10Test6 = new string[] { "17", "6", "10", "5", "13", "7", "1", "4", "12", "11", "14" };

				days[9].AddTestInput(Day10Test5, "", "4");
				days[9].AddTestInput(Day10Test1, "", "8");
				days[9].AddTestInput(Day10Test2, "", "19208");
				days[9].AddTestInput(Day10Test3, "", "4");
				days[9].AddTestInput(Day10Test4, "", "7");
				days[9].AddTestInput(Day10Test6, "", "28");
			}

			//day 11 tests
			{
				string[] Day11Test1 = new string[] {
				"L.LL.LL.LL",
				"LLLLLLL.LL",
				"L.L.L..L..",
				"LLLL.LL.LL",
				"L.LL.LL.LL",
				"L.LLLLL.LL",
				"..L.L.....",
				"LLLLLLLLLL",
				"L.LLLLLL.L",
				"L.LLLLL.LL"
				};

				days[10].AddTestInput(Day11Test1, "37", "26");
			}

			//day 12 test
			{
				string[] Day12Test1 = new string[] {
					"F10",
					"N3",
					"F7",
					"R90",
					"F11"
				};

				days[11].AddTestInput(Day12Test1, "25", "286");
			}

			//day 13 tests
			{
				string[] Test1 = new string[] {
					"939",
					"7,13,x,x,59,x,31,19"
				};

				string[] Test2 = new string[] {
					"939",
					"17,x,13,19"
				};
				string[] Test3 = new string[] {
					"939",
					"67,7,59,61"
				};
				string[] Test4 = new string[] {
					"939",
					"67,x,7,59,61"
				};
				string[] Test5 = new string[] {
					"939",
					"67,7,x,59,61"
				};
				string[] Test6 = new string[] {
					"939",
					"1789,37,47,1889"
				};

				days[12].AddTestInput(Test2, "", "3417");
				days[12].AddTestInput(Test1, "", "1068781");
				days[12].AddTestInput(Test3, "", "754018");
				days[12].AddTestInput(Test4, "", "779210");
				days[12].AddTestInput(Test5, "", "1261476");
				days[12].AddTestInput(Test6, "", "1202161486");
			}

			//day 15 tests
			{
				string[] Test1 = new string[] {
					"0,3,6"
				};

				days[14].AddTestInput(Test1, "436", "");
			}

			//day 16 tests
			{
				string[] Test1 = new string[] {
					"class: 1-3 or 5-7",
					"row: 6-11 or 33-44",
					"seat: 13-40 or 45-50",
					"",
					"your ticket:",
					"7,1,14",
					"",
					"nearby tickets:",
					"7,3,47",
					"40,4,50",
					"55,2,20",
					"38,6,12"
				};

				days[15].AddTestInput(Test1, "71", "");
			}

			//day 17 tests
			{
				string[] Test1 = new string[] {
					".#.",
					"..#",
					"###"
				};

				days[16].AddTestInput(Test1, "112", "848");
			}

			//day 18 tests
			{
				string[] Test1 = new string[] { "1 + 2 * 3 + 4 * 5 + 6" };
				string[] Test2 = new string[] { "1 + (2 * 3) + (4 * (5 + 6))" };
				string[] Test3 = new string[] { "2 * 3 + (4 * 5)" };
				string[] Test4 = new string[] { "5 + (8 * 3 + 9 + 3 * 4 * 3)" };
				string[] Test5 = new string[] { "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))" };
				string[] Test6 = new string[] { "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2" };

				days[17].AddTestInput(Test1, "71", "231");
				days[17].AddTestInput(Test2, "51", "51");
				days[17].AddTestInput(Test3, "26", "46");
				days[17].AddTestInput(Test4, "437", "1445");
				days[17].AddTestInput(Test5, "12240", "669060");
				days[17].AddTestInput(Test6, "13632", "23340");
			}

			//day 19 tests
			{
				string[] Test1 = new string[] {
					"0: 4 1 5",
					"1: 2 3 | 3 2",
					"2: 4 4 | 5 5",
					"3: 4 5 | 5 4",
					"4: \"a\"",
					"5: \"b\"",
					"",
					"ababbb",
					"bababa",
					"abbbab",
					"aaabbb",
					"aaaabbb"
					};

				days[18].AddTestInput(Test1, "2", "");
			}

			//day 21 tests
			{
				string[] Test1 = new string[] {
					"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)",
					"trh fvjkl sbzzf mxmxvkd (contains dairy)",
					"sqjhc fvjkl (contains soy)",
					"sqjhc mxmxvkd sbzzf (contains fish)",
					};

				days[20].AddTestInput(Test1, "5", "");
			}

			//day 22 tests
			{
				string[] Test1 = new string[] {
					"Player 1:",
					"9",
					"2",
					"6",
					"3",
					"1",
					"",
					"Player 2:",
					"5",
					"8",
					"4",
					"7",
					"10"
					};

				string[] Test2 = new string[] {
				"Player 1:",
				"38",
				"1",
				"28",
				"32",
				"43",
				"21",
				"42",
				"29",
				"18",
				"13",
				"39",
				"41",
				"49",
				"31",
				"19",
				"26",
				"27",
				"40",
				"35",
				"14",
				"3",
				"36",
				"12",
				"16",
				"45",
				"",
				"Player 2:",
				"34",
				"15",
				"47",
				"20",
				"23",
				"2",
				"11",
				"9",
				"8",
				"7",
				"25",
				"50",
				"48",
				"24",
				"46",
				"44",
				"10",
				"6",
				"22",
				"5",
				"33",
				"30",
				"4",
				"17",
				"37"
				};
				days[21].AddTestInput(Test1, "306", "291");
				days[21].AddTestInput(Test2, "", "34173");
			}

			////run last
			Console.WriteLine("Running Last");
			days[days.Count - 1].RunAllSolution1Tests();
			days[days.Count - 1].RunSolution1();
			days[days.Count - 1].RunAllSolution2Tests();
			days[days.Count - 1].RunSolution2();

			//run all
			//Console.WriteLine("Running All");
			//foreach (var item in days)
			//{
			//	item.RunSolution1();
			//	item.RunSolution2();
			//}

			Console.ReadKey();
		}
	}
}
