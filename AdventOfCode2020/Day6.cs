using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "6686";
			Answer2 = "3476";
		}

		protected override string Solution1(string[] input)
		{
			List<List<char>> groupAnswer = new List<List<char>>();
			groupAnswer.Add(new List<char>());

			int i = 0;
			foreach (var answer in input)
			{
				if (answer == "")
				{
					groupAnswer.Add(new List<char>());
					i += 1;
				}
				else
				{
					foreach (var c in answer)
					{
						if (!groupAnswer[i].Contains(c))
							groupAnswer[i].Add(c);
					}
				}
			}

			int count = 0;
			foreach (var item in groupAnswer)
				count += item.Count;

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			//find all the letters
			List<List<char>> group = new List<List<char>>();
			group.Add(new List<char>());
			int index = 0;
			foreach (var answer in input)
			{
				if (answer == "")
				{
					group.Add(new List<char>());
					index += 1;
				}
				else
				{
					foreach (var c in answer)
					{
						if (!group[index].Contains(c))
							group[index].Add(c);
					}
				}
			}

			int output = 0;
			List<List<char>> groupAnswer = new List<List<char>>();
			groupAnswer.Add(new List<char>());
			int groupCount = 0;
			for (int i = 0; i < input.Length;)
			{
				if (input[i] == "")
				{
					group.Add(new List<char>());
					i++;
					groupCount++;
				}

				if (i >= input.Length)
					break;

				//get group
				List<string> list = new List<string>();
				while (input[i] != "")
				{
					list.Add(input[i]);
					i++;

					if (i >= input.Length)
						break;
				}

				//find dupes
				foreach (var c in group[groupCount])
				{
					int yesCount = 0;

					foreach (var item in list)
					{
						if (item.Contains(c))
							yesCount++;
					}

					if (yesCount == list.Count)
						output += 1;
				}

			}

			return output.ToString();
		}
	}
}
