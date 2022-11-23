using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOC
{
	public class CellularAutomaton<T>
	{
		public int xSize, ySize;

		//maybe use dictionary so out of bounds is easier
		public T[,] board { get; set; }
		public T[,] boardTemp { get; set; }

		//list of possible values that can be in a dictionary
		public List<T> possibleItems = null;

		public CellularAutomaton(int xSize, int ySize)
		{
			this.xSize = xSize;
			this.ySize = ySize;
			board = new T[xSize, ySize];
			boardTemp = new T[xSize, ySize];
		}

		public CellularAutomaton(int size)
		{
			this.xSize = size;
			this.ySize = size;
			board = new T[xSize, ySize];
			boardTemp = new T[xSize, ySize];
		}

		public delegate void ProcessStepCoord(Dictionary<T, int> surrounded, int x, int y, CellularAutomaton<T> gameOfLife);
		public event ProcessStepCoord ProcessCoord;
		protected void OnProcessCoord(Dictionary<T, int> surrounded, int x, int y, CellularAutomaton<T> gameOfLife)
		{
			if (ProcessCoord != null)
				ProcessCoord(surrounded, x, y, gameOfLife);
		}

		public delegate void ProcessAfterStep(CellularAutomaton<T> gameOfLife);
		public event ProcessAfterStep ProcessStepAfter;
		protected void OnProcessStepAfter(CellularAutomaton<T> gameOfLife)
		{
			if (ProcessStepAfter != null)
				ProcessStepAfter(gameOfLife);
		}

		public delegate void ProcessBeforeStep(CellularAutomaton<T> gameOfLife);
		public event ProcessBeforeStep ProcessStepBefore;
		protected void OnProcessStepBefore(CellularAutomaton<T> gameOfLife)
		{
			if (ProcessStepBefore != null)
				ProcessStepBefore(gameOfLife);
		}

		public void Step()
		{
			OnProcessStepBefore(this);

			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					Dictionary<T, int> surrounded = CountSurrounded(x, y);

					//make a delegate for events
					OnProcessCoord(surrounded, x, y, this);
				}
			}

			//make a delegate for callbacks
			OnProcessStepAfter(this);
			SwapBuffer();
		}

		public void Step(int steps, bool print = false)
		{
			for (int step = 0; step < steps; step++)
			{
				Step();

				if (print)
				{
					Print();
				}
			}
		}

		public Dictionary<T, int> CountSurrounded(int x, int y, bool includeSelf = false)
		{
			Dictionary<T, int> output = CreateDictionary();

			//top
			for (int i = -1; i < 2; i++)
			{
				//oob
				if (x + i < 0 || y - 1 < 0 || x + i >= xSize)
					continue;

				if (!output.ContainsKey(board[x + i, y - 1]))
					output.Add(board[x + i, y - 1], 0);

				output[board[x + i, y - 1]] += 1;
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

				if (!output.ContainsKey(board[x + i, y]))
					output.Add(board[x + i, y], 0);

				output[board[x + i, y]] += 1;
			}

			//bottom
			for (int i = -1; i < 2; i++)
			{
				//oob
				if (x + i < 0 || y + 1 >= ySize || x + i >= xSize)
					continue;

				if (!output.ContainsKey(board[x + i, y + 1]))
					output.Add(board[x + i, y + 1], 0);

				output[board[x + i, y + 1]] += 1;
			}
			return output;
		}

		public bool Inbounds(int x, int y)
		{
			return (x >= 0 && x < xSize && y >= 0 && y < ySize);
		}

		public void SwapBuffer()
		{
			Array.Copy(boardTemp, board, xSize * ySize);
		}

		public Dictionary<T, int> CountItems(bool match = true)
		{
			Dictionary<T, int> output = CreateDictionary();
			for (int x = 0; x < xSize; x++)
			{
				for (int y = 0; y < ySize; y++)
				{
					if (!output.ContainsKey(board[x, y]))
						output.Add(board[x, y], 0);

					output[board[x, y]] += 1;
				}
			}
			return output;
		}

		//public void ParseInput(string[] input)
		//{
		//	int x = 0;
		//	int y = 0;
		//	foreach (var item in input)
		//	{
		//		foreach (var c in item)
		//		{
		//			if (c == trueChar)
		//				board[x, y] = true;
		//			else
		//				board[x, y] = false;
		//			y++;
		//		}
		//		x++;
		//		y = 0;
		//	}
		//}

		private Dictionary<T, int> CreateDictionary()
		{
			Dictionary<T, int> output = new Dictionary<T, int>();

			if (possibleItems != null)
			{
				foreach (T item in possibleItems)
				{
					output.Add(item, 0);
				}
			}
			return output;
		}

		public void Print()
		{
			Helpers.PrintMatrix(board, xSize, "");
		}
	}
}
