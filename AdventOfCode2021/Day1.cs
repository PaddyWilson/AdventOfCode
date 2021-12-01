using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day1 : BaseDay
	{
		public Day1()
		{
			Day = "1";
			Answer1 = "1616";
			Answer2 = "1645";
		}

		protected override string Solution1(string[] input)
		{
			int[] numbers = new int[input.Length];

			for (int i = 0; i < input.Length; i++)
				numbers[i] = int.Parse(input[i]);

			int count = 0;

			for (int i = 0; i < numbers.Length - 1; i++)
			{
				if (numbers[i] < numbers[i + 1])
					count++;
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int[] numbers = new int[input.Length];

			for (int i = 0; i < input.Length; i++)
				numbers[i] = int.Parse(input[i]);

			int count = 0;

			for (int i = 0; i < numbers.Length - 3; i++)
			{
				int one = numbers[i] + numbers[i + 1] + numbers[i + 2];
				int two = numbers[i+ 1] + numbers[i + 2] + numbers[i + 3];

				if (one < two)
					count++;
			}

			return count.ToString();
		}
	}
}
