using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day18 : BaseDay
	{
		public Day18()
		{
			Day = "18";
			Answer1 = "768";
			Answer2 = "781";
		}

		protected override string Solution1(string[] input)
		{
			//over size array by 1 radius so out of bounds are easy to handle
			int gridSize = 100;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length;
				stepCount = 4;
			}
			GameOfLife gameOfLife = new GameOfLife(gridSize);
			gameOfLife.ParseInput(input);

			for (int step = 0; step < stepCount; step++)
			{
				for (int x = 0; x < gridSize; x++)
				{
					for (int y = 0; y < gridSize; y++)
					{
						bool light = gameOfLife.board[x, y];
						(int, int) onOff = gameOfLife.CountSurrounded(x, y);

						if (light)
						{
							if (onOff.Item1 == 2 || onOff.Item1 == 3)
								gameOfLife.boardTemp[x, y] = true;//stay on
							else
								gameOfLife.boardTemp[x, y] = false; // turn off
						}
						else if (!light && onOff.Item1 == 3)
						{
							gameOfLife.boardTemp[x, y] = true;
						}
					}
				}

				gameOfLife.SwapBuffer();
			}

			//count on lights
			return gameOfLife.CountTrue().ToString();
		}

		protected override string Solution2(string[] input)
		{
			//over size array by 1 radius so out of bounds are easy to handle
			int gridSize = 100;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length;
				stepCount = 5;
			}

			GameOfLife gameOfLife = new GameOfLife(gridSize);
			gameOfLife.ParseInput(input);

			SetCornerLights(gameOfLife.board, gridSize);

			for (int step = 0; step < stepCount; step++)
			{
				for (int x = 0; x < gridSize; x++)
				{
					for (int y = 0; y < gridSize; y++)
					{
						bool light = gameOfLife.board[x, y];
						(int, int) onOff = gameOfLife.CountSurrounded(x, y);

						if (light && onOff.Item1 == 2)
							gameOfLife.boardTemp[x, y] = true;//stay on = on
						else if (light && onOff.Item1 == 3)
							gameOfLife.boardTemp[x, y] = true;//stay on = on
						else if (!light && onOff.Item1 == 3)
							gameOfLife.boardTemp[x, y] = true;//off = on
						else
							gameOfLife.boardTemp[x, y] = false;
					}
				}
				SetCornerLights(gameOfLife.boardTemp, gridSize);
				gameOfLife.SwapBuffer();
			}

			//count on lights
			return gameOfLife.CountTrue().ToString();
		}

		private void SetCornerLights(bool[,] lights, int gridsize)
		{
			lights[0, 0] = true;
			lights[0, gridsize - 1] = true;
			lights[gridsize - 1, 0] = true;
			lights[gridsize - 1, gridsize - 1] = true;
		}
	}
}
