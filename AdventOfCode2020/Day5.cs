using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day5 : BaseDay
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "947";
			Answer2 = "636";
		}

		protected override string Solution1(string[] input)
		{
			char[,] seats = new char[128, 8];

			//testing
			int x = 0;
			int y = 0;
			SeatRow(seats, 0, 127, "BFFFBBFRRR", 0, ref x, ref y);
			Console.WriteLine(x + ":" + y + " = " + ((x * 8) + y));
			SeatRow(seats, 0, 127, "FFFBBBFRRR", 0, ref x, ref y);
			Console.WriteLine(x + ":" + y + " = " + ((x * 8) + y));
			SeatRow(seats, 0, 127, "BBFFBBFRLL", 0, ref x, ref y);
			Console.WriteLine(x + ":" + y + " = " + ((x * 8) + y));


			int highest = 0;
			foreach (var ticket in input)
			{
				int rowL = 0;
				int rowH = 127;

				int colL = 0;
				int colH = 7;
				SeatRow(seats, 0, 127, ticket, 0, ref x, ref y);
			}

			for (x = 0; x < 128; x++)
			{
				for (y = 0; y < 8; y++)
				{
					//Console.BackgroundColor = ConsoleColor.Black;
					if (seats[x, y] == '#')
					{
						//Console.BackgroundColor = ConsoleColor.Green;
						int temp = (x * 8) + y;
						if (temp > highest)
							highest = temp;
					}
					//Console.Write(seats[x, y]);
				}
				//Console.WriteLine();
			}

			return highest.ToString();
		}

		private bool SeatRow(char[,] seats, int min, int max, string input, int inputIndex, ref int x, ref int y)
		{
			// find the diff then add or subtract it from the min or max

			char letter = input[inputIndex];
			int diff = ((max - min + 1) / 2);
			if (letter == 'F')
				max -= diff;
			else if (letter == 'B')
				min += diff;
			else
				Console.WriteLine("NO!!");

			if (diff == 1 && letter == 'F')
				return SeatCol(seats, max, 0, 7, input, inputIndex + 1, ref x, ref y);//determine seats
			if (diff == 1 && letter == 'B')
				return SeatCol(seats, min, 0, 7, input, inputIndex + 1, ref x, ref y);//determine seats

			return SeatRow(seats, min, max, input, inputIndex + 1, ref x, ref y);
		}

		private bool SeatCol(char[,] seats, int row, int min, int max, string input, int inputIndex, ref int x, ref int y)
		{
			// find the diff then add or subtract it from the min or max

			char letter = input[inputIndex];
			int diff = (max - min + 1) / 2;
			if (letter == 'L')
				max -= diff;
			else if (letter == 'R')
				min += diff;
			else
				Console.WriteLine("NO!!");

			if (diff == 1)
			{
				seats[row, min] = '#';
				x = row;
				y = min;
				return true;//determine seats
			}

			return SeatCol(seats, row, min, max, input, inputIndex + 1, ref x, ref y);
		}

		protected override string Solution2(string[] input)
		{
			char[,] seats = new char[128, 8];

			//testing
			int x = 0;
			int y = 0;

			int highest = 0;
			foreach (var ticket in input)
			{
				SeatRow(seats, 0, 127, ticket, 0, ref x, ref y);
			}

			//Console.WriteLine();
			for (x = 6; x < 100; x++)
			{
				//Console.BackgroundColor = ConsoleColor.Black;
				for (y = 0; y < 8; y++)
				{
					//Console.BackgroundColor = ConsoleColor.Black;
					if (seats[x, y] != '#')
					{
						//Console.BackgroundColor = ConsoleColor.Green;
						int temp = (x * 8) + y;
						if (temp > highest)
							highest = temp;
					}
					//Console.Write(seats[x, y]);
				}
				//Console.WriteLine();
			}
			//Console.BackgroundColor = ConsoleColor.Black;
			return highest.ToString();
		}
	}
}
