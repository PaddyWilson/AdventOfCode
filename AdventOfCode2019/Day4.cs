using AOC;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode2019
{
	public class Day4 : BaseDay
	{
		public Day4()
		{
			Day = "4";
			Answer1 = "1099";
			Answer2 = "710";
		}

		protected override string Solution1(string[] input)
		{
			long count = 0;

			long start = long.Parse(input[0].Split('-')[0]);
			long end = long.Parse(input[0].Split('-')[1]);

			for (long i = start; i <= end; i++)
			{
				bool maybe = false;
				for (long x = 0; x < 5; x++)
				{
					//two of the same digits in a row
					if (i.ToString()[(int)x] == i.ToString()[(int)x + 1])
					{
						maybe = true;
						break;
					}
				}

				if (!maybe) continue;
				maybe = false;
				for (long x = 0; x < 5; x++)
				{
					char a = i.ToString()[(int)x];
					char b = i.ToString()[(int)x + 1];
					if (i.ToString()[(int)x] > i.ToString()[(int)x + 1])
					{
						maybe = false;
						break;
					}

					maybe = true;
				}

				if (!maybe)
					continue;

				count++;
			}

			return count.ToString();
		}


		protected override string Solution2(string[] input)
		{
			long count = 0;

			long start = long.Parse(input[0].Split('-')[0]);
			long end = long.Parse(input[0].Split('-')[1]);

			List<long> results = new List<long>();

			for (long i = start; i <= end; i++)
			{
				bool maybe = false;

				long ct = 0;
				for (long x = 0; x < 5; x++)
				{
					//two of the same digits in a row
					if (i.ToString()[(int)x] == i.ToString()[(int)x + 1])
					{
						ct++;
						maybe = true;
					}
				}

				if (!maybe) continue;
				maybe = false;
				for (long x = 0; x < 5; x++)
				{
					char a = i.ToString()[(int)x];
					char b = i.ToString()[(int)x + 1];
					if (i.ToString()[(int)x] > i.ToString()[(int)x + 1])
					{
						maybe = false;
						break;
					}

					maybe = true;
				}

				if (!maybe)
					continue;

				count++;
				results.Add(i);
			}

			count = 0;
			foreach (var item in results)
			{
				Dictionary<char, long> c = new Dictionary<char, long>();
				for (long x = 0; x < 5; x++)
				{
					//two of the same digits in a row
					if (item.ToString()[(int)x] == item.ToString()[(int)x + 1])
					{
						if (!c.ContainsKey(item.ToString()[(int)x]))
							c.Add(item.ToString()[(int)x], 1);
						else
							c[item.ToString()[(int)x]]++;
					}
				}

				foreach (var item2 in c)
				{
					if (item2.Value == 1)
					{
						count++;
						break;
					}
				}

			}

			return count.ToString();
		}
	}
}
