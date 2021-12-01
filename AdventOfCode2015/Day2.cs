using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day2 : BaseDay
	{
		public Day2()
		{
			Day = "2";
			Answer1 = "1588178";
			Answer2 = "3783758";
		}

		protected override string Solution1(string[] input)
		{
			int count = 0;

			List<int> area = new List<int>();
			area.Add(0);
			area.Add(0);
			area.Add(0);

			foreach (var item in input)
			{
				string[] numbers = item.Split('x');

				int l = int.Parse(numbers[0]);
				int w = int.Parse(numbers[1]);
				int h = int.Parse(numbers[2]);

				area[0] = (l * w);
				area[1] = (w * h);
				area[2] = (h * l);

				count += (2 * l * w) + (2 * w * h) + (2 * h * l);

				area.Sort();
				count += area[0];
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int count = 0;

			List<int> area = new List<int>();
			area.Add(0);
			area.Add(0);
			area.Add(0);

			foreach (var item in input)
			{
				string[] numbers = item.Split('x');

				int l = int.Parse(numbers[0]);
				int w = int.Parse(numbers[1]);
				int h = int.Parse(numbers[2]);

				area[0] = (l);
				area[1] = (w);
				area[2] = (h);

				area.Sort();

				count += area[0] + area[0] + area[1] + area[1];

				//ribbon length
				count += l * w * h;
			}

			return count.ToString();
		}
	}
}
