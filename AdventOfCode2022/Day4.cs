using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Day4 : BaseDay
	{
		public Day4()
		{
			Day = "4";
			Answer1 = "490";
			Answer2 = "921";
		}

		protected override string Solution1(string[] input)
		{
			int output = 0;

			foreach (var item in input)
			{
				string[] temp = item.Split(',');

				(int, int) team1 = (int.Parse(temp[0].Split('-')[0]), int.Parse(temp[0].Split('-')[1]));
				(int, int) team2 = (int.Parse(temp[1].Split('-')[0]), int.Parse(temp[1].Split('-')[1]));

				if (team1.Item1 <= team2.Item1 && team1.Item2 >= team2.Item2)
				{
					output++;
				}
				else if (team1.Item1 >= team2.Item1 && team1.Item2 <= team2.Item2)
				{
					output++;
				}
			}

			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int output = 0;

			foreach (var item in input)
			{
				string[] temp = item.Split(',');

				(int, int) team1 = (int.Parse(temp[0].Split('-')[0]), int.Parse(temp[0].Split('-')[1]));
				(int, int) team2 = (int.Parse(temp[1].Split('-')[0]), int.Parse(temp[1].Split('-')[1]));

				if (team1.Item1 <= team2.Item1 && team1.Item2 >= team2.Item2)
				{
					output++;
				}
				else if (team1.Item1 >= team2.Item1 && team1.Item2 <= team2.Item2)
				{
					output++;
				}
				else if (team1.Item1 <= team2.Item1 && team1.Item2 <= team2.Item2)
				{
					for (int i = team1.Item1; i <= team1.Item2; i++)
					{
						for (int j = team2.Item1; j <= team2.Item2; j++)
						{
							if (i == j)
							{
								output++;
								i += 1000000;//for breaking out of the two loops
								j += 1000000;//for breaking out of the two loops
							}
						}
					}
				}
				else if (team1.Item1 >= team2.Item1 || team1.Item2 >= team2.Item2)
				{
					for (int i = team2.Item1; i <= team2.Item2; i++)
					{
						for (int j = team1.Item1; j <= team1.Item2; j++)
						{
							if (i == j)
							{
								output++;
								i += 1000000;//for breaking out of the two loops
								j += 1000000;//for breaking out of the two loops
							}
						}
					}
				}
			}

			return output.ToString();
		}
	}
}
