using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day5 : BaseDay
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "236";
			Answer2 = "51";
		}

		protected override string Solution1(string[] input)
		{
			List<string> output = new List<string>();

			//test input
			//Console.WriteLine(NiceString1("ugknbfddgicrmopn"));
			//Console.WriteLine(NiceString1("aaa"));
			//Console.WriteLine(NiceString1("jchzalrnumimnmhp"));
			//Console.WriteLine(NiceString1("haegwjzuvuyypxyu"));
			//Console.WriteLine(NiceString1("dvszwmarrgswjxmb"));

			foreach (var item in input)
			{
				if (NiceString1(item))
					output.Add(item);
			}

			return output.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<string> output = new List<string>();

			//test input
			//Console.WriteLine(NiceString2("qjhvhtzxzqqjkmpb"));
			//Console.WriteLine(NiceString2("aaaa"));
			//Console.WriteLine(NiceString2("xxyxx"));
			//Console.WriteLine(NiceString2("uurcxstgmygtbstg"));
			//Console.WriteLine(NiceString2("ieodomkazucvgmuy"));

			foreach (var item in input)
			{
				if (NiceString2(item))
					output.Add(item);
			}

			return output.Count.ToString();
		}

		private bool NiceString2(string input)
		{
			int pairCount = 0;
			for (int i = 0; i < input.Length - 2; i++)
			{
				string sub = input.Substring(i + 2);//, input.Length - 1);
				if (sub.Contains(input[i].ToString() + input[i + 1].ToString()))
				{
					pairCount++;
					break;
				}
			}

			int repeatCount = 0;

			for (int i = 0; i < input.Length - 2; i++)
			{
				if (input[i] == input[i + 2])
				{
					repeatCount++;
				}
			}

			if (pairCount >= 1 && repeatCount >= 1)
				return true;

			return false;
		}

		private bool NiceString1(string input)
		{
			if (input.Contains("ab") || input.Contains("cd") || input.Contains("pq") || input.Contains("xy"))
				return false;

			int twiceCount = 0;
			for (int i = 0; i < input.Length - 1; i++)
			{
				if (input[i] == input[i + 1])
					twiceCount++;
			}

			int vowelCount = 0;

			for (int i = 0; i < input.Length; i++)
			{
				switch (input[i])
				{
					case 'a':
					case 'e':
					case 'i':
					case 'o':
					case 'u':
						vowelCount++;
						break;
				}
			}

			if (twiceCount >= 1 && vowelCount >= 3)
				return true;

			return false;
		}
	}
}
