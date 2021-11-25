using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using AOC;

namespace AdventOfCode2019
{
	internal class Day10 : BaseDay
	{
		public Day10()
		{
			Day = "10";
			Answer1 = "292";
			Answer2 = "317";
		}

		protected override string Solution1(string[] input)
		{
			int width = input[0].Length;
			int height = input.Length;
			char[,] map = new char[width, height];

			for (int x = 0; x < height; x++)
				for (int y = 0; y < width; y++)
					map[x, y] = input[x][y];

			long[] output = FindBestStation(map, width, height);

			return output[2].ToString();
		}

		protected override string Solution2(string[] input)
		{
			int width = input[0].Length;
			int height = input.Length;
			char[,] map = new char[width, height];
			{
				for (int x = 0; x < height; x++)
					for (int y = 0; y < width; y++)
						map[x, y] = input[x][y];
			}
			long[] best = FindBestStation(map, width, height);

			char[,,] data = new char[width, height, 2];

			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					data[x, y, 0] = map[x, y];
					data[x, y, 1] = (char)0;
				}
			}

			int bestX = (int)best[0];
			int bestY = (int)best[1];

			int currentX = 0;
			int currentY = 0;

			int count = 0;

			bool running = true;

			while (running)
			{
				//up
				var up = FindUp(bestX, bestY, data, width, height);
				var down = FindDown(bestX, bestY, data, width, height);
				var left = FindRight(bestX, bestY, data, width, height);
				var right = FindLeft(bestX, bestY, data, width, height);

				var upRight = FindUpRight(bestX, bestY, data, width, height);
				var downRight = FindDownRight(bestX, bestY, data, width, height);
				var downLeft = FindDownLeft(bestX, bestY, data, width, height);
				var upLeft = FindUpLeft(bestX, bestY, data, width, height);

				//reset visited state
				for (int x = 0; x < height; x++)
					for (int y = 0; y < width; y++)
						data[x, y, 1] = (char)0;

				if (up.Count > 0)
				{
					DistroyAstroid(data, up.ElementAt(0).Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//up right
				foreach (var item in upRight.Reverse())
				{
					DistroyAstroid(data, item.Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//right
				if (right.Count > 0)
				{
					DistroyAstroid(data, right.ElementAt(0).Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//down right
				foreach (var item in downRight.Reverse())
				{
					DistroyAstroid(data, item.Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//down
				if (down.Count > 0)
				{
					DistroyAstroid(data, down.ElementAt(0).Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//down left
				foreach (var item in downLeft.Reverse())
				{
					DistroyAstroid(data, item.Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//left
				if (left.Count > 0)
				{
					DistroyAstroid(data, left.ElementAt(0).Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				//up left
				foreach (var item in upLeft.Reverse())
				{
					DistroyAstroid(data, item.Value, ref currentX, ref currentY);
					count++; Visual(bestX, bestY, currentX, currentY, data, width, height);
					if (count == 200 && running)
					{
						running = false;
						break;
					}
				}
				if (count == 200 && running)
				{
					running = false;
					break;
				}

				Visual(bestX, bestY, currentX, currentY, data, width, height);

				//int i = 0;
			}

			return ((currentY * 100) + currentX).ToString();
		}

		internal static void Visual(int startX, int startY, int currentX, int currentY, char[,,] data, int width, int height)
		{
			//for (int x = 0; x < height; x++)
			//{

			//	for (int y = 0; y < width; y++)
			//	{
			//		Console.ResetColor();
			//		if (x == startX && y == startY)
			//		{
			//			Console.BackgroundColor = ConsoleColor.Green;
			//			Console.Write(data[x, y, 0]);
			//		}
			//		else if (x == currentX && y == currentY)
			//		{
			//			Console.BackgroundColor = ConsoleColor.Red;
			//			Console.Write(data[x, y, 0]);
			//		}
			//		else
			//			Console.Write(data[x, y, 0]);
			//	}
			//	Console.WriteLine();
			//}
			//Console.WriteLine("Distroy x={0} y={1}", currentY, currentX);
			//Console.ReadLine();
		}

		internal static void DistroyAstroid(char[,,] data, string coords, ref int x, ref int y)
		{
			x = int.Parse(coords.Split(',')[0]);
			y = int.Parse(coords.Split(',')[1]);

			data[x, y, 0] = '.';
		}

		internal static SortedDictionary<double, string> FindUp(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i >= 0; i--)
			{
				if (data[i, y, 1] == (char)1) continue;//has been visited before
				data[i, y, 1] = (char)1;

				if (x == i) { continue; }//same tile
				if (data[i, y, 0] == '.') continue;//void space						

				if (output.ContainsKey(1)) continue;
				output.Add(1, i + "," + y);
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindDown(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i < height; i++)
			{
				if (data[i, y, 1] == (char)1) continue;//has been visited before
				data[i, y, 1] = (char)1;
				if (x == i) { continue; }//same tile
				if (data[i, y, 0] == '.') continue;//void space						

				if (output.ContainsKey(1)) continue;
				output.Add(1, i + "," + y);
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindRight(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();

			for (int j = y; j >= 0; j--)
			{
				if (data[x, j, 1] == (char)1) continue;//has been visited before
				data[x, j, 1] = (char)1;

				if (y == j) { continue; }//same tile
				if (data[x, j, 0] == '.') continue;//void space

				if (output.ContainsKey(1)) continue;
				output.Add(1, x + "," + j);
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindLeft(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int j = y; j < height; j++)
			{
				if (data[x, j, 1] == (char)1) continue;//has been visited before
				data[x, j, 1] = (char)1;

				if (y == j) { continue; }//same tile
				if (data[x, j, 0] == '.') continue;//void space

				if (output.ContainsKey(1)) continue;
				output.Add(1, x + "," + j);
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindUpLeft(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i >= 0; i--)
			{
				for (int j = y; j >= 0; j--)
				{
					if (x == i && y == j) continue;//same tile
					if (data[i, j, 0] == '.') continue;//void space
					if (data[i, j, 1] == (char)1) continue;//has been visited before

					//double ratio = (double)((double)((double)i - (double)x) / (double)((double)j - (double)y));

					double xDiff = (double)((double)i - (double)x);
					double yDiff = (double)((double)j - (double)y);
					double ratio = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

					if (output.ContainsKey(ratio)) continue;

					output.Add(ratio, i + "," + j);
					data[i, j, 1] = (char)1;
				}
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindUpRight(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i >= 0; i--)
			{
				for (int j = y; j < width; j++)
				{
					if (x == i && y == j) continue;//same tile
					if (data[i, j, 0] == '.') continue;//void space
					if (data[i, j, 1] == (char)1) continue;//has been visited before

					//double ratio = (double)((double)((double)i - (double)x) / (double)((double)j - (double)y));
					double xDiff = (double)((double)i - (double)x);
					double yDiff = (double)((double)j - (double)y);
					double ratio = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

					if (output.ContainsKey(ratio)) continue;

					output.Add(ratio, i + "," + j);
					data[i, j, 1] = (char)1;
				}
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindDownLeft(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i < height; i++)
			{
				for (int j = y; j >= 0; j--)
				{
					if (x == i && y == j) continue;//same tile
					if (data[i, j, 0] == '.') continue;//void space
					if (data[i, j, 1] == (char)1) continue;//has been visited before

					//double ratio = (double)((double)((double)i - (double)x) / (double)((double)j - (double)y));
					double xDiff = (double)((double)i - (double)x);
					double yDiff = (double)((double)j - (double)y);
					double ratio = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

					if (output.ContainsKey(ratio)) continue;

					output.Add(ratio, i + "," + j);
					data[i, j, 1] = (char)1;
				}
			}
			return output;
		}

		internal static SortedDictionary<double, string> FindDownRight(int x, int y, char[,,] data, int width, int height)
		{
			SortedDictionary<double, string> output = new SortedDictionary<double, string>();
			for (int i = x; i < height; i++)
			{
				for (int j = y; j < width; j++)
				{
					if (x == i && y == j) continue;//same tile
					if (data[i, j, 0] == '.') continue;//void space
					if (data[i, j, 1] == (char)1) continue;//has been visited before

					//double ratio = (double)((double)((double)i - (double)x) / (double)((double)j - (double)y));
					double xDiff = (double)((double)i - (double)x);
					double yDiff = (double)((double)j - (double)y);
					double ratio = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
					if (output.ContainsKey(ratio)) continue;

					output.Add(ratio, i + "," + j);
					data[i, j, 1] = (char)1;
				}
			}
			return output;
		}

		internal static long[] FindBestStation(char[,] map, int width, int height)
		{
			char[,,] data = new char[width, height, 2];
			char[,,] dataBackup = new char[width, height, 2];

			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					data[x, y, 0] = map[x, y];
					data[x, y, 1] = (char)0;
				}
			}
			Array.Copy(data, dataBackup, width * height * 2);

			int mostX = 0;
			int mostY = 0;
			int detectedCount = 0;

			//search for visables
			for (int x = 0; x < height; x++)
			{
				for (int y = 0; y < width; y++)
				{
					if (data[x, y, 0] == '.') continue;
					Array.Copy(dataBackup, data, width * height * 2);

					int count = 0;

					List<double> ratios = new List<double>();
					SortedDictionary<double, string> ratiosD;
					//up stright
					ratiosD = FindUp(x, y, data, width, height);
					count += ratiosD.Count;

					//down stright
					ratiosD = FindDown(x, y, data, width, height);
					count += ratiosD.Count;

					//left stright
					ratiosD = FindRight(x, y, data, width, height);
					count += ratiosD.Count;

					//right stright
					ratiosD = FindLeft(x, y, data, width, height);
					count += ratiosD.Count;

					//left up
					ratiosD = FindUpLeft(x, y, data, width, height);
					count += ratiosD.Count;

					//right up
					ratiosD = FindUpRight(x, y, data, width, height);
					count += ratiosD.Count;

					//right down
					ratiosD = FindDownLeft(x, y, data, width, height);
					count += ratiosD.Count;

					//left down
					ratiosD = FindDownRight(x, y, data, width, height);
					count += ratiosD.Count;

					if (count > detectedCount)
					{
						detectedCount = count;
						mostX = x;
						mostY = y;
					}
				}
			}
			return new long[] { mostX, mostY, detectedCount };
		}
	}
}