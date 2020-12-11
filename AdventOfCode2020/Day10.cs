using System;
using System.Collections.Generic;
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
			int three = 0;

			int lastNumber = 0;
			foreach (var item in numbers)
			{
				int diff = item - lastNumber;
				lastNumber = item;
				if (diff == 1)
					one++;
				else if (diff == 3)
					three++;
			}

			three++;

			return (one * three).ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<int> numbers = new List<int>();
			foreach (var item in input)
				numbers.Add(int.Parse(item));
			numbers.Add(0);
			numbers.Sort();

			long count = Works(numbers.ToArray(), 0, 1);
			count += Works(numbers.ToArray(), 0, 2);
			count += Works(numbers.ToArray(), 0, 3);
			past.Clear();//remove the past testing info 
			return count.ToString();
		}

		//this dictionary remembers past answers to the works Function
		//so it does not have to recalculate things
		Dictionary<string, long> past = new Dictionary<string, long>();
		private long Works(int[] numbers, int lastNum, int index)
		{
			//hit end of numbers array
			if (index == numbers.Length)
				return 1;

			if (past.ContainsKey(lastNum + " " + numbers[index]))
				return past[lastNum + " " + numbers[index]];

			//int n = numbers[index];
			int diff = numbers[index] - lastNum;

			long count = 0;
			if (diff <= 3)
			{
				//if (index + 1 < numbers.Length)
				count += Works(numbers, numbers[index], index + 1);
				if (index + 2 < numbers.Length)
					count += Works(numbers, numbers[index], index + 2);
				if (index + 3 < numbers.Length)
					count += Works(numbers, numbers[index], index + 3);
			}

			past.Add(lastNum + " " + numbers[index], count);
			return count;
		}
	}
}