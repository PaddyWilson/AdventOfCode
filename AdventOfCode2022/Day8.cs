using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOC
{
	//the basic layout of for a new Day
	public class Day8 : BaseDay
	{
		public Day8()
		{
			Day = "8";
			Answer1 = "1818";
			Answer2 = "368368";
		}

		protected override string Solution1(string[] input)
		{
			int[,] trees = new int[input[0].Length, input.Length];

			for (int x = 0; x < input.Length; x++)
				for (int y = 0; y < input[x].Length; y++)
					trees[x, y] = int.Parse(input[x][y].ToString());

			int visibleTress = 0;
			//add the trees at the edge
			visibleTress += (input.Length + input.Length + input[0].Length + input[0].Length - 4);

			for (int x = 1; x < input.Length - 1; x++)
			{
				for (int y = 1; y < input[x].Length - 1; y++)
				{
					if (IsVisible(trees, x, y, input.Length, input[x].Length))
					{ visibleTress += 1; }
				}
			}

			return visibleTress.ToString();
		}

		private bool IsVisible(int[,] trees, int x, int y, int xSize, int ySize)
		{
			//int tree = trees[x, y];

			//up
			for (int i = x - 1; i >= 0; i--)
			{
				//int current = trees[i, y];
				//tree visible at edge
				if (i == 0 && trees[i, y] < trees[x, y])
				{ return true; }
				//tree visable
				else if (i > 0 && trees[i, y] < trees[x, y])
				{ continue; }
				else
				{ break; }

			}//down
			for (int i = x + 1; i <= xSize - 1; i++)
			{
				//int current = trees[i, y];
				//tree visible at edge
				if (i == xSize - 1 && trees[i, y] < trees[x, y])
				{ return true; }
				//tree visable
				else if (i < xSize - 1 && trees[i, y] < trees[x, y])
				{ continue; }
				else
				{ break; }
			}//left
			for (int i = y - 1; i >= 0; i--)
			{
				//int current = trees[x, i];
				//tree visible at edge
				if (i == 0 && trees[x, i] < trees[x, y])
				{ return true; }
				//tree visable
				else if (i > 0 && trees[x, i] < trees[x, y])
				{ continue; }
				else
				{ break; }
			}//right
			for (int i = y + 1; i <= ySize - 1; i++)
			{
				//int current = trees[x, i];
				//tree visible at edge
				if (i == ySize - 1 && trees[x, i] < trees[x, y])
				{ return true; }
				//tree visable
				else if (i < ySize - 1 && trees[x, i] < trees[x, y])
				{ continue; }
				else
				{ break; }
			}

			return false;
		}

		protected override string Solution2(string[] input)
		{
			int[,] trees = new int[input[0].Length, input.Length];

			for (int x = 0; x < input.Length; x++)
				for (int y = 0; y < input[x].Length; y++)
					trees[x, y] = int.Parse(input[x][y].ToString());

			int bestTreeScore = int.MinValue;

			for (int x = 1; x < input.Length - 1; x++)
			{
				for (int y = 1; y < input[x].Length - 1; y++)
				{
					int score = TreeScore(trees, x, y, input.Length, input[x].Length);
					if (score > bestTreeScore)
						bestTreeScore = score;
				}
			}

			return bestTreeScore.ToString();
		}

		private int TreeScore(int[,] trees, int x, int y, int xSize, int ySize)
		{
			int score = 1;
			int visTrees = 0;
			//up
			for (int i = x - 1; i >= 0; i--)
			{
				//tree visable
				if (i > 0 && trees[i, y] < trees[x, y])
				{
					visTrees++;
				}
				else
				{
					visTrees++;
					break;
				}
			}
			score *= visTrees;

			visTrees = 0;
			//down
			for (int i = x + 1; i <= xSize - 1; i++)
			{
				//tree visable
				if (i < xSize - 1 && trees[i, y] < trees[x, y])
				{
					visTrees++;
				}
				else
				{
					visTrees++;
					break;
				}
			}
			score *= visTrees;

			visTrees = 0;
			//left
			for (int i = y - 1; i >= 0; i--)
			{
				//tree visable
				if (i > 0 && trees[x, i] < trees[x, y])
				{
					visTrees++;
				}
				else
				{
					visTrees++;
					break;
				}
			}
			score *= visTrees;

			visTrees = 0;
			//right
			for (int i = y + 1; i <= ySize - 1; i++)
			{
				//tree visable
				if (i < ySize - 1 && trees[x, i] < trees[x, y])
				{
					visTrees++;
				}
				else
				{
					visTrees++;
					break;
				}
			}
			score *= visTrees;

			return score;
		}
	}
}
