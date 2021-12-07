using AOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
	public class Day7 : BaseDay
	{
		public Day7()
		{
			Day = "7";
			Answer1 = "356922";
			Answer2 = "100347031";
		}

		protected override string Solution1(string[] input)
		{
			string[] input2 = input[0].Split(',');
			int[] crabSubs = new int[input2.Length];

			for (int i = 0; i < input2.Length; i++)
				crabSubs[i] = int.Parse(input2[i]);

			long lowestFuel = long.MaxValue;

			for (int i = 0; i < 1000; i++)
			{
				long fuel = 0;
				foreach (var crab in crabSubs)
				{
					long diff = crab - i;
					if (diff < 0)
						diff *= -1;
					fuel += diff;
				}

				if (fuel < lowestFuel)
					lowestFuel = fuel;
			}

			return lowestFuel.ToString();
		}

		protected override string Solution2(string[] input)
		{
			string[] input2 = input[0].Split(',');
			int[] crabSubs = new int[input2.Length];

			int min = int.MaxValue;
			int max = 0;

			for (int i = 0; i < input2.Length; i++)
			{
				crabSubs[i] = int.Parse(input2[i]);
				if (crabSubs[i] < min)
					min = crabSubs[i];
				if (crabSubs[i] > max)
					max = crabSubs[i];
			}

			long lowestFuel = long.MaxValue;

			for (int i = min; i <= max; i++)
			{
				long fuel = 0;
				foreach (var crab in crabSubs)
				{
					long diff = crab - i;
					if (diff < 0)
						diff *= -1;

					for (int j = 0; j <= diff; j++)
						fuel += j;
				}

				if (fuel < lowestFuel)
					lowestFuel = fuel;
			}


			return lowestFuel.ToString();
		}
	}
}
