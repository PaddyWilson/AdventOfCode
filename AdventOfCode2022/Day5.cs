﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC
{
	public class Day5 : BaseDay
	{
		public Day5()
		{
			Day = "5";
			Answer1 = "WCZTHTMPS";
			Answer2 = "BLSGJSDTS";
		}

		protected override string Solution1(string[] input)
		{
			//List<Stack<char>> stack = GetStack(input);
			List<List<char>> stack = GetList(input);
			List<(int, int, int)> moves = GetMoves(input);

			//make the moves
			//List<char> temp = new List<char>(input.Length*10);
			
			foreach (var move in moves)
			{
				int amount = move.Item1;
				int from = move.Item2;
				int to = move.Item3;

				var temp = stack[from - 1].GetRange(stack[from - 1].Count - amount, amount);
				temp.Reverse();

				stack[to - 1].AddRange(temp);
				stack[from - 1].RemoveRange(stack[from - 1].Count - amount, amount);

				//for (int i = 0; i < amount; i++)
				//	stack[to - 1].Push(stack[from - 1].Pop());
			}

			string output = "";
			foreach (var item in stack)
				output += item.Last();

			//foreach (var item in stack)
			//	output += item.Peek();

			return output;
		}



		protected override string Solution2(string[] input)
		{
			//List<Stack<char>> stack = GetStack(input);
			List<List<char>> stack = GetList(input);
			List<(int, int, int)> moves = GetMoves(input);

			//make the moves
			foreach (var move in moves)
			{
				int amount = move.Item1;
				int from = move.Item2;
				int to = move.Item3;

				//takes <10s for the small big input
				stack[to - 1].AddRange(stack[from - 1].GetRange(stack[from - 1].Count - amount, amount));
				stack[from - 1].RemoveRange(stack[from - 1].Count -  amount, amount);

				//takes about 1h:50m for the small big input
				//Stack<char> temp = new Stack<char>();

				//for (int i = 0; i < amount; i++)
				//{
				//	temp.Push(stack[from - 1].Pop());
				//}

				//for (int i = 0; i < amount; i++)
				//{
				//	stack[to - 1].Push(temp.Pop());
				//}
			}

			string output = "";

			foreach (var item in stack)
				output += item.Last();

			//foreach (var item in stack)
			//	output += item.Peek();
			return output;
		}

		private List<List<char>> GetList(string[] input)
		{
			List<List<char>> stack = new List<List<char>>();
			List<string> tempList = new List<string>();

			foreach (var item in input)
			{
				if (item == "")
					break;
				tempList.Add(item);
			}

			//parse stack data into usable format
			for (int j = 0; j < tempList[0].Length; j++)
			{
				//on space
				if (tempList[tempList.Count - 1][j] == ' ')
					continue;

				int stackIndex = int.Parse(tempList[tempList.Count - 1][j].ToString()) - 1;

				//on number
				stack.Add(new List<char>());
				for (int i = tempList.Count - 2; i >= 0; i--)
				{
					char tChar = tempList[i][j];
					if (tChar != ' ')
						stack[stackIndex].Add(tChar);
				}
			}
			return stack;
		}

		private List<(int, int, int)> GetMoves(string[] input)
		{
			List<(int, int, int)> movesList = new List<(int, int, int)>();

			bool moves = false;
			foreach (var item in input)
			{
				if (item == "")
				{
					moves = true;
				}
				else if (moves)
				{
					string[] temp = item.Split(' ');
					movesList.Add((int.Parse(temp[1]), int.Parse(temp[3]), int.Parse(temp[5])));
				}
			}

			return movesList;
		}
	}
}
