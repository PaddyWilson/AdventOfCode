using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day2 : BaseDay
	{
		public Day2()
		{
			Day = "2";
			Answer1 = "1936494";
			Answer2 = "1997106066";
		}

		protected override string Solution1(string[] input)
		{
			int hor = 0;
			int depth = 0;

			foreach (var item in input)
			{
				string[] temp = item.Split(" ");

				if (temp[0][0] == 'f')
					hor += int.Parse(temp[1]);
				else if (temp[0][0] == 'd')
					depth += int.Parse(temp[1]);
				else if (temp[0][0] == 'u')
					depth -= int.Parse(temp[1]);
			}

			return (hor * depth).ToString();
		}

		protected override string Solution2(string[] input)
		{
			int hor = 0;
			int depth = 0;
			int aim = 0;

			foreach (var item in input)
			{
				string[] temp = item.Split(" ");

				if (temp[0][0] == 'f')
				{
					hor += int.Parse(temp[1]);
					depth += aim* int.Parse(temp[1]);
				}
				else if (temp[0][0] == 'd')
					aim += int.Parse(temp[1]);
				else if (temp[0][0] == 'u')
					aim -= int.Parse(temp[1]);
			}

			return (hor * depth).ToString();
		}
	}
}
