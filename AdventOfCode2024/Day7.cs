using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day7 : BaseDay
	{
		public Day7()
		{
			Day = "7";
			Answer1 = "21572148763543";
			Answer2 = "581941094529163";
		}

		protected override string Solution1(string[] input)
		{
			ulong output = 0;
			foreach (var item in input)
			{
				var numbers = item.ExtractULong();
				if (Solve(numbers, 0, 1))
					output += numbers[0];
			}
			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			ulong output = 0;
			foreach (var item in input)
			{
				var numbers = item.ExtractULong();
				if (Solve(numbers, 0, 1, true))
					output += numbers[0];
			}
			return output.ToString();
		}
		
		bool Solve(List<ulong> numbers, ulong value, int index, bool part2 = false)
		{
			if (numbers[0] == value && numbers.Count <= index)
				return true;
			else if (numbers[0] != value && numbers.Count <= index)
				return false;
			else if (numbers[0] < value)
				return false;

			if (Solve(numbers, value + numbers[index], index + 1, part2))
				return true;
			else if (Solve(numbers, value * numbers[index], index + 1, part2))
				return true;
			else if (part2 && Solve(numbers, ulong.Parse(value.ToString() + numbers[index].ToString()), index + 1, part2))
				return true;
			return false;
		}
	}
}
