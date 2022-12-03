using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day3 : BaseDay
	{
		public Day3()
		{
			Day = "3";
			Answer1 = "8202";
			Answer2 = "2864";
		}

		protected override string Solution1(string[] input)
		{
			int count = 0;
			foreach (var item in input)
			{
				char[] one = item.Substring(0, (item.Length / 2)).ToCharArray();
				char[] two = item.Substring(item.Length / 2).ToCharArray();

				foreach (var c in one)
				{
					if (two.Contains(c))
					{
						count += Priorities(c);
						break;
					}
				}
			}
			return count.ToString();
		}		

		protected override string Solution2(string[] input)
		{
			int count = 0;

			char[] al = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();

			for (int i = 0; i < input.Length; i+=3)
			{
				foreach (var c in al)
				{
					if (input[i].Contains(c) && input[i+1].Contains(c) && input[i+2].Contains(c))
					{
						count += Priorities(c);
						break;
					}
				}
			}
			return count.ToString();
		}
		
		private int Priorities(char input)
		{
			if (input >= 'A' && input <= 'Z')
				return input - 65 + 27;

			if (input >= 'a' && input <= 'z')
				return input - 96;

			return 0;
		}
	}
}
