using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day9 : BaseDay
	{
		public Day9()
		{
			Day = "9";
			Answer1 = "20874512";
			Answer2 = "3012420";
		}

		protected override string Solution1(string[] input)
		{
			ulong[] numbers = new ulong[input.Length];
			for (int i = 0; i < input.Length; i++)
				numbers[i] = ulong.Parse(input[i]);


			bool running = true;
			int index = 25;
			while (running)
			{
				ulong current = numbers[index];

				bool works = false;
				for (int low = index - 25; low < index; low++)
				{
					for (int high = index - 25; high < index; high++)
					{
						if (low == high)
							continue;
						if (numbers[low] + numbers[high] == current)
							works = true;
					}
				}

				if (!works)
					return current.ToString();

				index++;
			}


			return "-1";
		}

		protected override string Solution2(string[] input)
		{
			ulong[] numbers = new ulong[input.Length];
			for (int i = 0; i < input.Length; i++)
				numbers[i] = ulong.Parse(input[i]);

			ulong numberToFind = 20874512;

			for (int low = 0; low < numbers.Length; low++)
			{
				ulong acc = 0;
				ulong smallest = ulong.MaxValue - 1;
				ulong highest = 0;
				for (int high = low; high < numbers.Length; high++)
				{
					acc += numbers[high];

					if (smallest > numbers[high])
						smallest = numbers[high];
					if (highest < numbers[high])
						highest = numbers[high];

					if (acc == numberToFind)
						return (smallest + highest).ToString();
					if (acc > numberToFind)
						break;
				}
			}
			return "-1";
		}
	}
}
