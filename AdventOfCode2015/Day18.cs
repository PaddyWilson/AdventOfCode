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
			int gridSize = 100;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length;
				stepCount = 4;
			}

			CellularAutomaton<bool> game = new CellularAutomaton<bool>(gridSize);
			//gameOfLife.ParseInput(input);
			Parse(input, game);
			
			game.possibleItems = new List<bool> { true, false };

			game.ProcessCoord += OnStep;

			game.Step(stepCount);

			//count on lights
			return game.CountItems()[true].ToString();
		}

		protected override string Solution2(string[] input)
		{
			int gridSize = 100;
			int stepCount = 100;

			//TESTNG
			if (input.Length < gridSize - 20)
			{
				gridSize = input.Length;
				stepCount = 5;
			}

			CellularAutomaton<bool> game = new CellularAutomaton<bool>(gridSize);
			//game.ParseInput(input);

			Parse(input, game);

			game.possibleItems = new List<bool> { true, false };

			game.ProcessCoord += OnStep;			
			game.ProcessStepAfter += SetCornerLights;

			//set initial board state
			game.board[0, 0] = true;
			game.board[0, game.ySize - 1] = true;
			game.board[game.xSize - 1, 0] = true;
			game.board[game.xSize - 1, game.ySize - 1] = true;

			game.Step(stepCount);

			//count on lights
			return game.CountItems()[true].ToString();
		}

		private void OnStep(Dictionary<bool, int> surrounded, int x, int y, CellularAutomaton<bool> game)
		{
			bool light = game.board[x, y];
			if (light && surrounded[true] == 2)
				game.boardTemp[x, y] = true;//stay on = on
			else if (light && surrounded[true] == 3)
				game.boardTemp[x, y] = true;//stay on = on
			else if (!light && surrounded[true] == 3)
				game.boardTemp[x, y] = true;//off = on
			else
				game.boardTemp[x, y] = false;
		}

		private void SetCornerLights(CellularAutomaton<bool> game)
		{
			game.boardTemp[0, 0] = true;
			game.boardTemp[0, game.ySize - 1] = true;
			game.boardTemp[game.xSize - 1, 0] = true;
			game.boardTemp[game.xSize - 1, game.ySize - 1] = true;
		}

		private void Parse(string[] input, CellularAutomaton<bool> game)
		{
			int x = 0;
			int y = 0;
			foreach (var item in input)
			{
				foreach (var c in item)
				{
					if (c == '#')
						game.board[x, y] = true;
					else
						game.board[x, y] = false;
					y++;
				}
				x++;
				y = 0;
			}
		}
	}
}
