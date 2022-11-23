using AOC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			CellularAutomaton<char> seats = new CellularAutomaton<char>(input.Length, input[0].Length);

			//parse input
			for (int x = 0; x < seats.xSize; x++)
				for (int y = 0; y < seats.ySize; y++)
					seats.board[x, y] = input[x][y];

			seats.possibleItems = new List<char> { 'L', '.', '#' };
			seats.ProcessCoord += Seats_ProcessCoord;

			while (true)
			{
				char[,] matchingTest = new char[seats.xSize, seats.ySize];
				//copy seats for later matching
				Array.Copy(seats.board, matchingTest, seats.board.Length);

				seats.Step();

				//check it the arrays match
				if (Helpers.ArrayMatch(seats.board, matchingTest, seats.xSize, seats.ySize))
					break;
			}

			//count occupied seats
			return seats.CountItems()['#'].ToString();
		}

		private void Seats_ProcessCoord(Dictionary<char, int> surrounded, int x, int y, CellularAutomaton<char> ca)
		{
			if (ca.board[x, y] == 'L' && surrounded['#'] == 0)
				ca.boardTemp[x, y] = '#';
			else if (ca.board[x, y] == '#' && surrounded['#'] >= 4)
				ca.boardTemp[x, y] = 'L';
			else
				ca.boardTemp[x, y] = ca.board[x, y];
		}

		protected override string Solution2(string[] input)
		{
			CellularAutomaton<char> seats = new CellularAutomaton<char>(input.Length, input[0].Length);

			////parse input
			for (int x = 0; x < seats.xSize; x++)
				for (int y = 0; y < seats.ySize; y++)
					seats.board[x, y] = input[x][y];

			seats.possibleItems = new List<char> { 'L', '.', '#' };
			seats.ProcessCoord += Seats_ProcessCoord2;

			while (true)
			{
				char[,] matchingTest = new char[seats.xSize, seats.ySize];
				//copy seats for later matching
				Array.Copy(seats.board, matchingTest, seats.board.Length);

				seats.Step();
				//check it the arrays match
				if (Helpers.ArrayMatch(seats.board, matchingTest, seats.xSize, seats.ySize))
					break;
			}

			//count occupied seats
			return seats.CountItems()['#'].ToString();
		}

		private void Seats_ProcessCoord2(Dictionary<char, int> surrounded, int x, int y, CellularAutomaton<char> ca)
		{
			int taken = 0;
			int empty = 0;

			//up
			for (int i = y - 1; i >= 0; i--)
			{
				if (ca.board[x, i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x, i] == '#')
				{
					taken++;
					break;
				}
			}
			//down
			for (int i = y + 1; i < ca.ySize; i++)
			{
				if (ca.board[x, i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x, i] == '#')
				{
					taken++;
					break;
				}
			}
			//left
			for (int i = x-1; i >= 0; i--)
			{
				if (ca.board[i, y] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[i, y] == '#')
				{
					taken++;
					break;
				}
			}
			//right
			for (int i = x + 1; i < ca.xSize; i++)
			{
				if (ca.board[i, y] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[i, y] == '#')
				{
					taken++;
					break;
				}
			}			
			//left up
			for (int i = 1; i < ca.xSize + ca.ySize; i++)
			{
				if (!ca.Inbounds(x - i, y - i)) break;
				if (ca.board[x - i, y - i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x - i, y - i] == '#')
				{
					taken++;
					break;
				}
			}
			//right up
			for (int i = 1; i < ca.xSize + ca.ySize; i++)
			{
				if (!ca.Inbounds(x - i, y + i)) break;
				if (ca.board[x - i, y + i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x - i, y + i] == '#')
				{
					taken++;
					break;
				}
			}
			//right down
			for (int i = 1; i < ca.xSize + ca.ySize; i++)
			{
				if (!ca.Inbounds(x + i, y + i)) break;
				if (ca.board[x + i, y + i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x + i, y + i] == '#')
				{
					taken++;
					break;
				}
			}
			//left down
			for (int i = 1; i < ca.xSize + ca.ySize; i++)
			{
				if (!ca.Inbounds(x + i, y - i)) break;
				if (ca.board[x + i, y - i] == 'L')
				{
					empty++;
					break;
				}
				if (ca.board[x + i, y - i] == '#')
				{
					taken++;
					break;
				}
			}

			if (ca.board[x, y] == 'L' && taken == 0)
				ca.boardTemp[x, y] = '#';
			else if (ca.board[x, y] == '#' && taken >= 5)
				ca.boardTemp[x, y] = 'L';
			else
				ca.boardTemp[x, y] = ca.board[x, y];
		}
	}
}
