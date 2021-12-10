using AOC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
	public class Day10 : BaseDay
	{
		public Day10()
		{
			Day = "10";
			Answer1 = "464991";
			Answer2 = "3662008566";
		}

		protected override string Solution1(string[] input)
		{
			Dictionary<char, int> tally = new Dictionary<char, int>();
			tally.Add('(', 0);
			tally.Add(')', 0);
			tally.Add('[', 0);
			tally.Add(']', 0);
			tally.Add('{', 0);
			tally.Add('}', 0);
			tally.Add('<', 0);
			tally.Add('>', 0);

			List<char> open = new List<char>() { '(', '[', '{', '<', };
			List<char> close = new List<char>() { ')', ']', '}', '>', };

			foreach (var item in input)
			{
				List<char> characters = new List<char>(item.ToArray());
				for (int i = characters.Count - 2; i >= 0; i--)
				{
					for (int j = 0; j < open.Count; j++)
					{
						if (characters[i] == open[j] && i + 1 < characters.Count && characters[i + 1] == close[j])
						{
							characters.RemoveRange(i, 2);
							break;
						}
					}
				}

				for (int i = 0; i < characters.Count; i++)
				{
					for (int j = 0; j < close.Count; j++)
					{
						if (characters[i] == close[j])
						{
							tally[characters[i]]++;
							i = int.MaxValue - 10;
							j = int.MaxValue - 10;
						}
					}
				}
			}

			int output = 0;
			output += tally[')'] * 3;
			output += tally[']'] * 57;
			output += tally['}'] * 1197;
			output += tally['>'] * 25137;
			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			Dictionary<char, int> tally = new Dictionary<char, int>();
			tally.Add('(', 0);
			tally.Add(')', 0);
			tally.Add('[', 0);
			tally.Add(']', 0);
			tally.Add('{', 0);
			tally.Add('}', 0);
			tally.Add('<', 0);
			tally.Add('>', 0);

			List<char> open = new List<char>() { '(', '[', '{', '<', };
			List<char> close = new List<char>() { ')', ']', '}', '>', };

			List<string> incompleteStrings = new List<string>();

			foreach (var item in input)
			{
				List<char> characters = new List<char>(item.ToArray());
				//string temp = new string(characters.ToArray());
				//Console.WriteLine(temp);
				for (int i = characters.Count - 2; i >= 0; i--)
				{
					for (int j = 0; j < open.Count; j++)
					{
						if (characters[i] == open[j] && i + 1 < characters.Count && characters[i + 1] == close[j])
						{
							characters.RemoveRange(i, 2);
							break;
						}
					}
				}
				bool corrupted = false;
				for (int i = 0; i < characters.Count; i++)
				{
					for (int j = 0; j < close.Count; j++)
					{
						if (characters[i] == close[j])
						{
							corrupted = true;
							i = int.MaxValue - 10;
							j = int.MaxValue - 10;
						}
					}
				}
				if (!corrupted)
				{
					incompleteStrings.Add(new string(characters.ToArray()));
				}
			}

			List<long> scores = new List<long>();

			foreach (var item in incompleteStrings)
			{
				string closedString = "";
				for (int i = item.Length - 1; i >= 0; i--)
				{
					if (item[i] == '(')
						closedString += ')';
					else if (item[i] == '[')
						closedString += ']';
					else if (item[i] == '{')
						closedString += '}';
					else if (item[i] == '<')
						closedString += '>';
				}

				long totalScore = 0;
				for (int i = 0; i < closedString.Length; i++)
				{
					totalScore *= 5;
					if (closedString[i] == ')')
						totalScore += 1;
					else if (closedString[i] == ']')
						totalScore += 2;
					else if (closedString[i] == '}')
						totalScore += 3;
					else if (closedString[i] == '>')
						totalScore += 4;
				}
				scores.Add(totalScore);
			}
			scores.Sort();
			return scores[scores.Count / 2].ToString();
		}

		private char FindCorrupted(List<char> characters)
		{



			return 'c';
		}

	}
}
