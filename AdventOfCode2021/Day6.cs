using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "393019";
			Answer2 = "1757714216975";
		}

		protected override string Solution1(string[] input)
		{
			string[] tempNum = input[0].Split(',');
			int[] numbers = new int[tempNum.Length];
			for (int i = 0; i < tempNum.Length; i++)
				numbers[i] = int.Parse(tempNum[i]);

			long[] fish = new long[9];
			foreach (var item in numbers)
				fish[item]++;

			return CalculateFish(fish, 80).ToString();
		}

		protected override string Solution2(string[] input)
		{
			string[] tempNum = input[0].Split(',');
			int[] numbers = new int[tempNum.Length];
			for (int i = 0; i < tempNum.Length; i++)
				numbers[i] = int.Parse(tempNum[i]);

			long[] fish = new long[9];
			foreach (var item in numbers)
				fish[item]++;

			return CalculateFish(fish, 256).ToString();
		}

		private ulong CalculateFish(long[] fish, int days)
		{
			int day = 1;
			while (day <= days)
			{
				long temp = fish[0];
				for (int i = 0; i < fish.Length-1; i++)
					fish[i] = fish[i + 1];

				fish[6] += temp;
				fish[8] = temp;

				day++;
			}

			ulong count = 0;
			foreach (var item in fish)
				count += (ulong)item;
			return count;
		}
	}
}
