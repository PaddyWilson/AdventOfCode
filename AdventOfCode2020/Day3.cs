using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AOC;

namespace AdventOfCode2020
{
	public class Day3 : BaseDay
	{
		public Day3()
		{
			Day = "3";
			Answer1 = "223";
			Answer2 = "3517401300";
		}

		protected override string Solution1(string[] input)
		{
			char[,] hill = new char[input[0].Length, input.Length];

			for (int y = 0; y < input.Length; y++)
				for (int x = 0; x < input[0].Length; x++)
					hill[x, y] = input[y][x];

			int treeCount = CountTreesOnHill(hill, input[0].Length, input.Length, 1, 3);

			return treeCount.ToString();
		}



		protected override string Solution2(string[] input)
		{
			char[,] hill = new char[input[0].Length, input.Length];

			for (int y = 0; y < input.Length; y++)
				for (int x = 0; x < input[0].Length; x++)
					hill[x, y] = input[y][x];

			long treeCount = CountTreesOnHill(hill, input[0].Length, input.Length, 1, 1);
			treeCount *= CountTreesOnHill(hill, input[0].Length, input.Length, 1, 3);
			treeCount *= CountTreesOnHill(hill, input[0].Length, input.Length, 1, 5);
			treeCount *= CountTreesOnHill(hill, input[0].Length, input.Length, 1, 7);
			treeCount *= CountTreesOnHill(hill, input[0].Length, input.Length, 2, 1);

			return treeCount.ToString();
		}

		private int CountTreesOnHill(char[,] hill, int width, int height, int down, int right)
		{
			int treeCount = 0;
			int x = 0;
			for (int y = down; y < height; y += down)
			{
				x += right;
				if (x >= width)
					x -= width;

				//if (right != 3)
				//{
				//	for (int y2 = 0; y2 < 50; y2++)
				//	{
				//		for (int i = 0; i < width; i++)
				//		{
				//			Console.BackgroundColor = ConsoleColor.Black;
				//			if (i == x && y == y2)
				//				Console.BackgroundColor = ConsoleColor.Red;

				//			Console.Write(hill[i, y2]);
				//		}
				//		Console.WriteLine();
				//	}
				//	Console.WriteLine();
				//}

				if (hill[x, y] == '#')
					treeCount++;
			}
			return treeCount;
		}
	}
}
