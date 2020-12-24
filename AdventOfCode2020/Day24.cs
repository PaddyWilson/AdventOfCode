using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day24 : BaseDay
	{
		public Day24()
		{
			Day = "24";
			Answer1 = "375";
			Answer2 = "3937";
		}

		private enum GridDir
		{
			East,
			West,
			SouthEast,
			SouthWest,
			NorthEast,
			NorthWest
		}

		private List<List<GridDir>> ParseInput(string[] input)
		{
			List<List<GridDir>> instructions = new List<List<GridDir>>();

			foreach (var item in input)
			{
				List<GridDir> temp = new List<GridDir>();
				for (int i = 0; i < item.Length; i++)
				{
					if (item[i] == 'e')
					{ temp.Add(GridDir.East); }
					else if (item[i] == 'w')
					{ temp.Add(GridDir.West); }
					else if (item[i] == 's')
					{
						i++;
						if (item[i] == 'e')
						{ temp.Add(GridDir.SouthEast); }
						else if (item[i] == 'w')
						{ temp.Add(GridDir.SouthWest); }
					}
					else if (item[i] == 'n')
					{
						i++;
						if (item[i] == 'e')
						{ temp.Add(GridDir.NorthEast); }
						else if (item[i] == 'w')
						{ temp.Add(GridDir.NorthWest); }
					}
				}
				instructions.Add(temp);
			}
			return instructions;
		}

		protected override string Solution1(string[] input)
		{
			List<List<GridDir>> instructions = ParseInput(input);

			int size = 100;
			int offset = size / 2;

			bool[,] grid = new bool[size, size];

			foreach (var instruc in instructions)
			{
				int x = offset;
				int y = offset;

				foreach (var dir in instruc)
				{
					var temp = GetCord(x, y, dir);
					x = temp.Item1;
					y = temp.Item2;
				}

				if (grid[x, y])
					grid[x, y] = false;
				else
					grid[x, y] = true;
			}

			int count = 0;
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					if (grid[x, y])
						count++;
				}
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			List<List<GridDir>> instructions = ParseInput(input);

			int size = 150;
			int offset = size / 2;

			bool[,] grid = new bool[size, size];

			foreach (var instruc in instructions)
			{
				int x = offset;
				int y = offset;

				foreach (var dir in instruc)
				{
					var temp = GetCord(x, y, dir);
					x = temp.Item1;
					y = temp.Item2;
				}

				if (grid[x, y])
					grid[x, y] = false;
				else
					grid[x, y] = true;
			}

			List<GridDir> allDirs = new List<GridDir>()
			{
				GridDir.West,
				GridDir.East,
				GridDir.SouthWest,
				GridDir.SouthEast,
				GridDir.NorthWest,
				GridDir.NorthEast
			};

			bool[,] writeGrid = new bool[size, size];
			for (int day = 0; day < 100; day++)
			{
				Array.Copy(grid, writeGrid, size * size);

				for (int x = 1; x < size - 1; x++)
				{
					for (int y = 1; y < size - 2; y++)
					{
						int black = 0;

						foreach (var item in allDirs)
						{
							var coord = GetCord(x, y, item);
							if (grid[coord.Item1, coord.Item2])
								black++;
						}

						if (grid[x, y] && black == 0)
							writeGrid[x, y] = false;
						else if (grid[x, y] && black > 2)
							writeGrid[x, y] = false;
						else if (!grid[x, y] && black == 2)
							writeGrid[x, y] = true;
					}
				}

				Array.Copy(writeGrid, grid, size * size);
			}

			int count = 0;
			for (int x = 0; x < size; x++)
			{
				for (int y = 0; y < size; y++)
				{
					if (grid[x, y])
						count++;
				}
			}
			return count.ToString();
		}

		private (int, int) GetCord(int x, int y, GridDir dir)
		{
			if (dir == GridDir.East)
			{
				x++;
			}
			else if (dir == GridDir.West)
			{
				x--;
			}
			else if (dir == GridDir.SouthEast)
			{
				y++;
				if (y % 2 == 0)
					x++;
			}
			else if (dir == GridDir.SouthWest)
			{
				y++;
				if (y % 2 == 1)
					x--;
			}
			else if (dir == GridDir.NorthEast)
			{
				y--;
				if (y % 2 == 0)
					x++;
			}
			else if (dir == GridDir.NorthWest)
			{
				y--;
				if (y % 2 == 1)
					x--;
			}
			return (x, y);
		}
	}
}
