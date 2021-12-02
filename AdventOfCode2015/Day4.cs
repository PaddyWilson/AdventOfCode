using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day4 : BaseDay
	{
		public Day4()
		{
			Day = "4";
			Answer1 = "117946";
			Answer2 = "3938038";
		}

		protected override string Solution1(string[] input)
		{
			string baseHash = input[0];
			int count = 0;

			while (true)
			{
				if (Helpers.CreateMD5(baseHash + count).Substring(0, 5) == "00000")
					break;

				count++;
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			string baseHash = input[0];
			int count = 0;

			while (true)
			{
				if (Helpers.CreateMD5(baseHash + count).Substring(0, 6) == "000000")
					break;

				count++;
			}

			return count.ToString();
		}
	}
}
