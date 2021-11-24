using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day19 : BaseDay
	{
		public Day19()
		{
			Day = "19";
			Answer1 = "0";
			Answer2 = "0";
		}

		private class Rules
		{
			public string Index { get; set; }
			public List<string> Rule { get; set; }

			public Rules(string Index)
			{
				this.Index = Index;
				this.Rule = new List<string>();
			}

			//public bool Match() {
			//	return false;
			//}
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, Rules> rules = new Dictionary<string, Rules>();
			List<string> data = new List<string>();

			bool IsData = false;
			foreach (var item in input)
			{
				if (item == "")
				{
					IsData = true;
				}
				else if (!IsData)
				{
					string[] temp = item.Split(" ");

					string index = temp[0].Replace(":", "");

					rules.Add(index, new Rules(index));

					for (int i = 1; i < temp.Length; i++)
						rules[index].Rule.Add(temp[i]);
				}
				else if (IsData)
				{
					data.Add(item);
				}
			}

			int count = 0;
			foreach (var item in data)
			{
				if (IsMatch(item, rules))
					count++;
			}


			return "-1";// count.ToString();
		}

		private bool IsMatch(string item, Dictionary<string, Rules> rules, string ruleIndex = "0", int depth = 0)
		{
			if (depth >= item.Length)
			{
				Console.WriteLine("Too Long");
				return false;
			}
			else if (false)
			{

			}




			return false;
		}

		protected override string Solution2(string[] input)
		{
			return "-1";
		}
	}
}
