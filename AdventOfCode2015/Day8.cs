using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day8 : BaseDay
	{
		public Day8()
		{
			Day = "8";
			Answer1 = "1333";
			Answer2 = "2046";
		}

		protected override string Solution1(string[] input)
		{
			int countOfChars = 0;
			int countOfEscapeChars = 0;

			foreach (var item in input)
			{
				countOfChars += item.Length;

				countOfEscapeChars -= 2;
				for (int i = 0; i < item.Length; i++)
				{
					if (item[i] == '\\')
					{
						if (item[i + 1] == 'x')
						{
							countOfEscapeChars += 1;
							i += 3;
						}
						else if (item[i + 1] == '\\' || item[i + 1] == '\"')
						{
							countOfEscapeChars += 1;
							i++;
						}
					}
					else
					{
						countOfEscapeChars++;
					}
				}
			}

			return (countOfChars - countOfEscapeChars).ToString();
		}

		protected override string Solution2(string[] input)
		{
			int countOfChars = 0;
			int countOfEscapeChars = 0;

			foreach (var item in input)
			{
				countOfChars += item.Length;

				countOfEscapeChars += 2;
				for (int i = 0; i < item.Length; i++)
				{
					if (item[i] == '\\' || item[i] == '\"')
					{
						countOfEscapeChars += 2;
					}
					else
					{
						countOfEscapeChars++;
					}
				}
			}

			return (countOfEscapeChars - countOfChars).ToString();
		}
	}
}
