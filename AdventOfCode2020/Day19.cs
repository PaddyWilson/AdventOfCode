using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day19 : BaseDay
	{
		public Day19()
		{
			Day = "19";
			Answer1 = "115";
			Answer2 = "237";
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
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<string, Rules> rules = new Dictionary<string, Rules>();
			List<string> data = new List<string>();

			Parse(input, rules, data);

			string reg = "^" + CreateRegex("0", rules) + "$";

			Regex regex = new Regex(reg);

			//Console.WriteLine();
			int count = 0;
			foreach (var item in data)
			{
				//Console.WriteLine(item + " " + regex.IsMatch(item));
				if (regex.IsMatch(item))
					count++;
			}

			return count.ToString();// count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<string, Rules> rules = new Dictionary<string, Rules>();
			List<string> data = new List<string>();

			Parse(input, rules, data);

			rules.Remove("8");
			rules.Remove("11");

			AddRule("8: 42 | 42 8", rules);
			AddRule("11: 42 31 | 42 11 31", rules);

			string reg = "^" + CreateRegex("0", rules) + "$";

			Regex regex = new Regex(reg);

			//Console.WriteLine();
			int count = 0;
			foreach (var item in data)
			{
				//Console.WriteLine(item + " " + regex.IsMatch(item));
				if (regex.IsMatch(item))
					count++;
			}

			return count.ToString();// count.ToString();
		}

		private void Parse(string[] input, Dictionary<string, Rules> rules, List<string> data)
		{
			bool IsData = false;
			foreach (var item in input)
			{
				if (item == "")
				{
					IsData = true;
				}
				else if (!IsData)
				{
					AddRule(item, rules);
				}
				else if (IsData)
				{
					data.Add(item);
				}
			}
		}

		private void AddRule(string rule, Dictionary<string, Rules> rules)
		{
			string[] temp = rule.Split(" ");

			string index = temp[0].Replace(":", "");

			rules.Add(index, new Rules(index));

			for (int i = 1; i < temp.Length; i++)
				rules[index].Rule.Add(temp[i].Replace("\"", ""));
		}

		int maxDepth = 40;
		private string CreateRegex(string nextRule, Dictionary<string, Rules> rules, int depth = 0)
		{
			if (depth == maxDepth)
				return "";

			string output = "";
			output += "(";
			foreach (var item in rules[nextRule].Rule)
			{
				if (item == "|")
					output += "|";
				else if (item == "a")
					output += "a";
				else if (item == "b")
					output += "b";
				else
					output += CreateRegex(item, rules, depth++);
			}
			output += ")";
			return output;
		}
	}
}
