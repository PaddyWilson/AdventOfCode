using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "400410";
			Answer2 = "15343601";
		}

		protected override string Solution1(string[] input)
		{
			bool[,] lights = new bool[1000, 1000];

			for (int x = 0; x < 1000; x++)
				for (int y = 0; y < 1000; y++)
					lights[x, y] = false;

			foreach (var item in input)
			{
				string[] instructions = item.Replace("turn ", "").Split(' ');

				int x1 = int.Parse(instructions[1].Split(',')[0]);
				int y1 = int.Parse(instructions[1].Split(',')[1]);

				int x2 = int.Parse(instructions[3].Split(',')[0]);
				int y2 = int.Parse(instructions[3].Split(',')[1]);

				for (int y = y1; y <= y2; y++)
				{
					for (int x = x1; x <= x2; x++)
					{
						switch (instructions[0])
						{
							case "on":
								lights[x, y] = true;
								break;
							case "off":
								lights[x, y] = false;
								break;
							case "toggle":
								lights[x, y] = !lights[x, y];
								break;
							default:
								break;
						}
					}
				}
			}//end foreach

			int count = 0;
			for (int y = 0; y < 1000; y++)
				for (int x = 0; x < 1000; x++)
					if (lights[x, y])
						count++;

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int[,] lights = new int[1000, 1000];

			for (int x = 0; x < 1000; x++)
				for (int y = 0; y < 1000; y++)
					lights[x, y] = 0;

			foreach (var item in input)
			{
				string[] instructions = item.Replace("turn ", "").Split(' ');

				int x1 = int.Parse(instructions[1].Split(',')[0]);
				int y1 = int.Parse(instructions[1].Split(',')[1]);

				int x2 = int.Parse(instructions[3].Split(',')[0]);
				int y2 = int.Parse(instructions[3].Split(',')[1]);

				for (int y = y1; y <= y2; y++)
				{
					for (int x = x1; x <= x2; x++)
					{
						switch (instructions[0])
						{
							case "on":
								lights[x, y] += 1;
								break;
							case "off":
								lights[x, y] -= 1;
								if (lights[x, y] < 0)
									lights[x, y] = 0;
								break;
							case "toggle":
								lights[x, y] += 2;
								break;
							default:
								break;
						}
					}
				}
			}//end foreach

			int count = 0;
			for (int y = 0; y < 1000; y++)
				for (int x = 0; x < 1000; x++)
					count += lights[x, y];

			return count.ToString();
		}
	}
}
