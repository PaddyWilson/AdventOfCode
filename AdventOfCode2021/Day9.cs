using AOC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2021
{
	public class Day9 : BaseDay
	{
		public Day9()
		{
			Day = "9";
			Answer1 = "528";
			Answer2 = "920448";
		}

		protected override string Solution1(string[] input)
		{
			int[,] grid = new int[input[0].Length, input.Length];

			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					grid[x, y] = int.Parse(input[y][x].ToString());
				}
			}

			int count = 0;

			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					int center = grid[x, y];
					int top = int.MaxValue;
					int bottom = int.MaxValue;
					int left = int.MaxValue;
					int right = int.MaxValue;

					try { top = grid[x, y - 1]; } catch (Exception) { }
					try { bottom = grid[x, y + 1]; } catch (Exception) { }
					try { left = grid[x - 1, y]; } catch (Exception) { }
					try { right = grid[x + 1, y]; } catch (Exception) { }

					if (top > center && bottom > center && right > center && left > center)
						count += center + 1;
				}
			}

			return count.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int[,] grid = new int[input[0].Length, input.Length];
			bool[,] basin = new bool[input[0].Length, input.Length];

			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					grid[x, y] = int.Parse(input[y][x].ToString());
					if (grid[x, y] == 9)
						basin[x, y] = true;
				}
			}

			int offset = 9;

			int nextBasin = offset;
			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					if (!basin[x, y])
					{
						nextBasin++;
						MapBasin(x, y, grid, basin, nextBasin, input[0].Length, input.Length);
					}
				}
			}


			//find the area of each basin
			int[] counts = new int[nextBasin + 1];
			for (int y = 0; y < input.Length; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					if (grid[x, y] - offset != 0)
					{
						counts[grid[x, y] - offset]++;
					}
				}
			}

			List<int> sorted = new List<int>(counts);
			sorted.Sort();
			sorted.Reverse();
			//find 3 basins with the highest area
			return (sorted[0] * sorted[1] * sorted[2]).ToString();
		}

		private void MapBasin(int x, int y, int[,] grid, bool[,] basin, int basinNumber, int xMax, int yMax)
		{
			if (basin[x, y])
				return;

			basin[x, y] = true;
			grid[x, y] = basinNumber;

			//up
			if ((y - 1) >= 0 && !basin[x, y - 1])
				MapBasin(x, y - 1, grid, basin, basinNumber, xMax, yMax);
			//down
			if ((y + 1) < yMax && !basin[x, y + 1])
				MapBasin(x, y + 1, grid, basin, basinNumber, xMax, yMax);
			//left
			if ((x - 1) >= 0 && !basin[x - 1, y])
				MapBasin(x - 1, y, grid, basin, basinNumber, xMax, yMax);
			//right
			if ((x + 1) < xMax && !basin[x + 1, y])
				MapBasin(x + 1, y, grid, basin, basinNumber, xMax, yMax);
		}

	}
}
