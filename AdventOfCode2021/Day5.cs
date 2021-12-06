using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day5 : BaseDay
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "7269";
			Answer2 = "21140";
		}

		protected override string Solution1(string[] input)
		{
			List<(int, int, int, int)> coord = new List<(int, int, int, int)>();

			int gridSize = 1000;

			//for debug/test input
			//if (input.Length < 200)
			//gridSize = 10;

			foreach (var item in input)
			{
				string[] temp = item.Split(" -> ");

				int x1 = int.Parse(temp[0].Split(',')[0]);
				int y1 = int.Parse(temp[0].Split(',')[1]);
				int x2 = int.Parse(temp[1].Split(',')[0]);
				int y2 = int.Parse(temp[1].Split(',')[1]);

				coord.Add((x1, y1, x2, y2));
			}

			int[,] grid = new int[gridSize, gridSize];

			foreach (var item in coord)
			{
				int x1 = item.Item1;
				int y1 = item.Item2;
				int x2 = item.Item3;
				int y2 = item.Item4;

				if (x1 == x2)
				{
					//Console.WriteLine("Line x");
					if (y1 < y2)
					{
						//Console.WriteLine("y++ " + item);
						for (int y = y1; y <= y2; y++)
							grid[x1, y]++;
					}
					else
					{
						//Console.WriteLine("y-- " + item);
						for (int y = y1; y >= y2; y--)
							grid[x1, y]++;
					}
					//if (input.Length < 200)
					//PrintGrid(grid, gridSize);
				}
				else if (y1 == y2)
				{
					//Console.WriteLine("Line y");
					if (x1 < x2)
					{
						//Console.WriteLine("x++ " + item);
						for (int x = x1; x <= x2; x++)
							grid[x, y1]++;
					}
					else
					{
						//Console.WriteLine("x++ " + item);
						for (int x = x1; x >= x2; x--)
							grid[x, y1]++;
					}
					//if (input.Length < 200)
					//PrintGrid(grid, gridSize);
				}

			}

			int count = 0;

			for (int y = 0; y < gridSize; y++)
			{
				for (int x = 0; x < gridSize; x++)
				{
					if (grid[x, y] > 1)
						count++;
				}
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<(int, int, int, int)> coord = new List<(int, int, int, int)>();

			int gridSize = 1000;

			//for debug/test input
			//if (input.Length < 200)
				//gridSize = 10;

			foreach (var item in input)
			{
				string[] temp = item.Split(" -> ");

				int x1 = int.Parse(temp[0].Split(',')[0]);
				int y1 = int.Parse(temp[0].Split(',')[1]);
				int x2 = int.Parse(temp[1].Split(',')[0]);
				int y2 = int.Parse(temp[1].Split(',')[1]);

				coord.Add((x1, y1, x2, y2));
			}

			int[,] grid = new int[gridSize, gridSize];

			foreach (var item in coord)
			{
				int x1 = item.Item1;
				int y1 = item.Item2;
				int x2 = item.Item3;
				int y2 = item.Item4;

				if (x1 == x2)
				{
					if (y1 < y2)
					{
						for (int y = y1; y <= y2; y++)
							grid[x1, y]++;
					}
					else
					{
						for (int y = y1; y >= y2; y--)
							grid[x1, y]++;
					}
				}
				else if (y1 == y2)
				{
					if (x1 < x2)
					{
						for (int x = x1; x <= x2; x++)
							grid[x, y1]++;
					}
					else
					{
						for (int x = x1; x >= x2; x--)
							grid[x, y1]++;
					}
				}
				else
				{
					int x = -1;
					int y = -1;
					bool first = true;
					while (x != x2 && y != y2)
					{
						if (first)
						{
							x = x1;
							y = y1;
							first = false;
						}
						else
						{
							if (x1 <= x2)
								x++;
							else
								x--;

							if (y1 <= y2)
								y++;
							else
								y--;
						}
						grid[x, y]++;
					}
				}
			}

			//if (input.Length < 200)
				//PrintGrid(grid, gridSize);

			int count = 0;

			for (int y = 0; y < gridSize; y++)
			{
				for (int x = 0; x < gridSize; x++)
				{
					if (grid[x, y] > 1)
						count++;
				}
			}

			return count.ToString();
		}

		private void PrintGrid(int[,] grid, int size)
		{
			Console.WriteLine();
			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					Console.Write(grid[x, y]);
				}
				Console.WriteLine();
			}
		}
	}
}
