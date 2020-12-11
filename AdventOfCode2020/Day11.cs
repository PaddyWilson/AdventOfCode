using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day11 : BaseDay
	{
		public Day11()
		{
			Day = "11";
			Answer1 = "2424";
			Answer2 = "2208";
		}

		protected override string Solution1(string[] input)
		{
			char[,] seats = new char[input.Length + 2, input[0].Length + 2];

			for (int x = 1; x <= input.Length; x++)
				for (int y = 1; y <= input[x - 1].Length; y++)
					seats[x, y] = input[x - 1][y - 1];

			while (true)
			{
				char[,] seatsTemp = new char[input.Length + 2, input[0].Length + 2];
				Array.Copy(seats, seatsTemp, seats.Length);

				SearchSeats1(seats, ref seatsTemp, input.Length, input[0].Length);
				//check it the arrays match
				if (CheckIfSeatsSame(seats, seatsTemp, input.Length, input[0].Length))
				{
					Array.Copy(seatsTemp, seats, seats.Length);
					break;
				}

				Array.Copy(seatsTemp, seats, seats.Length);
				//PrintSeats(seats, input.Length + 2, input[0].Length + 2);
			}

			//count occupied seats
			int output = 0;
			for (int x = 1; x <= input.Length; x++)
				for (int y = 1; y <= input[x - 1].Length; y++)
					if (seats[x, y] == '#')
						output++;

			return output.ToString();
		}

		private bool CheckIfSeatsSame(char[,] seatsInput, char[,] seatsOutput, int width, int height)
		{
			int seatCount = 0;
			int c = 0;
			for (int x = 1; x <= width; x++)
				for (int y = 1; y <= height; y++)
				{
					if (seatsInput[x, y] == seatsOutput[x, y])
						seatCount++;
					c++;
				}

			if (seatCount == c)
				return true;
			return false;
		}

		private void SearchSeats1(char[,] seatsInput, ref char[,] seatsOutput, int width, int height)
		{
			for (int x = 1; x <= width; x++)
			{
				for (int y = 1; y <= height; y++)
				{
					int taken = 0;
					int empty = 0;

					for (int i = -1; i < 2; i++)
					{
						if (seatsInput[x + i, y - 1] == 'L')
							empty++;
						if (seatsInput[x + i, y - 1] == '#')
							taken++;
					}

					for (int i = -1; i < 2; i++)
					{
						if (x + i == x) continue;
						if (seatsInput[x + i, y] == 'L')
							empty++;
						if (seatsInput[x + i, y] == '#')
							taken++;
					}

					for (int i = -1; i < 2; i++)
					{
						if (seatsInput[x + i, y + 1] == 'L')
							empty++;
						if (seatsInput[x + i, y + 1] == '#')
							taken++;
					}

					if (seatsInput[x, y] == 'L' && taken == 0)
						seatsOutput[x, y] = '#';

					else if (seatsInput[x, y] == '#' && taken >= 4)
						seatsOutput[x, y] = 'L';

				}
			}
		}

		private void PrintSeats(char[,] seats, int xV, int yV)
		{
			for (int x = 0; x < xV; x++)
			{
				for (int y = 0; y < yV; y++)
				{
					Console.Write(seats[x, y]);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		protected override string Solution2(string[] input)
		{
			char[,] seats = new char[input.Length + 2, input[0].Length + 2];

			for (int x = 1; x <= input.Length; x++)
				for (int y = 1; y <= input[x - 1].Length; y++)
					seats[x, y] = input[x - 1][y - 1];

			while (true)
			{
				char[,] seatsTemp = new char[input.Length + 2, input[0].Length + 2];
				Array.Copy(seats, seatsTemp, seats.Length);

				SearchSeats2(seats, ref seatsTemp, input.Length, input[0].Length);
				//check it the arrays match
				if (CheckIfSeatsSame(seats, seatsTemp, input.Length, input[0].Length))
				{
					Array.Copy(seatsTemp, seats, seats.Length);
					break;
				}

				Array.Copy(seatsTemp, seats, seats.Length);
				//PrintSeats(seats, input.Length + 2, input[0].Length + 2);
			}

			//count occupied seats
			int output = 0;
			for (int x = 1; x <= input.Length; x++)
				for (int y = 1; y <= input[x - 1].Length; y++)
					if (seats[x, y] == '#')
						output++;

			return output.ToString();
		}

		private void SearchSeats2(char[,] seatsInput, ref char[,] seatsOutput, int width, int height)
		{
			for (int x = 1; x <= width; x++)
			{
				for (int y = 1; y <= height; y++)
				{
					int taken = 0;
					int empty = 0;

					//up
					int count = 1;
					while (true)
					{
						if (y - count == 0) break;
						if (seatsInput[x, y - count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x, y - count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}
					//down
					count = 1;
					while (true)
					{
						if (y + count == height + 1) break;
						if (seatsInput[x, y + count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x, y + count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}

					//left
					count = 1;
					while (true)
					{
						if (x - count == 0) break;
						if (seatsInput[x - count, y] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x - count, y] == '#')
						{
							taken++;
							break;
						}
						count++;
					}
					//right
					count = 1;
					while (true)
					{
						if (x + count == width + 1) break;
						if (seatsInput[x + count, y] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x + count, y] == '#')
						{
							taken++;
							break;
						}
						count++;
					}

					//left up
					count = 1;
					while (true)
					{
						if (x - count == 0 || y - count == 0) break;
						if (seatsInput[x - count, y - count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x - count, y - count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}
					//right up
					count = 1;
					while (true)
					{
						if (x - count == 0 || y + count == height + 1) break;
						if (seatsInput[x - count, y + count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x - count, y + count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}

					//right down
					count = 1;
					while (true)
					{
						if (x + count == width + 1 || y + count == height + 1) break;
						if (seatsInput[x + count, y + count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x + count, y + count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}
					//left down
					count = 1;
					while (true)
					{
						if (x + count == width + 1 || y - count == 0) break;
						if (seatsInput[x + count, y - count] == 'L')
						{
							empty++;
							break;
						}
						if (seatsInput[x + count, y - count] == '#')
						{
							taken++;
							break;
						}
						count++;
					}

					if (seatsInput[x, y] == 'L' && taken == 0)
						seatsOutput[x, y] = '#';
					else if (seatsInput[x, y] == '#' && taken >= 5)
						seatsOutput[x, y] = 'L';

				}
			}
		}
	}
}
