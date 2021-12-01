using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day1 : BaseDay
	{
		public Day1()
		{
			Day = "1";
			Answer1 = "74";
			Answer2 = "1795";
		}

		protected override string Solution1(string[] input)
		{
			int count = 0;

			foreach (var character in input[0])
			{
				if (character == '(')
					count++;
				else if (character == ')')
					count--;
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int count = 0;
			int index = 0;
			foreach (var character in input[0])
			{
				index++;

				if (character == '(')
					count++;
				else if (character == ')')
					count--;

				if (count == -1)
					break;
			}

			return index.ToString();
		}
	}
}
