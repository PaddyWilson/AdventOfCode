using AOC;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
	public class Day4 : BaseDay
	{
		public Day4()
		{
			Day = "4";
			Answer1 = "67716";
			Answer2 = "1830";
		}

		protected override string Solution1(string[] input)
		{
			List<int> numbersToCall = new List<int>();

			foreach (var item in input[0].Split(','))
				numbersToCall.Add(int.Parse(item));

			List<int[,]> boards = new List<int[,]>();
			List<bool[,]> called = new List<bool[,]>();

			//parse input
			int newBoardIndex = 0;
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == "")
				{
					boards.Add(new int[5, 5]);
					called.Add(new bool[5, 5]);
					newBoardIndex = 0;
				}
				else
				{
					List<string> items = new List<string>(input[i].Split(' '));
					//remove empty items
					for (int j = items.Count - 1; j >= 0; j--)
						if (items[j] == "")
							items.RemoveAt(j);

					for (int j = 0; j < items.Count; j++)
						boards[boards.Count - 1][newBoardIndex, j] = int.Parse(items[j]);

					newBoardIndex++;
				}
			}

			//play game
			foreach (var calledNumber in numbersToCall)
			{
				//pay a round
				for (int i = 0; i < boards.Count; i++)
					searchBoard(boards[i], called[i], calledNumber);

				//look for winning board
				for (int i = 0; i < boards.Count; i++)
				{
					if (checkWin(called[i]))
						return (sumUnmarked(boards[i], called[i]) * calledNumber).ToString();
				}
			}

			return "error";
		}

		protected override string Solution2(string[] input)
		{
			List<int> numbersToCall = new List<int>();

			foreach (var item in input[0].Split(','))
				numbersToCall.Add(int.Parse(item));

			List<int[,]> boards = new List<int[,]>();
			List<bool[,]> called = new List<bool[,]>();

			//parse input
			int newBoardIndex = 0;
			for (int i = 1; i < input.Length; i++)
			{
				if (input[i] == "")
				{
					boards.Add(new int[5, 5]);
					called.Add(new bool[5, 5]);
					newBoardIndex = 0;
				}
				else
				{
					List<string> items = new List<string>(input[i].Split(' '));
					//remove empty items
					for (int j = items.Count - 1; j >= 0; j--)
						if (items[j] == "")
							items.RemoveAt(j);

					for (int j = 0; j < items.Count; j++)
						boards[boards.Count - 1][newBoardIndex, j] = int.Parse(items[j]);

					newBoardIndex++;
				}
			}

			foreach (var calledNumber in numbersToCall)
			{
				//pay a round
				for (int i = 0; i < boards.Count; i++)
					searchBoard(boards[i], called[i], calledNumber);

				//look for winning board
				for (int i = boards.Count - 1; i >= 0; i--)
				{
					bool removeBoard = false;

					if (checkWin(called[i]))
						removeBoard = true;

					if (boards.Count == 1 && removeBoard)
						return (sumUnmarked(boards[i], called[i]) * calledNumber).ToString();

					if (removeBoard && boards.Count > 1)
					{
						boards.RemoveAt(i);
						called.RemoveAt(i);
					}
				}
			}

			return "error";
		}

		private void searchBoard(int[,] boardNumbers, bool[,] boardedCalled, int calledNumber)
		{
			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					if (boardNumbers[x, y] == calledNumber)
						boardedCalled[x, y] = true;
				}
			}
		}

		private int sumUnmarked(int[,] boardNumbers, bool[,] boardedCalled)
		{
			int sum = 0;
			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					if (!boardedCalled[x, y])
						sum += boardNumbers[x, y];
				}
			}
			return sum;
		}

		private bool checkWin(bool[,] boardedCalled)
		{
			//check rows
			for (int y = 0; y < 5; y++)
			{
				int trueCount = 0;
				for (int x = 0; x < 5; x++)
				{
					if (boardedCalled[x, y])
						trueCount++;
					else
						break;
				}

				if (trueCount == 5)
					return true;
			}

			//check col
			for (int x = 0; x < 5; x++)
			{
				int trueCount = 0;
				for (int y = 0; y < 5; y++)
				{
					if (boardedCalled[x, y])
						trueCount++;
					else
						break;
				}

				if (trueCount == 5)
					return true;
			}

			return false;
		}
	}
}
