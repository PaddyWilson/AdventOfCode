﻿using AdventOfCode2023;
using AOC;

BaseDay.Year = "2023";
BaseDay.InputDir = Path.Combine(Directory.GetCurrentDirectory(), "Input", BaseDay.Year + Path.DirectorySeparatorChar.ToString());

List<BaseDay> days = new List<BaseDay>() {
    new Day1(),
    new Day2(),
    //new Day3(),
    //new Day4(),
    //new Day5(),
    //new Day6(),
    //new Day7(),
    //new Day8(),
    //new Day9(),
    //new Day10(),
    //new Day11(),
    //new Day12(),
	//new Day13(),
	//new Day14(),
	//new Day15(),
	//new Day16(),
	//new Day17(),
	//new Day18(),
	//new Day19(),
	//new Day20(),
	//new Day21(),
	//new Day22(),
	//new Day23(),
	//new Day24(),
	//new Day25(),
};

//tests
{
    days[0].AddTestInput(new string[] {
        "two1nine",
        "eightwothree",
        "abcone2threexyz",
        "xtwone3four",
        "4nineeightseven2",
        "zoneight234",
        "7pqrstsixteen",
         }, "", "281");

    //days[2].AddTestInput(new string[] { "A Y", "B X", "C Z" }, "15", "12");
    //days[3].AddTestInputFromFile("Day3test.txt", "157", "70");
    //days[4].AddTestInputFromFile("Day4test.txt", "2", "4");

    ////test very large community made input files for day 5
    //days[5].AddTestInputFromFile("aoc_2022_day05_message.txt", "CHRISTMAS", "GREETINGS");
    //days[5].AddTestInputFromFile("aoc_2022_day05_large_input.txt", "GATHERING", "DEVSCHUUR");
    ////days[5].AddTestInputFromFile("aoc_2022_day05_large_input-2.txt", "KERSTBOOM", "HENKLEEFT");

    //days[7].AddTestInputFromFile("day7test.txt", "95437", "24933642");
    ////large test file , errors with a stack overflow 
    ////days[7].AddTestInputFromFile("aoc_2022_day07_deep.txt", "0", "0");

    //days[8].AddTestInputFromFile("day8test.txt", "21", "8");
    //days[10].AddTestInputFromFile("day10test.txt", "13140", "");
    //days[11].AddTestInputFromFile("day11test.txt", "10605", "2713310158");
    //days[12].AddTestInputFromFile("day12test.txt", "31", "29");
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