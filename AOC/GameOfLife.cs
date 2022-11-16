using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOC
{
	public class GameOfLife
	{
		public int xSize, ySize;

		//maybe use dictionary so out of bounds is easier
		public bool[,] board { get; set; }
		public bool[,] boardTemp { get; set; }

		public GameOfLife(int xSize, int ySize)
		{
			this.xSize = xSize;
			this.ySize = ySize;
			board = new bool[xSize, ySize];
			boardTemp = new bool[xSize, ySize];
		}

		public GameOfLife(int size)
		{
			this.xSize = size;
			this.ySize = size;
			board = new bool[xSize, ySize];
			boardTemp = new bool[xSize, ySize];
		}

		public void Step()
		{
			//depends on defind rules
			//different for each puzzle
		}

		/// <summary>
		/// Counts surrrounding true's and false's 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="includeSelf"></param>
		/// <returns>(int true count, int false conut)</returns>
		public (int, int) CountSurrounded(int x, int y, bool includeSelf = false)
		{
			int on = 0;
			int off = 0;

			//top
			for (int i = -1; i < 2; i++)
			{
				//oob
				if (x + i < 0 || y - 1 < 0 || x + i >= xSize)
					continue;

				if (board[x + i, y - 1])
					on++;
				else
					off++;
			}

			//center
			for (int i = -1; i < 2; i++)
			{
				if (!includeSelf)
				{
					//dont count self
					if (x + i == x)
						continue;
				}
				//oob
				if (x + i < 0 || x + i >= xSize)
					continue;

				if (board[x + i, y])
					on++;
				else
					off++;
			}

			//bottom
			for (int i = -1; i < 2; i++)
			{
				//oob
				if (x + i < 0 || y + 1 >= ySize || x + i >= xSize)
					continue;

				if (board[x + i, y + 1])
					on++;
				else
					off++;
			}
			return (on, off);
		}

		public void SwapBuffer()
		{
			Array.Copy(boardTemp, board, xSize * ySize);
		}

		/// <summary>
		/// Counts the amount of true's on the board
		/// </summary>
		/// <param name="match">Put false if you want to match false</param>
		/// <returns></returns>
		public int CountTrue(bool match = true)
		{
			int count = 0;
			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					if (board[x, y] == match)
						count++;
				}
			}
			return count;
		}

		public void ParseInput(string[] input, char trueChar = '#')
		{
			int x = 0;
			int y = 0;
			foreach (var item in input)
			{
				foreach (var c in item)
				{
					if (c == trueChar)
						board[x, y] = true;
					else
						board[x, y] = false;
					y++;
				}
				x++;
				y = 0;
			}
		}

		public void Print()
		{
			Helpers.PrintMatrix(board, xSize, "");
		}
	}
}
