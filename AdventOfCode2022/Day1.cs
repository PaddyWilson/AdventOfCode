using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day1 : BaseDay
	{
		public Day1()
		{
			Day = "1";
			Answer1 = "69206";
			Answer2 = "197400";
		}

		protected override string Solution1(string[] input)
		{
			int output = 0;
			int count = 0;

			foreach (var item in input)
			{
				if (item == "")
				{
					if(count >= output) 
						output= count;
					count = 0;
				}
				else
				{
					count += int.Parse(item);
				}
			}

			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<int> list = new List<int>();

			int count = 0;
			foreach (var item in input)
			{
				if (item == "")
				{
					list.Add(count);
					count = 0;
				}
				else
				{
					count += int.Parse(item);
				}
			}

			list.Sort();
			list.Reverse();

			int output = list[0] + list[1] + list[2];

			return output.ToString();
		}
	}
}
