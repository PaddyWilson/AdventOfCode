using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day6 : BaseDay
	{
		public Day6()
		{
			Day = "6";
			Answer1 = "5131";
			Answer2 = "1784";
		}

		protected override string Solution1(string[] input)
		{
			char[,] map;
			bool[,] visited;
			(int, int) guard;
			Parse(input, out map, out visited, out guard);
			(int, int) direction = (-1, 0);
			int xSize = map.GetLength(0);
			int ySize = map.GetLength(1);

			while (true)
			{
				(int, int) next = (guard.Item1 + direction.Item1, guard.Item2 + direction.Item2);
				if (next.Item1 < 0 || next.Item2 < 0 || next.Item1 >= xSize || next.Item2 >= ySize)
					break;
				if (map[next.Item1, next.Item2] == '#')
				{
					if (direction.Item1 == -1)//up
						direction = (0, 1);
					else if (direction.Item1 == 1)//down
						direction = (0, -1);
					else if (direction.Item2 == -1)//left
						direction = (-1, 0);
					else if (direction.Item2 == 1)//right
						direction = (+1, 0);
				}
				else
				{
					visited[next.Item1, next.Item2] = true;
					guard = next;
				}
			}

			int output = 0;
			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					if (visited[x, y])
						output++;
				}
			}
			return output.ToString();
		}

		protected override string Solution2(string[] input)
		{
			char[,] map;
			bool[,] visited;
			(int, int) guardStartPos;
			Parse(input, out map, out visited, out guardStartPos);
			int xSize = map.GetLength(0);
			int ySize = map.GetLength(1);

			int output = 0;
			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					if (map[x, y] == '.' && guardStartPos != (x, y))
					{
						(int, int) direction = (-1, 0);
						(int, int) guard = guardStartPos;
						map[x, y] = '#';

						//dont worry about if it loops just see if it has ran a long time
						//ajust as needed
						int loopCount = 0;
						while (loopCount <= 10000)
						{
							(int, int) next = (guard.Item1 + direction.Item1, guard.Item2 + direction.Item2);
							if (next.Item1 < 0 || next.Item2 < 0 || next.Item1 >= xSize || next.Item2 >= ySize)
								break;
							if (map[next.Item1, next.Item2] == '#')
							{
								if (direction.Item1 == -1)//up
									direction = (0, 1);
								else if (direction.Item1 == 1)//down
									direction = (0, -1);
								else if (direction.Item2 == -1)//left
									direction = (-1, 0);
								else if (direction.Item2 == 1)//right
									direction = (+1, 0);
							}
							else
							{
								guard = next;
							}
							loopCount++;
						}

						if (loopCount >= 10000)
							output++;

						map[x, y] = '.';
					}
				}
			}
			return output.ToString();
		}

		private static void Parse(string[] input, out char[,] map, out bool[,] visited, out (int, int) guardStarting)
		{
			int xSize = input.Length;
			int ySize = input[0].Length;
			map = new char[xSize, ySize];
			visited = new bool[xSize, ySize];
			guardStarting = (0, 0);
			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					map[x, y] = input[x][y];
					if (map[x, y] == '^')
					{
						map[x, y] = '.';
						guardStarting = (x, y);
						visited[x, y] = true;
					}
				}
			}
		}
	}
}
