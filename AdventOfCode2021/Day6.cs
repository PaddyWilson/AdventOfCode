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

			//List<int> fish = new List<int>(numbers);
			Dictionary<int, long> fish = new Dictionary<int, long>();
			for (int i = 0; i < 9; i++)
				fish.Add(i, 0);
			foreach (var item in numbers)
				fish[item]++;

			int days = 1;
			while (days <= 80)
			{
				long temp = fish[0];
				fish[0] = fish[1];
				fish[1] = fish[2];
				fish[2] = fish[3];
				fish[3] = fish[4];
				fish[4] = fish[5];
				fish[5] = fish[6];
				fish[6] = fish[7];
				fish[7] = fish[8];

				fish[6] += temp;
				fish[8] = temp;

				days++;
			}
			ulong count = 0;
			foreach (var item in fish)
				count += (ulong)item.Value;
			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			string[] tempNum = input[0].Split(',');
			int[] numbers = new int[tempNum.Length];
			for (int i = 0; i < tempNum.Length; i++)
				numbers[i] = int.Parse(tempNum[i]);

			//List<int> fish = new List<int>(numbers);
			Dictionary<int, long> fish = new Dictionary<int, long>();
			for (int i = 0; i < 9; i++)
				fish.Add(i, 0);
			foreach (var item in numbers)
				fish[item]++;

			int days = 1;
			while (days <= 256)
			{
				long temp = fish[0];
				fish[0] = fish[1];
				fish[1] = fish[2];
				fish[2] = fish[3];
				fish[3] = fish[4];
				fish[4] = fish[5];
				fish[5] = fish[6];
				fish[6] = fish[7];
				fish[7] = fish[8];

				fish[6] += temp;
				fish[8] = temp;

				days++;
			}

			ulong count = 0;
			foreach (var item in fish)
				count += (ulong)item.Value;
			return count.ToString();
		}
	}
}
