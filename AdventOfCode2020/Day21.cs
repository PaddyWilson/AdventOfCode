using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day21 : BaseDay
	{
		public Day21()
		{
			Day = "21";
			Answer1 = "0";
			Answer2 = "0";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, Dictionary<string, int>> food = new Dictionary<string, Dictionary<string, int>>();
			Dictionary<string, int> foodCount = new Dictionary<string, int>();

			foreach (var item in input)
			{
				string[] data = item.Split("(contains");

				string[] ingredients = data[0].Split(" ");
				string[] allergy = data[1].Split(" ");

				foreach (var fo in ingredients)
				{
					if (fo.Length > 0)
					{
						if (!food.ContainsKey(fo))
						{
							food.Add(fo, new Dictionary<string, int>());
							foodCount.Add(fo, 0);
						}

						foodCount[fo] += 1;

						foreach (var al in allergy)
						{
							string tempAl = al.Replace(",", "").Replace(")", "");

							if (tempAl.Length > 0)
							{
								if (!food[fo].ContainsKey(tempAl))
									food[fo].Add(tempAl, 0);

								food[fo][tempAl] += 1;
							}
						}
					}
				}
			}

			int count = 0;

			foreach (var item in food)
			{
				int c = 0;
				foreach (var al in item.Value)
				{
					if (al.Value != 1)
					{
						count += foodCount[item.Key];
					}
				}
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			return "-1";
		}
	}
}
