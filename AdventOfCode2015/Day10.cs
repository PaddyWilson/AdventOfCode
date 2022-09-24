using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day10 : BaseDay
	{
		public Day10()
		{
			Day = "10";
			Answer1 = "360154";
			Answer2 = "5103798";
		}

		protected override string Solution1(string[] input)
		{
			return SolveFaster(input[0], 40);
		}

		protected override string Solution2(string[] input)
		{
			return SolveFaster(input[0], 50);
		}

		//Very slow but works
		private string Solve(string input, int loopCount)
		{
			string look = input;
			string say = "";

			for (int i = 0; i < loopCount; i++)
			{
				for (int x = 0; x < look.Length;)
				{
					int count = 0;
					int y;

					for (y = x; y < look.Length; y++)
					{
						if (look[x] != look[y])
							break;
						count++;
					}

					say += count.ToString() + look[x].ToString();
					x += count;
				}

				look = say;
				say = "";
			}

			return look.Length.ToString();
		}

		//Much faster than Solve()
		//Using arrays and StringBuilder not just strings
		private string SolveFaster(string input, int loopCount)
		{
			char[] look = input.ToCharArray();
			StringBuilder say = new StringBuilder();

			for (int i = 0; i < loopCount; i++)
			{
				for (int x = 0; x < look.Length;)
				{
					int count = 0;
					int y;

					for (y = x; y < look.Length; y++)
					{
						if (look[x] != look[y])
							break;
						count++;
					}

					say.Append(count.ToString() + look[x]);
					x += count;
				}

				look = say.ToString().ToCharArray();
				say = new StringBuilder();
			}

			return look.Length.ToString();
		}
	}
}
