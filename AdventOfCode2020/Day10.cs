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
			Answer2 = "0";
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
			foreach (var item in input)
				numbers.Add(int.Parse(item));
			numbers.Sort();

			int highest = numbers[numbers.Count - 1];// 183 with last adapter
			List<List<int>> lists = new List<List<int>>();
			lists.Add(new List<int>());
			lists[0].Add(0);

			List<int> deletes = new List<int>();
			foreach (var num in numbers)
			{
				deletes.Clear();
				for (int i = 0; i < lists.Count; i++)
				{
					int diff = num - lists[i][lists[i].Count - 1];

					if (diff == 0)
						continue;

					if (diff >= 4)
					{
						deletes.Add(i);
						continue;
					}

					int[] temp = new int[lists[i].Count + 1];
					lists[i].CopyTo(temp, 0);

					temp[temp.Length - 1] = num;

					lists.Add(new List<int>(temp));
				}

				for (int i = deletes.Count - 1; i >= 0; i--)
					lists.RemoveAt(deletes[i]);
			}

			int count = 0;
			foreach (var list in lists)
			{
				if (list[list.Count - 1] == highest)
					count++;
			}

			//int count = 0;
			//Works(numbers.ToArray(), 0, 0, ref count);

			return count.ToString();
		}


		private bool Works(int[] numbers, int lastNum, int index, ref int count)
		{
			//1 4 5 6 7 10 11 12 15 16 19 

			if (index >= numbers.Length)
			{
				if (index == numbers.Length && numbers[index - 1] == lastNum)
				{
					count++;
					return true;
				}
				else
					return false;
			}

			int num = numbers[index];
			int diff = num - lastNum;

			//i don't think this can be hit
			if (diff == 0)
			{
				//count++;
				return true;
			}

			if (diff >= 4)
				return false;

			Works(numbers, num, index + 1, ref count);
			Works(numbers, num, index + 2, ref count);
			Works(numbers, num, index + 3, ref count);

			return false;
		}
	}
}
