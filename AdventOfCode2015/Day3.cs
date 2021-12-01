using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2015
{
	public class Day3 : BaseDay
	{
		public Day3()
		{
			Day = "3";
			Answer1 = "2081";
			Answer2 = "2341";
		}

		protected override string Solution1(string[] input)
		{
			int x = 0;
			int y = 0;

			Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();
			visited.Add((x, y), 1);

			foreach (var character in input[0])
			{
				Move(visited, ref x,ref y, character);
			}

			return visited.Count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int xS = 0;
			int yS = 0;

			int xR = 0;
			int yR = 0;

			Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();
			visited.Add((0, 0), 1);

			for (int i = 0; i < input[0].Length - 1; i+=2)
			{
				Move(visited, ref xS, ref yS, input[0][i]);
				Move(visited, ref xR, ref yR, input[0][i+1]);
			}

			return visited.Count.ToString();
		}

		private void Move(Dictionary<(int, int), int> visited, ref int x, ref int y, char character)
		{
			if (character == '>')
				x++;
			else if (character == '<')
				x--;
			else if (character == '^')
				y++;
			else if (character == 'v')
				y--;

			if (!visited.ContainsKey((x, y)))
				visited.Add((x, y), 1);
			else
				visited[(x, y)] += 1;
		}
	}
}
