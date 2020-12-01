using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day1 : BaseDay
	{
		public Day1()
		{
			Day = "1";
			Answer1 = "440979";
			Answer2 = "82498112";
		}

		protected override string Solution1(string[] input)
		{
			int[] numbers = new int[input.Length];

			for (int i = 0; i < input.Length; i++)
				numbers[i] = int.Parse(input[i]);

			int number1 = 0;
			int number2 = 0;

			for (int i = 0; i < numbers.Length; i++)
			{
				for (int j = i + 1; j < numbers.Length; j++)
				{
					if ((numbers[i] + numbers[j]) == 2020)
					{
						number1 = numbers[i];
						number2 = numbers[j];
					}
				}
			}

			return (number1 * number2).ToString();
		}

		protected override string Solution2(string[] input)
		{
			int[] numbers = new int[input.Length];

			for (int i = 0; i < input.Length; i++)
				numbers[i] = int.Parse(input[i]);

			int number1 = 0;
			int number2 = 0;
			int number3 = 0;

			for (int i = 0; i < numbers.Length; i++)
			{
				for (int j = i + 1; j < numbers.Length; j++)
				{
					for (int k = j + 1; k < numbers.Length; k++)
					{
						if ((numbers[i] + numbers[j]) + numbers[k] == 2020)
						{
							number1 = numbers[i];
							number2 = numbers[j];
							number3 = numbers[k];
						}
					}
				}
			}

			return (number1 * number2 * number3).ToString();
		}
	}
}
