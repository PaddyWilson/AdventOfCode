using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day17 : BaseDay
	{
		public Day17()
		{
			Day = "17";
			Answer1 = "1304";
			Answer2 = "18";
		}

		protected override string Solution1(string[] input)
		{
			List<int> jugs = Parse(input);

			int maxEggnog = 150;
			//TEST INPUT MAX = 25
			if (input.Length < 10)
				maxEggnog = 25;

			List<List<(char, int)>> jugCombos = GetCombos(jugs, maxEggnog);

			return jugCombos.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<int> jugs = Parse(input);

			int maxEggnog = 150;
			//TEST INPUT MAX = 25
			if (input.Length < 10)
				maxEggnog = 25;

			//Already have the dtaa from Solution1()
			//List<List<(char, int)>> jugCombos = GetCombos(jugs, maxEggnog);

			Dictionary<int, int> jugsCounts = new Dictionary<int, int>();
			foreach (var item in getCombosMemory)
			{
				if (!jugsCounts.ContainsKey(item.Key.Length))
					jugsCounts.Add(item.Key.Length, 0);

				jugsCounts[item.Key.Length] += 1;
			}

			int lowest = int.MaxValue;
			foreach (var item in jugsCounts.Values)
				if(lowest > item)
					lowest = item;

			return lowest.ToString();
		}

		private List<int> Parse(string[] input)
		{
			List<int> result = new List<int>();

			foreach (var item in input)
				result.Add(int.Parse(item));

			return result;
		}

		private List<List<(char, int)>> GetCombos(List<int> jugs, int maxEggnog)
		{
			//reset memory on runs
			getCombosMemory.Clear();

			List<List<(char, int)>> results = new List<List<(char, int)>>();

			int amount = 0;

			//to make sure that no jug is used twice
			// assign a index to the jug. the char is the index for easy sorting
			List<(char, int)> jugsIndexed = new List<(char, int)>();

			char tChar = 'A';
			foreach (var item in jugs)
			{
				jugsIndexed.Add((tChar, item));
				tChar++;
			}

			List<(char, int)> currentJugs = new List<(char, int)>();

			GetCombos(jugsIndexed, maxEggnog, ref results, ref currentJugs, ref amount);

			return results;
		}

		//stores the indexed combos of the nogJugs
		Dictionary<string, bool> getCombosMemory = new Dictionary<string, bool>();
		private void GetCombos(List<(char, int)> jugs, int maxEggnog, ref List<List<(char, int)>> results, ref List<(char, int)> currentJugs, ref int amount)
		{
			if (amount == maxEggnog)
			{
				List<char> usedJugIndexes = new List<char>();
				foreach (var item in currentJugs)
					usedJugIndexes.Add(item.Item1);
				usedJugIndexes.Sort();

				string jugString = "";
				foreach (var item in usedJugIndexes)
					jugString += item;

				if (getCombosMemory.ContainsKey(jugString))
					return;

				getCombosMemory.Add(jugString, true);
				results.Add(new List<(char, int)>(currentJugs));
			}
			if (amount > maxEggnog)
				return;

			for (int i = jugs.Count - 1; i >= 1; i--)
			{
				(char, int) jugSize = jugs[0];

				amount += jugSize.Item2;
				jugs.RemoveAt(0);
				currentJugs.Add(jugSize);
				GetCombos(jugs, maxEggnog, ref results, ref currentJugs, ref amount);

				amount -= jugSize.Item2;
				jugs.Add(jugSize);
				currentJugs.RemoveAt(currentJugs.Count - 1);
			}
		}
	}
}
