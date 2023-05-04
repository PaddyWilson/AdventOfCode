using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC
{
	public class Day12 : BaseDay
	{
		public Day12()
		{
			Day = "12";
			Answer1 = "440";
			Answer2 = "0";
		}

		protected override string Solution1(string[] input)
		{
			char[,] map = new char[input.Length, input[0].Length];

			Point start = new Point(0, 0);
			Point end = new Point();
			Point size = new Point(input.Length, input[0].Length);

			for (int x = 0; x < size.X; x++)
			{
				for (int y = 0; y < size.Y; y++)
				{
					map[x, y] = input[x][y];
					if (input[x][y] == 'S')
					{
						start.X = x;
						start.Y = y;
						//map[x, y] = 'a';
					}
					if (input[x][y] == 'E')
					{
						end.X = x;
						end.Y = y;
						//map[x, y] = (char)(((int)'z') + 1);
					}

				}
			}

			int[,] distance = new int[size.X, size.Y];
			for (int x = 0; x < size.X; x++)
				for (int y = 0; y < size.Y; y++)
					distance[x, y] = -1;

			//distance[start.X, start.Y] = 0;
			//distance[end.X, end.Y] = 27;

			//MapDistance(map, ref distance, size, start, start, map[end.X, end.Y]);

			MapDistance(map, ref distance, size, start);

			//Helpers.PrintMatrix(CombineArrays(map, distance, (size.X, size.Y)), size.X, size.Y, 4);
		Helpers.PrintMatrixToFile("day12test map dump.txt", CombineArrays(map, distance, (size.X, size.Y)), size.X, size.Y, 4);
			return distance[end.X, end.Y].ToString();
		}
		protected override string Solution2(string[] input)
		{
			char[,] map = new char[input.Length, input[0].Length];

			int endX = 0, endY = 0;

			List<(int, int)> aList = new List<(int, int)>();

			for (int x = 0; x < input.Length; x++)
			{
				for (int y = 0; y < input[0].Length; y++)
				{
					map[x, y] = input[x][y];
					if (input[x][y] == 'a')
					{
						aList.Add((x, y));
					}
					if (input[x][y] == 'E')
					{
						endX = x;
						endY = y;
					}
				}
			}

			int xSize = input.Length;
			int ySize = input[0].Length;

			int lowestA = int.MaxValue;

			foreach (var item in aList)
			{
				int[,] distance = new int[xSize, ySize];
				for (int x = 0; x < input.Length; x++)
					for (int y = 0; y < input[0].Length; y++)
						distance[x, y] = -1;
				highest = 'a';

				//MapDistance(map, ref distance, (xSize, ySize), item.Item1, item.Item2, map[item.Item1, item.Item2], 'E', 0);

				if (distance[endX, endY] < lowestA && distance[endX, endY] != -1)
					lowestA = distance[endX, endY];

			}
			//Helpers.PrintMatrixToFile("day12test map dump.txt", CombineArrays(map, distance, (xSize, ySize)), xSize, ySize, 4);
			return lowestA.ToString();
		}



		private string[,] CombineArrays(char[,] map, int[,] distance, (int, int) size)
		{
			string[,] output = new string[size.Item1, size.Item2];

			for (int x = 0; x < size.Item1; x++)
			{
				for (int y = 0; y < size.Item2; y++)
				{
					if (distance[x, y] == -1)
						output[x, y] = map[x, y].ToString();
					else
						output[x, y] = distance[x, y].ToString();
				}
			}

			return output;
		}

		//for testing
		char highest = 'a';
		private void MapDistance(char[,] map, ref int[,] distance, Point size, Point current, Point last, char end, int depth = 0)
		{
			//Helpers.PrintMatrix(distance, size.Item1, size.Item2);
			//not in bounds
			if (!current.InBounds(size))
				return;

			char currentChar = map[current.X, current.Y];
			char lastChar = map[last.X, last.Y];

			//found a end
			if (lastChar == 'z' && currentChar == 'E')
			{
				Helpers.PrintMatrix(CombineArrays(map, distance, (size.X, size.Y)), size.X, size.Y, 4);
				if (distance[current.X, current.Y] > depth || distance[current.X, current.Y] == -1)
				{
					distance[current.X, current.Y] = depth;
				}
				return;
			}

			//found char other than E search
			if (lastChar == end)
			{
				if (distance[current.X, current.Y] > depth)
					distance[current.X, current.Y] = depth;
				return;
			}

			//a little hack because p is the max up from S without going down
			// n is the next best letter for searching up because it's the least down from p
			//if (lastChar == 'p' && currentChar == 'n')
			//{
			//	MapDistance(map, ref distance, size, new Point(current.X - 1, current.Y), new Point(current), end, depth + 1);
			//	MapDistance(map, ref distance, size, new Point(current.X, current.Y - 1), new Point(current), end, depth + 1);
			//	return;
			//}

			//two chars higher, no good
			if (currentChar > lastChar + 1 && lastChar != 'S')
				return;

			//any amount lower, as long as it has not been checked = good
			if (currentChar < lastChar && distance[current.X, current.Y] != -1)
			{
				return;
			}

			//testing
			if (highest <= currentChar)
			{
				highest = currentChar;
				Console.WriteLine(highest + " Depth " + depth);
			}

			if (distance[current.X, current.Y] > depth || distance[current.X, current.Y] == -1 && currentChar != 'E')
			{
				distance[current.X, current.Y] = depth;
			}
			else
			{
				return;
			}

			MapDistance(map, ref distance, size, new Point(current.X - 1, current.Y), new Point(current), end, depth + 1);//up
			MapDistance(map, ref distance, size, new Point(current.X, current.Y - 1), new Point(current), end, depth + 1);//left
			MapDistance(map, ref distance, size, new Point(current.X + 1, current.Y), new Point(current), end, depth + 1);//down
			MapDistance(map, ref distance, size, new Point(current.X, current.Y + 1), new Point(current), end, depth + 1);//right
		}




		private void MapDistance(char[,] map, ref int[,] distance, Point size, Point start)
		{
			Queue<Point> waiting = new Queue<Point>();

			waiting.Enqueue(start);
			waiting.Enqueue(start);
			int depth = 0;
			while (waiting.Count != 0)
			{
				Point current = waiting.Dequeue();
				Point startPoint = waiting.Dequeue();
				//char currentChar = map[cur.X, cur.Y];

				//find lowest distance
				int dis = -1;

				char curChar = map[current.X, current.Y];
				char pastchar = map[startPoint.X, startPoint.Y];

				char maxChar = (char)(((int)map[current.X, current.Y]) + 1);
				char minChar = (char)(((int)map[current.X, current.Y]) - 4);

				//if (distance[current.X - 1, current.Y] != -1&& map[current.X - 1, current.Y] <= (map[current.X, current.Y]) && map[current.X - 1, current.Y] >= (map[current.X, current.Y] - 3))
				//{
				//	dis = distance[current.X - 1, current.Y];
				//}//valid down
				//if (InBounds((size.X, size.Y), current.X + 1, current.Y) && distance[current.X + 1, current.Y] != -1
				//	&& map[current.X + 1, current.Y] <= (map[current.X, current.Y]) && map[current.X + 1, current.Y] <= (map[current.X, current.Y] - 3))
				//{
				//	dis = distance[current.X + 1, current.Y];
				//}//valid left
				//if (InBounds((size.X, size.Y), current.X, current.Y - 1) && distance[current.X, current.Y - 1] != -1
				//	&& map[current.X, current.Y - 1] <= (map[current.X, current.Y]) && map[current.X, current.Y - 1] <= (map[current.X, current.Y] - 3))
				//{
				//	dis = distance[current.X, current.Y - 1];
				//}//valid right
				//if (InBounds((size.X, size.Y), current.X, current.Y + 1) && distance[current.X, current.Y + 1] != -1
				//	&& map[current.X, current.Y + 1] <= (map[current.X, current.Y]) && map[current.X, current.Y + 1] <= (map[current.X, current.Y] - 3))
				//{
				//	dis = distance[current.X, current.Y + 1];
				//}

				if (current == start)//starting point
				{
					distance[current.X, current.Y] = 0;
				}
				else if (curChar == 'E' && pastchar == 'z')//end point
				{
					distance[current.X, current.Y] = distance[startPoint.X, startPoint.Y] + 1;
				}
				else if (curChar == 'E')
				{
					//ignore just E by it self
				}
				else
				{
					distance[current.X, current.Y] = distance[startPoint.X, startPoint.Y] + 1;
				}

				//add new items to queue
				//valid up
				if (InBounds((size.X, size.Y), current.X - 1, current.Y) && distance[current.X - 1, current.Y] == -1 && (map[current.X - 1, current.Y] <= (map[current.X, current.Y] + 1) || curChar == 'S'))
				{
					waiting.Enqueue(new Point(current.X - 1, current.Y));
					waiting.Enqueue(current);
				}//valid down
				if (InBounds((size.X, size.Y), current.X + 1, current.Y) && distance[current.X + 1, current.Y] == -1 && (map[current.X + 1, current.Y] <= (map[current.X, current.Y] + 1) || curChar == 'S'))
				{
					waiting.Enqueue(new Point(current.X + 1, current.Y));
					waiting.Enqueue(current);
				}//valid left
				if (InBounds((size.X, size.Y), current.X, current.Y - 1) && distance[current.X, current.Y - 1] == -1 && (map[current.X, current.Y - 1] <= (map[current.X, current.Y] + 1) ||  curChar == 'S'))
				{
					waiting.Enqueue(new Point(current.X, current.Y - 1));
					waiting.Enqueue(current);
				}//valid right
				if (InBounds((size.X, size.Y), current.X, current.Y + 1) && distance[current.X, current.Y + 1] == -1 && (map[current.X, current.Y + 1] <= (map[current.X, current.Y] + 1) || curChar == 'S'))
				{
					waiting.Enqueue(new Point(current.X, current.Y + 1));
					waiting.Enqueue(current);
				}
				depth++;
				//Helpers.PrintMatrix(CombineArrays(map, distance, (size.X, size.Y)), size.X, size.Y, 4);
			}
		}

		//very slow, maybe try having a array with distance from start
		private void FindShortestPath(char[,] map, (int, int) size, int x, int y, char lastChar, char findChar, ref bool[,] vis, int depth, Dictionary<(int, int), int> list)
		{
			if (!InBounds(size, x, y))
				return;

			char t = map[x, y];

			if (lastChar == 'z' && map[x, y] == 'E')
			{
				if (!list.ContainsKey((x, y)))
					list.Add((x, y), depth);

				if (list[(x, y)] > depth)
					list[(x, y)] = depth;
				return;
			}

			//found findChar
			if (map[x, y] == findChar && map[x, y] != 'E')
			{
				if (!list.ContainsKey((x, y)))
					list.Add((x, y), depth);

				if (list[(x, y)] > depth)
					list[(x, y)] = depth;
				return;
			}

			//two chars higher or one lower
			if ((map[x, y] > lastChar + 1 && lastChar != 'S') || (map[x, y] < lastChar && lastChar != 'S'))
				return;

			//already visited
			if (vis[x, y])
				return;

			//bool[,] visited = new bool[size.Item1, size.Item2];
			//Array.Copy(vis, visited, vis.Length);

			vis[x, y] = true;

			//can only move up down left right
			FindShortestPath(map, size, x - 1, y, map[x, y], findChar, ref vis, depth + 1, list);//up
			FindShortestPath(map, size, x + 1, y, map[x, y], findChar, ref vis, depth + 1, list);//down
			FindShortestPath(map, size, x, y - 1, map[x, y], findChar, ref vis, depth + 1, list);//left
			FindShortestPath(map, size, x, y + 1, map[x, y], findChar, ref vis, depth + 1, list);//right

			vis[x, y] = false;

			return;
		}

		private bool InBounds((int, int) size, int x, int y)
		{
			return (x >= 0 && y >= 0 && x < size.Item1 && y < size.Item2);
		}



	}
}
