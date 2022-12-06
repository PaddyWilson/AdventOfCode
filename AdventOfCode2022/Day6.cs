using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "1855";
			Answer2 = "3256";
		}

		protected override string Solution1(string[] input)
		{
			List<char> signal = input[0].ToList();

			for (int i = 0; i < signal.Count - 4; i++)
			{
				var test = signal.GetRange(i, 4);

				Dictionary<char, int> counts = new Dictionary<char, int>();

				for (int x = 0; x < 4; x++)
				{
					if (!counts.ContainsKey(test[x]))
						counts.Add(test[x], 0);

					counts[test[x]]++;
				}

				if (counts.Count == 4)
					return (i + 4).ToString();
			}

			return "Can't find start of Signal";
		}

		protected override string Solution2(string[] input)
		{
			List<char> signal = input[0].ToList();

			for (int i = 0; i < signal.Count - 14; i++)
			{
				var test = signal.GetRange(i, 14);

				Dictionary<char, int> counts = new Dictionary<char, int>();

				for (int x = 0; x < 14; x++)
				{
					if (!counts.ContainsKey(test[x]))
						counts.Add(test[x], 0);

					counts[test[x]]++;
				}

				if (counts.Count == 14)
					return (i + 14).ToString();
			}

			return "Can't find start of message";
		}
	}
}
