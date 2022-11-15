using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day13 : BaseDay
	{
		public Day13()
		{
			Day = "13";
			Answer1 = "709";
			Answer2 = "668";
		}

		protected override string Solution1(string[] input)
		{
			List<string> names = new List<string>();
			Dictionary<string, Dictionary<string, int>> peopleHappiness = new Dictionary<string, Dictionary<string, int>>();

			Parse(input, ref names, ref peopleHappiness);

			return Solve(names, peopleHappiness).ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<string> names = new List<string>();
			Dictionary<string, Dictionary<string, int>> peopleHappiness = new Dictionary<string, Dictionary<string, int>>();

			Parse(input, ref names, ref peopleHappiness);

			//add myself to the list
			string me = "Myself";
			foreach (var item in peopleHappiness)
				item.Value.Add(me, 0);

			peopleHappiness.Add(me, new Dictionary<string, int>());
			foreach (var item in names)
				peopleHappiness[me].Add(item, 0);

			names.Add(me);

			return Solve(names, peopleHappiness).ToString();
		}

		private void Parse(string[] input, ref List<string> names, ref Dictionary<string, Dictionary<string, int>> peopleHappiness)
		{
			foreach (var item in input)
			{
				string[] temp = item.Split(' ');

				if (!peopleHappiness.ContainsKey(temp[0]))
				{
					peopleHappiness.Add(temp[0], new Dictionary<string, int>());
					names.Add(temp[0]);

				}

				int units = int.Parse(temp[3]);
				string person = temp[temp.Length - 1].Replace(".", "");

				//2 == gain | lose
				if (temp[2] == "gain")
					peopleHappiness[temp[0]].Add(person, units);
				else
					peopleHappiness[temp[0]].Add(person, units * -1);
			}
		}

		private int Solve(List<string> names, Dictionary<string, Dictionary<string, int>> peopleHappiness)
		{
			List<List<string>> permutations = Helpers.GetPermutations(names);

			int output = int.MinValue;

			foreach (var tableSeating in permutations)
			{
				int happinessTotal = 0;

				for (int i = 0; i < tableSeating.Count; i++)
				{
					int other = i + 1;
					if (other == tableSeating.Count)
						other = 0;

					happinessTotal += peopleHappiness[tableSeating[i]][tableSeating[other]];
					happinessTotal += peopleHappiness[tableSeating[other]][tableSeating[i]];
				}

				if (happinessTotal > output)
					output = happinessTotal;
			}

			return output;

		}
	}
}
