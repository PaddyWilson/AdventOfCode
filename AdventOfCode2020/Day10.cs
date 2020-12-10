using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day10 : BaseDay
	{
		public Day10()
		{
			Day = "10";
			Answer1 = "2664";
			Answer2 = "148098383347712";
		}

		protected override string Solution1(string[] input)
		{
			List<int> numbers = new List<int>();
			foreach (var item in input)
				numbers.Add(int.Parse(item));
			numbers.Sort();

			int one = 0;
			int two = 0;
			int three = 0;

			int lastNumber = 0;
			foreach (var item in numbers)
			{
				int diff = item - lastNumber;
				lastNumber = item;
				if (diff == 1)
					one++;
				else if (diff == 2)
					two++;
				else if (diff == 3)
					three++;
			}

			three++;

			return (one * three).ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<int> numbers = new List<int>();
			numbers.Add(0);
			foreach (var item in input)
				numbers.Add(int.Parse(item));
			numbers.Sort();

			// i took heavy insperation from a git repo
			long[] counts = new long[numbers.Count];
			counts[0] = 1;
			for (int i = 1; i < numbers.Count; i++)
			{
				for (int j = 0; j < i; j++)
				{
					int diff = numbers[i] - numbers[j];
					if (diff <= 3)
						counts[i] += counts[j];
				}
			}
			return counts.Last().ToString();
		}

		private int Work2(int i, List<int> numbers, List<int> memo)
		{
			if (memo.Contains(i))
				return memo[i];
			if (i == numbers.Count - 1)
				return 1;
			int numWay = 0;
			for (int j = i + 1; j < numbers.Count; j++)
			{
				if (numbers[j] - numbers[i] > 3)
					break;
				int answer = Work2(j, numbers, memo);
				numWay += answer;
				memo.Add(j);
			}
			return numWay;
		}
	}
}
