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
				new Day5()
			};

			////run last
			//Console.WriteLine("Running Last");
			//days[days.Count - 1].RunSolution1();
			//days[days.Count - 1].RunSolution2(); 


			//run all
			Console.WriteLine("Running All");
			foreach (var item in days)
			{
				item.RunSolution1();
				item.RunSolution2();
			}

			Console.ReadKey();
		}
	}
}
