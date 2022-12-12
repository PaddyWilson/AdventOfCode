using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Day10 : BaseDay
	{
		public Day10()
		{
			Day = "10";
			Answer1 = "14420";
			Answer2 = "RGLRBZAU";
		}

		protected override string Solution1(string[] input)
		{
			int cycle = 1;
			int X = 1;
			int signalStrength = 0;
			next = 20;
			foreach (var item in input)
			{
				var temp = item.Split(' ');
				if (temp[0] == "noop")
				{
					signalStrength += GetSignalStrength(cycle, X);
					cycle++;

				}
				else if (temp[0] == "addx")
				{
					signalStrength += GetSignalStrength(cycle, X);
					cycle++;


					signalStrength += GetSignalStrength(cycle, X);
					X += int.Parse(temp[1]);
					cycle++;
				}
			}

			return signalStrength.ToString();
		}

		protected override string Solution2(string[] input)
		{
			int cycle = 1;
			int X = 1;
			int signalStrength = 0;
			next = 40;
			pixelX = 0;
			pixelY = 0;

			char[,] grid = new char[6, 40];

			foreach (var item in input)
			{
				var temp = item.Split(' ');
				if (temp[0] == "noop")
				{
					DrawOnCRT(cycle, X, ref grid);
					cycle++;

				}
				else if (temp[0] == "addx")
				{
					DrawOnCRT(cycle, X, ref grid);
					cycle++;


					DrawOnCRT(cycle, X, ref grid);
					X += int.Parse(temp[1]);
					cycle++;
				}
			}
			//prints out the answer
			Console.WriteLine();
			Helpers.PrintMatrix(grid, 6, 40);

			return signalStrength.ToString();
		}

		private int pixelX = 0;
		private int pixelY = 0;
		private void DrawOnCRT(int cycle, int X, ref char[,] grid)
		{
			if (cycle > next)
			{
				//move to next row
				pixelX++;
				pixelY = 0;
				next += 40;
			}

			if (pixelY >= X - 1 && pixelY <= X + 1)
			{
				grid[pixelX, pixelY] = '#';
			}
			else
			{
				grid[pixelX, pixelY] = ' ';
			}

			pixelY++;
		}


		private int next = 20;
		private int GetSignalStrength(int cycle, int X)
		{
			if (cycle != next)
				return 0;

			next += 40;
			return cycle * X;
		}
	}
}
