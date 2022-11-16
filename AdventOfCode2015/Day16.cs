using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day16 : BaseDay
	{
		public Day16()
		{
			Day = "16";
			Answer1 = "103";
			Answer2 = "405";
		}

		protected override string Solution1(string[] input)
		{
			List<Dictionary<string, int>> sueList = Parse(input);

			Dictionary<string, int> matchData = new Dictionary<string, int>()
			{
				{"children", 3},
				{"cats", 7},
				{"samoyeds", 2},
				{"pomeranians", 3},
				{"akitas", 0},
				{"vizslas", 0},
				{"goldfish", 5},
				{"trees", 3},
				{"cars", 2},
				{"perfumes", 1 },
			};

			int sueCount = 0;
			foreach (var sue in sueList)
			{
				sueCount++;
				int matches = 0;
				foreach (var item in sue)
				{
					if (matchData[item.Key] == item.Value)
						matches++;
				}
				if (matches == 3)
				{
					return sueCount.ToString();
				}
			}

			return 0.ToString();
		}



		protected override string Solution2(string[] input)
		{
			List<Dictionary<string, int>> sueList = Parse(input);

			Dictionary<string, int> matchData = new Dictionary<string, int>()
			{
				{"children", 3},
				{"cats", 7},
				{"samoyeds", 2},
				{"pomeranians", 3},
				{"akitas", 0},
				{"vizslas", 0},
				{"goldfish", 5},
				{"trees", 3},
				{"cars", 2},
				{"perfumes", 1 },
			};

			int sueCount = 0;
			foreach (var sue in sueList)
			{
				sueCount++;
				int matches = 0;
				foreach (var item in sue)
				{
					if (item.Key == "cats" || item.Key == "trees")
					{
						if (matchData[item.Key] < item.Value)
							matches++;
					}
					else if (item.Key == "pomeranians" || item.Key == "goldfish")
					{
						if (matchData[item.Key] > item.Value)
							matches++;
					}
					else if (matchData[item.Key] == item.Value)
						matches++;
				}
				if (matches == 3)
				{
					return sueCount.ToString();
				}
			}

			return 0.ToString();
		}

		private List<Dictionary<string, int>> Parse(string[] input)
		{
			List<Dictionary<string, int>> sues = new List<Dictionary<string, int>>();

			foreach (var item in input)
			{
				string[] temp = item.Replace(":", "").Replace(",", "").Split(" ");

				Dictionary<string, int> list = new Dictionary<string, int>();

				list.Add(temp[2], int.Parse(temp[3]));
				list.Add(temp[4], int.Parse(temp[5]));
				list.Add(temp[6], int.Parse(temp[7]));

				sues.Add(list);
			}
			return sues;
		}
	}
}
